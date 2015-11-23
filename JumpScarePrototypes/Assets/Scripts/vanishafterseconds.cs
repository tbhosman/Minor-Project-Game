using UnityEngine;
using System.Collections;

public class vanishafterseconds : MonoBehaviour {

	public float seconds;
	// Use this for initialization
	void Start () {
		StartCoroutine (Shutdown ());
	}
	
	// Update is called once per frame
	void Update () {
	}

	
	IEnumerator Shutdown(){
		yield return new WaitForSeconds (seconds);
		gameObject.SetActive (false);
	}

}
