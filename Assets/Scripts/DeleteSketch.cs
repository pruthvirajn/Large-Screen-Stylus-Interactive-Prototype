using UnityEngine;
using System.Collections;

public class DeleteSketch : MonoBehaviour {
	public GameObject Focus;
	public GameObject GridUnit;
	public GameObject deleteProgress;
	public GameObject detectHand;
	public float progressTime = 1.0f;
	public GUIText statusText;
	private bool isActive = false;
	public bool isGridSelected = false;
	private float rate;
	private float i = 0;
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
			deleteProgress.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
			i = 0;
		}
		if (Focus.transform.position.y >= 7.5f && isGridSelected) {
			Destroy(GridUnit);
			Focus.SetActive(false);
			detectHand.SetActive(false);
			statusText.text = "Item Deleted";
		}
		if (isActive ) {
			i += Time.deltaTime * rate;
			deleteProgress.GetComponent<Renderer>().material.SetFloat("_Progress", i);
			
			if(i >= 1)
			{
				deleteProgress.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
				this.transform.parent = Focus.transform;
				isGridSelected = true;
				deleteProgress.SetActive(false);
			}
		}
		else {
			deleteProgress.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
			i = 0;
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
