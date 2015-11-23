using UnityEngine;
using System.Collections;

public class FlashlightHitArea : MonoBehaviour {

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
		if (Physics.Raycast (transform.position, transform.forward, out hitMiddle)){
			hitMiddlePosition = hitMiddle.transform.position;
		}
		if (Physics.Raycast (transform.position, transform.forward + Quaternion.AngleAxis(-angle, transform.up) * transform.forward, out hitDown)){
			hitDownPosition = hitDown.transform.position;
		}
		if (Physics.Raycast (transform.position, transform.forward + Quaternion.AngleAxis(angle,transform.up) * transform.forward, out hitUp)){
			hitUpPosition = hitUp.transform.position;
		}
		if (Physics.Raycast (transform.position, transform.forward + Quaternion.AngleAxis (angle,transform.right) * transform.forward, out hitRight)){
			hitRightPosition = hitRight.transform.position;
		}
		if (Physics.Raycast (transform.position, transform.forward + Quaternion.AngleAxis (-angle, transform.right) * transform.forward, out hitLeft)){
			hitLeftPosition = hitLeft.transform.position;
		}
	}
}
