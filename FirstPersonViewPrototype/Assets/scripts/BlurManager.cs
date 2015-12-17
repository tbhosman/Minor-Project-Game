using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BlurManager : MonoBehaviour {

	private GameObject Player;
	private float distance;
	public float bluramount;
	public GameObject ObjectToWalkTo;
	public float distanceToLerpFrom;
	public float maxblur;
	private GameObject mainCamera;

	void Start (){
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		Player = GameObject.FindGameObjectWithTag ("Player");
	}
	

	void Update () {
		distance = Vector3.Distance (Player.transform.position, ObjectToWalkTo.transform.position);
		if (distance < distanceToLerpFrom) {
			bluramount = Mathf.Lerp(maxblur,0,distance/distanceToLerpFrom);
			mainCamera.GetComponent<MotionBlur>().blurAmount = bluramount;
		}
	}

	void Assignbluramount(){
	}
}
