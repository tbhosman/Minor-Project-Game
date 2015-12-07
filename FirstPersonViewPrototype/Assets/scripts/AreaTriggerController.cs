using UnityEngine;
using System.Collections;

public class AreaTriggerController : MonoBehaviour {
	
	void OnTriggerEnter(Collider other){

		if (other.CompareTag("Player")){

			if (transform.name.StartsWith("StorageTrigger")){
				transform.parent.GetComponent<MainAreaTriggerController>().InArea("Storage");
			}
			else if (transform.name.StartsWith("OfficeTrigger")){
				transform.parent.GetComponent<MainAreaTriggerController>().InArea("Office");
			}
			else if (transform.name.StartsWith("ArchiveTrigger")){
				transform.parent.GetComponent<MainAreaTriggerController>().InArea("Archive");
			}
			else if (transform.name.StartsWith("MachineTrigger")){
				transform.parent.GetComponent<MainAreaTriggerController>().InArea("Machine");
			}
			else if (transform.name.StartsWith("LabTrigger")){
				transform.parent.GetComponent<MainAreaTriggerController>().InArea("Lab");
			}
			else if (transform.name.StartsWith("ReaktorTrigger")){
				transform.parent.GetComponent<MainAreaTriggerController>().InArea("Reaktor");
			}

		}
	}
}
