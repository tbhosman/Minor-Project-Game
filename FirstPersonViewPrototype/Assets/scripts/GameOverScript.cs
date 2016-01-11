/// <summary>
/// Script that controls the game over sequence. Makes the screen more white when the enemy is close. Also scales the volume of
/// the dying sound and adds more grain when getting closer. Starts the game over scene if enemy is too close to the player.
/// </summary>

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
	
		//check what distance there is between player and enemy
		PEDistance = Vector3.Distance (Player.transform.position, Enemy.transform.position);

		if (PEDistance < GameOverDistance) { //if player is within the range of losing

			alpha_value = 1;
			GetComponent<AudioSource>().volume = maxVolume;
			NoiseObject.GetComponent<NoiseAndGrain>().intensityMultiplier = 2;
			if (canReachPlayer()){ //if there is no wall between the player and the enemy, the player dies
				GameObject.Find("DataAquisitie").GetComponent<DataAquisitie>().GameOver(transform.position);
            	Application.LoadLevel("GameOverScene");
			}

		} else if (PEDistance < DyingDistance) { //player is close to enemy but not GameOver: interpolate alpha, sound and noise

			alpha_value = -1 / (DyingDistance - GameOverDistance) * PEDistance + DyingDistance / (DyingDistance - GameOverDistance);
			GetComponent<AudioSource>().volume = maxVolume * alpha_value;
			NoiseObject.GetComponent<NoiseAndGrain>().intensityMultiplier = alpha_value * 2;

		} else { //player is not close to enemy

			alpha_value = 0;
			GetComponent<AudioSource>().volume = 0;
			NoiseObject.GetComponent<NoiseAndGrain>().intensityMultiplier = 0;

		}

		ToWhitePanel.GetComponent<Image> ().color = new Color(255,255,255,alpha_value);
	}

	//check if enemy can reach player
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