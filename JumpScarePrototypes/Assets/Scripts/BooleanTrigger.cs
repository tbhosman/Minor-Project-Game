using UnityEngine;
using System.Collections;

public class BooleanTrigger : MonoBehaviour {


	public GameObject ObjectToTrigger;
	public string Scriptname;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			ObjectToTrigger.GetComponent<TriggerObjectScript>().Trigger = true;
			gameObject.SetActive(false);
		}
	}
}
