using UnityEngine;
using System.Collections;

public class SetActiveTrigger : MonoBehaviour {
	
	public GameObject ObjectToTrigger;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			ObjectToTrigger.SetActive (true);
			gameObject.SetActive(false);
		}
	}


}

