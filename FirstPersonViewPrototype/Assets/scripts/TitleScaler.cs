/// <summary>
/// Scales the title image according to the aspect ratio of the screen
/// </summary>

using UnityEngine;
using System.Collections;

public class TitleScaler : MonoBehaviour {

	public float scalerError;

	// Use this for initialization
	void Start () {
		float AR = (float) Screen.width / (float) Screen.height;
		transform.localScale = new Vector3(9 * AR, 1, (27.0f/16.0f) * AR);
	}
}
