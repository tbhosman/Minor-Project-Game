using UnityEngine;
using System.Collections;

public class raycastplayer : MonoBehaviour {
	private Ray playerforwardray;
	private RaycastHit rayhit;
	public Vector3 rayhitposition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		playerforwardray = new Ray (transform.position, transform.forward);
		if (Physics.Raycast (playerforwardray, out rayhit)) {
			rayhitposition = rayhit.point;
		}

	
	}
}
