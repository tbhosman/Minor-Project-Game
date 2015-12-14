using UnityEngine;
using System.Collections;

public class SendGameCompleted : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Dit gaat goed");
		GameObject.Find("DataAquisitie").GetComponent<DataAquisitie>().CompletedGame();
	}
}
