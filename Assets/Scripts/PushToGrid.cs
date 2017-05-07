using UnityEngine;
using System.Collections;

public class PushToGrid : MonoBehaviour {
	public GameObject Focus;
	public GameObject GridUnit;
	public GameObject selectProgress;
	public GameObject dropProgress;
	public GameObject dropPoint;
	public GameObject detectHand;
	public float progressTime = 1.0f;
	private bool isActive = false;
	private bool isDrop = false;
	public bool isGridSelected = false;
	private float rate;
	private float i = 0;
	private float j = 0;
	// Use this for initialization
	void Start () {
		rate = 1 / progressTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(ActivateStylus.isHandLost){
			if (!isActive)
				return;
			
			isActive = false;
			isDrop = false;
			selectProgress.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
			dropProgress.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
			i = 0;
			j = 0;
		}

		if ((Focus.transform.position.y >= -1.0f && Focus.transform.position.y <= 4.0f) && 
		    (Focus.transform.position.x >= -3.0f && Focus.transform.position.x <= 3.0f) && 
		    isGridSelected) {
			isDrop = true;
		}

		if (isActive ) {
			i += Time.deltaTime * rate;
			selectProgress.GetComponent<Renderer>().material.SetFloat("_Progress", i);
			
			if(i >= 1)
			{
				selectProgress.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
				this.transform.parent = Focus.transform;
				isGridSelected = true;
				selectProgress.SetActive(false);
			}
		}
		else {
			selectProgress.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
			i = 0;
		}

		if (isDrop ) {
			j += Time.deltaTime * rate;
			dropProgress.GetComponent<Renderer>().material.SetFloat("_Progress", j);
			
			if(j >= 1)
			{
				dropProgress.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
				this.transform.parent = dropPoint.transform;
				this.transform.position = new Vector3(0.0f, 1.5f, -4.0f);
				Focus.SetActive(false);
				dropProgress.SetActive(false);
				detectHand.SetActive(false);
			}
		}
		else {
			dropProgress.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
			j = 0;
		}
	}
	
	void OnTriggerEnter(Collider col){
		Debug.Log ("Collision detected");
		
		if (isActive)
			return;
		
		isActive = true;
	}
	
	void OnTriggerExit(Collider col){
		Debug.Log ("Collision bypassed");
		
		if (!isActive)
			return;
		
		isActive = false;
	}
}
