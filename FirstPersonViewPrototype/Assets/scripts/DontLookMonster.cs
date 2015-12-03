using UnityEngine;
using System.Collections;

public class DontLookMonster : MonoBehaviour {

	private GameObject Player;
	public Vector3 RelativeToPlayerPos;

	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		gameObject.transform.position = Player.transform.position + RelativeToPlayerPos;
	}

	void Deactivate(){
		gameObject.SetActive (false);
	}
}
