using UnityEngine;
using System.Collections;

public class ActivateStylus : MonoBehaviour {
	public GameObject focus;
	public static bool isHandLost = false;

	void HandDetected(){
		Debug.Log ("HandDetected");
		isHandLost = false;
		focus.SetActive (true);
	}

	void HandLost(){
		Debug.Log ("HandLost");
		isHandLost = true;
		focus.SetActive (false);
	}
}
