using UnityEngine;
using System.Collections;

public class ThirdUnitFitScreen : MonoBehaviour {

	public GameObject Focus;
	public GameObject selectProgress;
	public GameObject DetectHand;
	public GameObject unitMiddle;
	public float progressTime = 1.0f;
	private bool isActive = false;
	private float rate;
	private float i = 0;
	public float xPos = 7.0f;
	public Material active;
	public Material passive;
	private Vector3 thisObj;
	private Vector3 targetScale;
	public GameObject modalPlane;
	private Vector3 targetPosition;
	private Vector3 originPosition;
	private Vector3 zoomScale;
	public float scaleTime = 0.1f;
	public float scaleFactor = 0.01f;
	public float zoomFactor = 0.9f;
	
	// Use this for initialization
	void Start () {
		rate = 1 / progressTime;
		thisObj = transform.localScale;
		targetScale = new Vector3 (transform.localScale.x + scaleFactor, transform.localScale.y, transform.localScale.z + scaleFactor);
		zoomScale = new Vector3 (transform.localScale.x + zoomFactor, transform.localScale.y, transform.localScale.z + zoomFactor);
		targetPosition = new Vector3 (unitMiddle.transform.localPosition.x, 0.0f, unitMiddle.transform.localPosition.z - 3);
		originPosition = new Vector3 (xPos, targetPosition.y, targetPosition.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(ActivateStylus.isHandLost){
			if (!isActive)
				return;
			
			isActive = false;
			//StopAllCoroutines ();
			//StartCoroutine("scaleDownUnit", scaleTime);
			GetComponent<Renderer> ().material = passive;
			float t = 0;
			do {
				transform.localPosition = Vector3.Lerp(transform.localPosition, originPosition, t / 0.8f);
				t += Time.deltaTime;
			} while (t < scaleTime);
		}
		
		if (isActive) {
			i += Time.deltaTime * rate;
			selectProgress.GetComponent<Renderer> ().material.SetFloat ("_Progress", i);
			
			if (i >= 1) {
				selectProgress.GetComponent<Renderer> ().material.SetFloat ("_Progress", 0);
				float t = 0;
				do {
					transform.localScale = Vector3.Lerp (thisObj, zoomScale, t / scaleTime);
					transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, t / 0.8f);
					t += Time.deltaTime;
				} while (t < scaleTime);
				selectProgress.SetActive (false);
				Focus.SetActive (false);
				DetectHand.SetActive(false);
				modalPlane.SetActive(true);
			}
		} else {
			selectProgress.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
			i = 0;
		}
	}
	
	void OnTriggerEnter(Collider col){
		Debug.Log ("Collision detected");
		
		if (isActive)
			return;
		
		isActive = true;
		StartCoroutine("scaleUpUnit", scaleTime);
		GetComponent<Renderer> ().material = active;
	}
	
	void OnTriggerExit(Collider col){
		Debug.Log ("Collision bypassed");
		
		if (!isActive)
			return;
		
		isActive = false;
		//StopAllCoroutines ();
		//StartCoroutine("scaleDownUnit", scaleTime);
		GetComponent<Renderer> ().material = passive;
	}
	
	IEnumerator scaleUpUnit(float scaleTime){
		Debug.Log ("scaleUpUnit");
		float t = 0;
		do {
			transform.localScale = Vector3.Lerp (thisObj, targetScale, t / scaleTime);
			yield return null;
			t += Time.deltaTime;
		} while (t < scaleTime);
		transform.localScale = targetScale;
		yield break;
	}
	
	IEnumerator scaleDownUnit(float scaleTime){
		Debug.Log ("scaleDownUnit");
		float t = 0;
		do {
			transform.localScale = Vector3.Lerp (transform.localScale, thisObj, t / scaleTime);
			yield return null;
			t += Time.deltaTime;
		} while (t < scaleTime);
		transform.localScale = thisObj;
		yield break;
	}
	
	IEnumerator scaleWindowSize(float scaleTime){
		Debug.Log ("scaleDownUnit");
		float t = 0;
		do {
			transform.localScale = Vector3.Lerp (thisObj, zoomScale, t / scaleTime);
			transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, t / scaleTime);
			yield return null;
			t += Time.deltaTime;
		} while (t < scaleTime);
		transform.localScale = targetScale;
		transform.localPosition = targetPosition;
		yield break;
	}
}
