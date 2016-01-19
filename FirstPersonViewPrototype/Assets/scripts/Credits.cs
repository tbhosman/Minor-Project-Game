/// <summary>
/// Controller for moving the credits text
/// </summary>

using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public GameObject camera;
	public float speed = 1;
	public string level;

	void Start(){

		if (GameObject.Find ("MainMusicController"))
			Destroy (GameObject.Find ("MainMusicController"));

		if (GameObject.Find ("MainMusicController(Clone)"))
			Destroy (GameObject.Find ("MainMusicController(Clone)"));
		//StartCoroutine(waitForEndOfCredits ());
	}

	// Update is called once per frame
	void Update () {

		camera.transform.Translate (Vector3.down * Time.deltaTime * speed);
		if (GameObject.Find ("vlagGoed").transform.FindChild ("Canvas").localPosition.z < -1.9f) {
			Application.LoadLevel ("menu");
		}
	}
}
