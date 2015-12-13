using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class fillStamina : MonoBehaviour {

    public float scaleStam = 0f;
    public RectTransform StaminaTransform;
    private FirstPersonController fps;
    public float localStamina;
    public GameObject fpsController;

    void Start() {
        fps = fpsController.GetComponent<FirstPersonController>();
        
    }

	
	void Update () {
        StaminaTransform.localScale = new Vector3(scaleStam, StaminaTransform.localScale.y, StaminaTransform.localScale.z);
        scaleStam = 1.0f-(fps.stamina/100);
    }

    public void Scale(float scale) {
        if (scale < 0)
            scale = 0;
        else if (scale > 1) {
            scale = 1;
        }
        scaleStam = scale;
    }
}
