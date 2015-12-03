using UnityEngine;
using System.Collections;

public class StickToPlayerScript : MonoBehaviour {

	private GameObject Player;

	void Start(){
		Player = GameObject.FindGameObjectWithTag ("Player");
	}
	void Update () {
		gameObject.transform.position = Player.transform.position;
	}
}
