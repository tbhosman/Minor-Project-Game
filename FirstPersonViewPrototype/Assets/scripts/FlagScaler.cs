using UnityEngine;
using System.Collections;

public class FlagScaler : MonoBehaviour {

	public float AspectRatio;
	public float scaleFactor;
	public float camPosY;

	// Use this for initialization
	void Start () {
		AspectRatio = ((float)Screen.width)/((float)Screen.height);
		scaleFactor = (AspectRatio - 5.0f / 4.0f) * 0.3f / (16.0f / 9.0f - 5.0f / 4.0f) + 0.7f;
		camPosY = (AspectRatio - 5.0f / 4.0f) * 8.0f / (16.0f / 9.0f - 5.0f / 4.0f) - 3.0f;
		Vector3 flagScale = GameObject.Find("vlagGoed").transform.localScale;
		GameObject.Find ("vlagGoed").transform.localScale = flagScale * scaleFactor;
		Vector3 camPos = GameObject.Find ("Camera").transform.position;
		camPos.y = camPosY;
		GameObject.Find ("Camera").transform.position = camPos;
		if (AspectRatio < 1.6f) {
			gameObject.GetComponentInChildren<Cloth> ().enabled = false;
		}
	}
}
