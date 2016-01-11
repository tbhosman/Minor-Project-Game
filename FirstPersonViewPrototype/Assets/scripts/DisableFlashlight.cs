/// <summary>
/// Script for flickering flashlight (used in scary moments and picking up the flashlight)
/// </summary>

using UnityEngine;
using System.Collections;

public class DisableFlashlight : MonoBehaviour {
	public GameObject flashlight;

	void Start(){
		StartCoroutine (run ());
	}

	IEnumerator run(){
		flashlight.SetActive (false);
		yield return new WaitForSeconds(0.5f);
		flashlight.SetActive (true);
		yield return new WaitForSeconds(0.5f);
		flashlight.SetActive (false);
		yield return new WaitForSeconds(0.2f);
		flashlight.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		flashlight.SetActive (false);
		yield return new WaitForSeconds(0.05f);
		flashlight.SetActive (true);
		yield return new WaitForSeconds(0.05f);
		flashlight.SetActive(false);

	}
}
