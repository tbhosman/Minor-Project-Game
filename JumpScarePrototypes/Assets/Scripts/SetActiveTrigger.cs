using UnityEngine;
using System.Collections;

public class SetActiveTrigger : MonoBehaviour {

	public GameObject ObjectToTrigger;
	// Use this for initialization
	void Start () {
		ObjectToTrigger.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		ObjectToTrigger.SetActive (true);
	}


}

