///<summary>
/// flickering flashlight when you first pick it up
/// </summary>


using UnityEngine;
using System.Collections;

public class TogglePlayerLightScript : MonoBehaviour {

	public GameObject flashlight;
	public bool IsPickupToggle;

	void OnTriggerEnter(Collider col){
		if (!IsPickupToggle) {
			if (col.gameObject.tag == "Player") {
				StartCoroutine (ToggleFlashlight ());
			}
		}
	}


	public IEnumerator ToggleFlashlight(){
		Debug.Log ("Light toggled");
		flashlight.SetActive(!flashlight.activeSelf);
		yield return new WaitForSeconds (0.5f);
		flashlight.SetActive(!flashlight.activeSelf);
		yield return new WaitForSeconds (0.4f);
		flashlight.SetActive(!flashlight.activeSelf);
		yield return new WaitForSeconds (0.3f);
		flashlight.SetActive(!flashlight.activeSelf);
		yield return new WaitForSeconds (0.2f);
		flashlight.SetActive(!flashlight.activeSelf);
		yield return new WaitForSeconds (0.1f);
		flashlight.SetActive(!flashlight.activeSelf);
		yield return new WaitForSeconds(0.1f);
		flashlight.SetActive(!flashlight.activeSelf);
		gameObject.SetActive (false);
	}
}
