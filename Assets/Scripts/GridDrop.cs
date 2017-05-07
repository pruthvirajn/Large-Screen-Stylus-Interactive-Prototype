using UnityEngine;
using System.Collections;

public class GridDrop : MonoBehaviour {
	public Material active;
	public Material passive;
	private Vector3 thisObj;
	public GameObject focus;
	public GameObject DetectHand;
	public GameObject progressDot;
	public GameObject dropProgress01;
	public GameObject dropProgress02;
	public GameObject dropProgress03;
	public GameObject Grid;
	public GameObject dropBox01;
	public GameObject dropBox02;
	public GameObject dropBox03;
	private Vector3 targetScale;
	public float progressTime = 1.0f;
	public float scaleTime = 0.1f;
	private bool isActive = false;
	private float rate;
	private float i = 0;
	private float j = 0;
	public bool isGridSelected = false;
	// Use this for initialization
	void Start () {
		rate = 1 / progressTime;
		thisObj = this.transform.localScale;
		targetScale = new Vector3 (0.6f, 1f, 0.4f);
	}
	
	void FixedUpdate(){
		if(ActivateStylus.isHandLost){
			if (!isActive)
				return;
			
			isActive = false;
			GetComponent<Renderer> ().material = passive;
			progressDot.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
			i = 0;
			j = 0;
		}
		if (isActive) {
			i += Time.deltaTime * rate;
			progressDot.GetComponent<Renderer>().material.SetFloat("_Progress", i);

			if(i >= 1)
			{
				this.transform.parent = focus.transform;
				StartCoroutine("scaleUpUnit", scaleTime);
				isGridSelected = true;
				progressDot.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
				progressDot.SetActive (false);
			}
		}
		if (isGridSelected) {

			if((focus.transform.position.x >= -9) && (focus.transform.position.x <= -3) &&
			   (focus.transform.position.y <= 4) && (focus.transform.position.y >= -1)){
					Debug.Log("Grid is selected");
				dropProgress02.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
				dropProgress03.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
					j += Time.deltaTime * rate;
					dropProgress01.GetComponent<Renderer>().material.SetFloat("_Progress", j);
					
					if(j >= 1)
					{
						dropProgress01.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
						this.transform.position = dropBox01.transform.position;
						StartCoroutine("scaleUpUnit", scaleTime);
						this.transform.parent = Grid.transform;
						focus.SetActive(false);
					DetectHand.SetActive(false);
						dropProgress01.SetActive (false);
						//isGridSelected = false;
					}
			}
			if((focus.transform.position.x >= -3) && (focus.transform.position.x <= 3) &&
			   (focus.transform.position.y <= 4) && (focus.transform.position.y >= -1)){
				dropProgress01.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
				dropProgress03.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
					j += Time.deltaTime * rate;
					dropProgress02.GetComponent<Renderer>().material.SetFloat("_Progress", j);
					
					if(j >= 1)
					{
						dropProgress02.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
						this.transform.position = dropBox02.transform.position;
						StartCoroutine("scaleUpUnit", scaleTime);
						this.transform.parent = Grid.transform;
						focus.SetActive(false);
					DetectHand.SetActive(false);
						dropProgress02.SetActive (false);
						//isGridSelected = false;
					}
			}
			if((focus.transform.position.x >= 3) && (focus.transform.position.x <= 9) &&
			   (focus.transform.position.y <= 4) && (focus.transform.position.y >= -1)){
				dropProgress02.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
				dropProgress01.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
					j += Time.deltaTime * rate;
					dropProgress03.GetComponent<Renderer>().material.SetFloat("_Progress", j);
					
					if(j >= 1)
					{
	 					dropProgress03.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
						this.transform.position = dropBox03.transform.position;
						StartCoroutine("scaleUpUnit", scaleTime);
						this.transform.parent = Grid.transform;
						focus.SetActive(false);
					DetectHand.SetActive(false);
						dropProgress03.SetActive (false);
						//isGridSelected = false;
					}
			}
		}
	}
	
	void OnTriggerEnter(Collider col){
		Debug.Log ("Collision detected");
		
		if (!isActive) {
			isActive = true;
			GetComponent<Renderer> ().material = active;
		}
	}
	
	void OnTriggerExit(Collider col){
		Debug.Log ("Collision bypassed");
		
		if (!isActive)
			return;
		
		isActive = false;
		GetComponent<Renderer> ().material = passive;
		dropProgress01.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
		dropProgress02.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
		dropProgress03.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
		progressDot.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
		i = 0;
		j = 0;
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
}
