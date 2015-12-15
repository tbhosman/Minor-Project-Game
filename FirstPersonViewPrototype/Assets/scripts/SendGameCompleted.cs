using UnityEngine;
using System.Collections;

public class SendGameCompleted : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find("DataAquisitie").GetComponent<DataAquisitie>().CompletedGame();
	}
}
