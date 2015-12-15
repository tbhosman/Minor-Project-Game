using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public GameObject camera;
	public float speed = 1;
	public string level;

	void Start(){
		Destroy (GameObject.Find ("MainMusicController"));
		//StartCoroutine(waitForEndOfCredits ());
	}

	// Update is called once per frame
	void Update () {

		camera.transform.Translate (Vector3.down * Time.deltaTime * speed);
		Debug.Log (GameObject.Find ("vlagGoed").transform.FindChild ("Canvas").localPosition.z);
		if (GameObject.Find ("vlagGoed").transform.FindChild ("Canvas").localPosition.z < -1.188f) {
			Application.LoadLevel ("menu");
		}
	}
}
