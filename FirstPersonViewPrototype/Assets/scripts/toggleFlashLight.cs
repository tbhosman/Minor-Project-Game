///<summary>
/// pickup script for the flashlight. also displays a message
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class toggleFlashLight : MonoBehaviour {
   private bool toggleLight;
    public Text FlashLightText;
    // Use this for initialization
	void Start () {
        toggleLight = false;
        FlashLightText.text = "";
    }


    public bool getLight()
    {
        return toggleLight;
    }
    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            FlashLightText.text = "-Press e to take this flashlight-";
            if (Input.GetKeyDown("e"))
            {
                toggleLight = true;
                gameObject.SetActive(false);
                FlashLightText.text = "";
            }
        }
    }
    
    void OnTriggerExit(Collider coll)
    {
            FlashLightText.text = "";
    }
}
