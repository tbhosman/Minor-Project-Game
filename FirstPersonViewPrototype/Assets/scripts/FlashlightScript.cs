using UnityEngine;
using System.Collections;

public class FlashlightScript : MonoBehaviour {
    public toggleFlashLight toggleflashlight;
	private Light flashlight;
   
    
    void Start()
    {
        flashlight = this.GetComponent<Light>();
        flashlight.enabled = false;
    }
	void Update () {

  
        if (Input.GetKeyDown("f")&&toggleflashlight.getLight()){
			
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