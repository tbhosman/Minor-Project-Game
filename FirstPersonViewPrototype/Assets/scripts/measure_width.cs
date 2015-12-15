using UnityEngine;
using System.Collections;

public class measure_width : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("size: " + GetComponent<Renderer>().bounds);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
