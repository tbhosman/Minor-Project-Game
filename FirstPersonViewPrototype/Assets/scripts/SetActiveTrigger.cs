/// <summary>
/// Set the trigger on this game object to active
/// </summary>

using UnityEngine;
using System.Collections;

public class SetActiveTrigger : MonoBehaviour {
	
	public GameObject ObjectToTrigger;
	public bool activebool;


	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			ObjectToTrigger.SetActive (activebool);
			gameObject.SetActive(false);
		}
	}


}

