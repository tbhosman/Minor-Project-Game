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
	public bool seeingEnemy;
	public AudioSource scareSoundOnSeeingEnemy;
	public AudioClip[] scareSounds = new AudioClip[3];

	// Use this for initialization
	void Start () {
		scareSoundOnSeeingEnemy = GetComponent<AudioSource> ();
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

		CheckEnemyInSight ();
	}

	void CheckEnemyInSight(){
		RaycastHit hit;
		Vector3 rayDirection = GameObject.Find ("Enemy").transform.position - transform.position + new Vector3(0,0.3f,0);

		seeingEnemy = false;

		if ((Vector3.Angle(rayDirection, transform.forward)) <= GetComponent<Light>().spotAngle * 0.5f)
		{
			// Detect if player is within the field of view
			if (Physics.Raycast(transform.position, rayDirection, out hit))
			{
				seeingEnemy = hit.transform.CompareTag("radioactive");
			}else{seeingEnemy = false;}
		}else{seeingEnemy = false;}


		if (seeingEnemy && !scareSoundOnSeeingEnemy.isPlaying) {
			float rand = Random.Range(0.0f,1.0f);
			if (rand < 1.0f/3.0f){
				scareSoundOnSeeingEnemy.PlayOneShot(scareSounds[0]);
			}
			else if (rand < 2.0f/3.0f){
				scareSoundOnSeeingEnemy.PlayOneShot(scareSounds[1]);			
			}
			else{
				scareSoundOnSeeingEnemy.PlayOneShot(scareSounds[2]);
			}
		}
	}
}
