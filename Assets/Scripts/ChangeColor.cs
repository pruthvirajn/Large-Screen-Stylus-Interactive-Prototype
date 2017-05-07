using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {
	public Material active;
	public Material passive;
	private Vector3 thisObj;
	private Vector3 targetScale;
	public float scaleTime = 0.1f;
	public float scaleFactor = 0.01f;
	private bool isActive = false;

	// Use this for initialization
	void Start () {
		thisObj = transform.localScale;
		targetScale = new Vector3 (transform.localScale.x + scaleFactor, transform.localScale.y, transform.localScale.z + scaleFactor);
	}

	void Update(){
		if(ActivateStylus.isHandLost){
			if (!isActive)
				return;

			isActive = false;
			StopAllCoroutines ();
			StartCoroutine("scaleDownUnit", scaleTime);
			GetComponent<Renderer> ().material = passive;
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
