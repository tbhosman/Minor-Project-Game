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
		float angle = transform.GetComponent<Light>().spotAngle/2;
		if (Physics.Raycast (transform.position, transform.forward, out hitMiddle)){
			hitMiddlePosition = hitMiddle.point;
			//Debug.DrawRay(transform.position, transform.forward);
		}
		if (Physics.Raycast (transform.position, Quaternion.AngleAxis(-angle, transform.up) * transform.forward, out hitDown)){
			hitDownPosition = hitDown.point;
			//Debug.DrawRay(transform.position, Quaternion.AngleAxis(-angle, transform.up) * transform.forward);
		}
		if (Physics.Raycast (transform.position, transform.forward + Quaternion.AngleAxis(angle,transform.up) * transform.forward, out hitUp)){
			hitUpPosition = hitUp.point;
			//Debug.DrawRay(transform.position, Quaternion.AngleAxis(angle, transform.up) * transform.forward);
		}
		if (Physics.Raycast (transform.position, transform.forward + Quaternion.AngleAxis (angle,transform.right) * transform.forward, out hitRight)){
			hitRightPosition = hitRight.point;
			//Debug.DrawRay(transform.position, Quaternion.AngleAxis(angle, transform.right) * transform.forward);
		}
		if (Physics.Raycast (transform.position, transform.forward + Quaternion.AngleAxis (-angle, transform.right) * transform.forward, out hitLeft)){
			hitLeftPosition = hitLeft.point;
			//Debug.DrawRay(transform.position, Quaternion.AngleAxis(-angle, transform.right) * transform.forward);
		}
	}
}
