using UnityEngine;
using System.Collections;

public class zoomer : MonoBehaviour {

	public int level = 1;
	public float setTime = 1.5f;
	public float dimTime = 2.0f;
	public Light dimLight;
	public float zoomSpeed = 0.01f;

	Camera c;
	float timer;

	// Use this for initialization
	void Start () {
		c = GetComponent<Camera> ();
		timer = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		c.fieldOfView += zoomSpeed;

	}
}
