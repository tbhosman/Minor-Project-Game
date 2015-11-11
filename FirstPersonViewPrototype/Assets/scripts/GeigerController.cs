using UnityEngine;
using System.Collections;

public class GeigerController : MonoBehaviour {
	
	private float playerDistance;

	public AudioClip GeigerLevel1;
	public AudioClip GeigerLevel2;
	public AudioClip GeigerLevel3;
	public AudioClip GeigerLevel4;

	private bool canPlayAudio = true;
	public float GeigerParameter; // Use this for initialization
	void Start () {
	
		GeigerLevel1 = null;
		GeigerLevel2 = null;
		GeigerLevel3 = null;
		GeigerLevel4 = null;
	}

	void Update () {
	
		float radiodistance = DistanceToClosestRadioactive ();
		print(PlayAudio (radiodistance));
	}

	float DistanceToClosestRadioactive(){
		GameObject[] radioactives;
		radioactives =  GameObject.FindGameObjectsWithTag("radioactive");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject radioactive in radioactives) {
			float curdistance = Vector3.Distance(radioactive.transform.position,transform.position);
			if (curdistance<distance){
				distance = curdistance;
			}
		}
		return distance;
	}

	int PlayAudio(float radiodistance){
		int audiolevel = 0;

		if (radiodistance > 20) {
			audiolevel = 1;
		}
		if (radiodistance < 20 && radiodistance > 12) {
			audiolevel = 2;
		}
		if (radiodistance < 12 && radiodistance > 8) {
			audiolevel = 3;
		}
		if (radiodistance < 8) {
			audiolevel = 4;
		}

		return audiolevel;
	}
}
