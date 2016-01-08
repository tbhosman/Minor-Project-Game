using UnityEngine;
using System.Collections;

public class loadAnimation : MonoBehaviour {

	public float turnSpeed = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.rotation = Quaternion.Euler(new Vector3(0,0,turnSpeed) + transform.rotation.eulerAngles);
	}
}
