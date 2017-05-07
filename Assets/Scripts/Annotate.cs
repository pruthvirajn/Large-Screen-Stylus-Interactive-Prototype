using UnityEngine;
using System.Collections;

public class Annotate : MonoBehaviour {

	public Material active;
	public GameObject DetectHand;
	public GameObject focus;
	public Material passive;
	private Vector3 thisObj;
	private Vector3 targetScale;
	public GameObject annotateProgress;
	public GameObject tick;
	public float scaleTime = 0.1f;
	public float scaleFactor = 0.5f;
	private bool isActive = false;
	public float progressTime = 1.0f;
	private bool isAnnotate = false;
	public float rate;
	private float i = 0;
	
	// Use this for initialization
	void Start () {
		rate = 1 / progressTime;
		thisObj = transform.localScale;
		targetScale = new Vector3 (transform.localScale.x + scaleFactor, transform.localScale.y, transform.localScale.z + scaleFactor);
	}
	
	void Update(){
		if(ActivateStylus.isHandLost){
			if (!isActive)
				return;
			
			isActive = false;
			//StopAllCoroutines ();
			//StartCoroutine("scaleDownUnit", scaleTime);
			GetComponent<Renderer> ().material = passive;
		}

		if (Input.GetKeyDown("space")){
			Debug.Log ("space click detected");
			isAnnotate = true;
			DetectHand.SetActive(false);
			focus.SetActive(false);
		}

		if (isAnnotate) {
				i += Time.deltaTime * rate;
				annotateProgress.GetComponent<Renderer> ().material.SetFloat ("_Progress", i);
	
				if (i >= 1) {
					tick.SetActive(true);
				}
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
		StopAllCoroutines ();
		StartCoroutine("scaleDownUnit", scaleTime);
		annotateProgress.GetComponent<Renderer> ().material.SetFloat ("_Progress", 0);
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
}