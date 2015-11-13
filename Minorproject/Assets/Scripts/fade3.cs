using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class fade3 : MonoBehaviour {
	
	float timer;
	public float duration = 0.5f;
	public Light dimLight;
	public float zoomSpeed1 = 0.05f;
	public float zoomSpeed2 = 0.5f;
	Camera c;

	void Start () {
		timer = 0.0f;
		c = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > 22.0f && timer < 27.0f) {
			dimLight.intensity += zoomSpeed1;
		} else if (timer > 27.0f) {
			dimLight.intensity -= zoomSpeed1;
		}
		//Debug.Log (timer);
	}
}
