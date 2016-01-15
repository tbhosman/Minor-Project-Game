using UnityEngine;
using System.Collections;

public class Active_Flicker : MonoBehaviour {
	public float RangeOfRepeat;
	public float RangeOfFlicker;
	// Use this for initialization
	void Start () {
		Invoke ("StartLightFlicker",0.5f);
	}
	
	void StartLightFlicker(){
		float RandomTime = Random.Range (RangeOfFlicker, RangeOfRepeat);
		StartCoroutine (LightFlicker());
		Debug.Log ("LightFlicker " + RandomTime);
		Invoke("StartLightFlicker", RandomTime);
	}

	IEnumerator LightFlicker(){
		gameObject.GetComponent<Light> ().enabled = false;;
		float RandTime = Random.Range (0, RangeOfFlicker);
		Debug.Log ("LightFlickerActiveAfter: " + RandTime);
		yield return new WaitForSeconds(RandTime);
		gameObject.GetComponent<Light> ().enabled = true;
	}


}
