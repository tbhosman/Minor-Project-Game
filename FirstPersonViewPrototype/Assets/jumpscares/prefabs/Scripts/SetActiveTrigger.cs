using UnityEngine;
using System.Collections;

public class SetActiveTrigger : MonoBehaviour {
	
	public GameObject ObjectToTrigger;
	public bool activebool;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			ObjectToTrigger.SetActive (activebool);
			gameObject.SetActive(false);
		}
	}


}

