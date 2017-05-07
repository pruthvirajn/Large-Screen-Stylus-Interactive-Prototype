using UnityEngine;
using System.Collections;

public class DeleteGrid : MonoBehaviour {
	public GameObject Focus;
	public GameObject GridUnit;
	public GameObject deleteProgress;
	public GameObject deleteParticles;
	public float progressTime = 1.0f;
	public GUIText statusText;
	private bool isActive = false;
	private float rate;
	private float i = 0;
	// Use this for initialization
	void Start () {
		rate = 1 / progressTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Focus.transform.position.x >= 2 && Focus.transform.position.x <= 7) {
			isActive = true;
		}
		else {
			isActive = false;
			deleteProgress.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
			i = 0;
		}

		if (isActive ) {
			i += Time.deltaTime * rate;
			deleteProgress.GetComponent<Renderer>().material.SetFloat("_Progress", i);
			
			if(i >= 1)
			{
				deleteProgress.GetComponent<Renderer>().material.SetFloat("_Progress", 0);
				Destroy(GridUnit);
				if(deleteProgress.activeSelf)
				Instantiate(deleteParticles, transform.position, transform.rotation);
				statusText.text = "Item Deleted";
				deleteProgress.SetActive(false);
			}
		}
	}
}
