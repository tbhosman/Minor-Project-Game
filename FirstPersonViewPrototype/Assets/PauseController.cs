using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {
	
	public GameObject[] IgnorePanels = new GameObject[1];

	// Use this for initialization
	void Start () {
		//GameObject[] = new GameObject[ignoreNumber];
	}
	
	// Update is called once per frame
	void Update () {
		for (int i=0; i<IgnorePanels.Length; i++) {
			if (IgnorePanels[i].activeSelf){
				return; //pause cannot be opened
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {

		}

	}
}
