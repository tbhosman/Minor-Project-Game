/// <summary>
/// Keeps the DontLookMonster in same relative position of player
/// </summary>

using UnityEngine;
using System.Collections;

public class DontLookMonster : MonoBehaviour {

	private GameObject Player;
	public Vector3 RelativeToPlayerPos;
	public GameObject[] ObjectsToDeactivate;
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		gameObject.transform.position = Player.transform.position + RelativeToPlayerPos;
	}

	//This is an Event in DontLookScareAnimation
	void Deactivate(){
		gameObject.SetActive (false);
	}

	void DeactivateObjects(){
		foreach (GameObject ObjectToDeactivate in ObjectsToDeactivate) {
			ObjectToDeactivate.SetActive (false);
		}
	}
}
