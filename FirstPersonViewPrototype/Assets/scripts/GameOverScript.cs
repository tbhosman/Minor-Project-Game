using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class GameOverScript : MonoBehaviour {

	public float GameOverDistance;
	public float DyingDistance;
	private GameObject Player;
	private GameObject Enemy;
	private GameObject ToWhitePanel;
	private GameObject NoiseObject;
	public float PEDistance; //Player-Enemy distance
	public float alpha_value;
	public float maxVolume;

	void Start() {
		Player = GameObject.Find ("FPSController");
		Enemy = GameObject.Find ("Enemy");
		ToWhitePanel = GameObject.Find ("ToWhiteForGameOver");
		NoiseObject = GameObject.Find ("FirstPersonCharacter");
	}

	// Update is called once per frame
	void Update () {
	
		PEDistance = Vector3.Distance (Player.transform.position, Enemy.transform.position);


		if (PEDistance < GameOverDistance) {
			alpha_value = 1;
			GetComponent<AudioSource>().volume = maxVolume;
			NoiseObject.GetComponent<NoiseAndGrain>().intensityMultiplier = 2;
			if (canReachPlayer()){
            	Application.LoadLevel("menu");
			}
		} else if (PEDistance < DyingDistance) { //player is close to enemy but not GameOver, interpolate alpha
			alpha_value = -1 / (DyingDistance - GameOverDistance) * PEDistance + DyingDistance / (DyingDistance - GameOverDistance);
			GetComponent<AudioSource>().volume = maxVolume * alpha_value;
			NoiseObject.GetComponent<NoiseAndGrain>().intensityMultiplier = alpha_value * 2;
		} else {
			alpha_value = 0;
			GetComponent<AudioSource>().volume = 0;
			NoiseObject.GetComponent<NoiseAndGrain>().intensityMultiplier = 0;
		}

		ToWhitePanel.GetComponent<Image> ().color = new Color(255,255,255,alpha_value);
	}

	bool canReachPlayer(){
		RaycastHit hit;
		Vector3 pos = GameObject.Find ("Enemy").transform.position;
		Vector3 rayDirection = transform.position - pos;
		
		if (Physics.Raycast(pos,rayDirection, out hit)){
			return hit.transform.CompareTag("Player");
		}
		return false;
	}
}