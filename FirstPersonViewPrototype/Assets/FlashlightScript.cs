using UnityEngine;
using System.Collections;

public class FlashlightScript : MonoBehaviour {

	private Light flashlight;

	void Start () {
	
		flashlight = this.GetComponent<Light> ();

	}
	
	void Update () {
		
		if (Input.GetKeyDown("f")){
			
			if (flashlight.enabled == true)
			{
				flashlight.enabled = false;
			}
			else
			{
				flashlight.enabled = true;
			}
			
		}
	}
}