using UnityEngine;
using System.Collections;

public class ZoomInAction : MonoBehaviour {

	private Vector3 thisObj;
	private Vector3 targetScale;
	private bool isActive = false;
	public GameObject Focus;
	private float scaleTime = 3.0f;
	private float scaleFactor = 2.0f;
	private float initScale = 0;
	private bool scaleUp = true;
	private bool scaleDown = true;

	// Use this for initialization
	void Start () {
		thisObj = transform.localScale;
		targetScale = new Vector3 (transform.localScale.x + scaleFactor, transform.localScale.y, transform.localScale.z + scaleFactor);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Focus.transform.position.z > 0 && scaleUp) {
			scaleUp = false;
			scaleDown = true;
			targetScale = new Vector3 (transform.localScale.x + scaleFactor, transform.localScale.y, transform.localScale.z + scaleFactor);
			StopAllCoroutines();
			StartCoroutine("scaleUpUnit", scaleTime);
		}
		else if(Focus.transform.position.z < 0 && scaleDown){
			scaleDown = false;
			scaleUp = true;
			targetScale = new Vector3 (transform.localScale.x - scaleFactor, transform.localScale.y, transform.localScale.z - scaleFactor);
			StopAllCoroutines();
			StartCoroutine("scaleDownUnit", scaleTime);
		}
	}

	IEnumerator scaleUpUnit(float scaleTime){
		Debug.Log ("scaleUpUnit");
		float t = 0;
		do {
			transform.localScale = Vector3.Lerp (transform.localScale, targetScale, t / scaleTime);
			yield return null;
			t += Time.deltaTime;
		} while (t < scaleTime);
		//transform.localScale = targetScale;
		yield break;
	}
	
	IEnumerator scaleDownUnit(float scaleTime){
		Debug.Log ("scaleDownUnit");
		float t = 0;
		do {
			transform.localScale = Vector3.Lerp (transform.localScale, targetScale, t / scaleTime);
			yield return null;
			t += Time.deltaTime;
		} while (t < scaleTime);
		//transform.localScale = thisObj;
		yield break;
	}
}
