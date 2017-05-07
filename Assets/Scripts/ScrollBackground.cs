using UnityEngine;
using System.Collections;

public class ScrollBackground : MonoBehaviour {

	public float scrollSpeed;
	public float tileSize;

	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float newPos = Mathf.Repeat (Time.time * scrollSpeed, tileSize);
		transform.position = startPos + Vector3.up * newPos;
	}
}
