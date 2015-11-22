using UnityEngine;
using System.Collections;

public class FlashlightHitArea : MonoBehaviour {

	public float visibilityDistance = 100;
	public RaycastHit hitMiddle;
	public Vector3 hitMiddlePosition;
	public RaycastHit hitUp;
	public Vector3 hitUpPosition;
	public RaycastHit hitDown;
	public Vector3 hitDownPosition;
	public RaycastHit hitLeft;
	public Vector3 hitLeftPosition;
	public RaycastHit hitRight;
	public Vector3 hitRightPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float angle = GetComponent<Light>().spotAngle;
		if (Physics.Raycast (transform.position, transform.rotation.eulerAngles, out hitMiddle, visibilityDistance)){
			hitMiddlePosition = hitMiddle.transform.position;
			Debug.DrawLine(transform.position,hitMiddlePosition);
		}
		if (Physics.Raycast (transform.position, transform.rotation.eulerAngles + new Vector3 (angle, 0, 0), out hitDown, visibilityDistance)){
			hitDownPosition = hitDown.transform.position;
			Debug.DrawLine(transform.position,hitDownPosition);
		}
		if (Physics.Raycast (transform.position, transform.rotation.eulerAngles + new Vector3 (-angle, 0, 0), out hitUp, visibilityDistance)) {
			hitUpPosition = hitUp.transform.position;
			Debug.DrawLine(transform.position,hitUpPosition);
		}
		if (Physics.Raycast (transform.position, transform.rotation.eulerAngles + new Vector3 (0, angle, 0), out hitRight, visibilityDistance)) {
			hitRightPosition = hitRight.transform.position;
			Debug.DrawLine(transform.position,hitRightPosition);
		}
		if (Physics.Raycast (transform.position, transform.rotation.eulerAngles + new Vector3 (0, -angle, 0), out hitLeft, visibilityDistance)) {
			hitLeftPosition = hitLeft.transform.position;
			Debug.DrawLine(transform.position,hitLeftPosition);
		}
	}
}
