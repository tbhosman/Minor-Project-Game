///<summary>
/// motion blur
/// </summary>

using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class AdjustMotionBlur : MonoBehaviour {
	
	private Camera mainCam;
	private GameObject Player;
	public float BlurMiltipliert;
	public GameObject StartBlurFrom;
	public float BlurAmount;

	void Start(){
		BlurAmount = 1;
		Player = GameObject.FindGameObjectWithTag ("Player");
		mainCam = Camera.main;
	}

	void Update () {
		mainCam.GetComponent<MotionBlur> ().blurAmount = Vector3.Distance (Player.transform.position, StartBlurFrom.transform.position)*BlurAmount;
		BlurAmount = mainCam.GetComponent<MotionBlur> ().blurAmount;
	}

	void OnTriggerEnter(){
		mainCam.GetComponent<MotionBlur> ().blurAmount = 0;
		gameObject.SetActive (false);
	}
}
