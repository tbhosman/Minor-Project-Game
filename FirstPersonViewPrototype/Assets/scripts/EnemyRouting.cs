using UnityEngine;
using System.Collections;

public class EnemyRouting : MonoBehaviour {

	public GameObject[] waypoints;
	private GameObject waypoints_parent;
	public bool[] canReach;
	public ArrayList Reachables;
	public ArrayList newReachables;
	public GameObject waypoint;
	public float reachDist;
	public float speed;
	public Rigidbody rb;
	public int cacheSize;
	public int[] waypointcache;
	private int reachindex;
	private int waypoint_index;
	public GameObject enemyObject;
	public float CapsuleCastErrorDistance;
	public float angleError;
	public float turnSpeed;
	public float rampUpDuration;
	public bool wantIdle;
	public bool wantWalk;
	public bool isAnimIdle;
	public bool isAnimWalk;
	public float CapsuleCastRangeCorrection;

	// Use this for initialization
	void Start () {
		Reachables = new ArrayList();
		waypoints_parent = GameObject.Find ("Waypoints");
		waypoints = new GameObject[waypoints_parent.transform.childCount];
		canReach = new bool[waypoints_parent.transform.childCount];
		for (int i = 0; i < waypoints_parent.transform.childCount; i++)
		{
			waypoints[i] = waypoints_parent.transform.GetChild(i).gameObject;
			canReach[i] = Reachable(waypoints[i].transform.position);
			if (canReach[i] == true){
				Reachables.Add(waypoints[i]);
			}
		}
		waypoint = waypoints[newWaypoint ()];
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.TransformDirection(new Vector3(0,0,0));
		//rb.transform.LookAt (waypoint.transform.position + new Vector3(0,0.1f,0));

		waypointcache = new int[cacheSize];
		for (int i=0; i<cacheSize; i++) {
			waypointcache[i] = -1;
		}
	}
	
	// Update is called once per frame
	void Update () {

		//check which waypoints can be reached
		for (int i = 0; i < waypoints.Length; i++)
		{
			canReach[i] = Reachable(waypoints[i].transform.position);
		}

		//if a new waypoint is needed (enemy is close to current waypoint)
		if (Vector3.Distance (transform.position, waypoint.transform.position) < reachDist) {

			wantWalk = false;

			for (int i = 0; i < cacheSize-1; i++)
			{
				waypointcache[i] = waypointcache[i + 1]; //Shift all positions by one
			}
			waypointcache[cacheSize-1] = waypoint_index; //Add previous waypoint to cache

			waypoint_index = newWaypoint ();

			//set as new waypoint
			waypoint = waypoints[newWaypoint()];
			wantIdle = true;
			//rb.velocity = transform.TransformDirection(new Vector3(0,0,0));//StartCoroutine(RampSpeed(speed,0)); //set speed to 0 for turning to next waypoint
		}

		if (rb.velocity == new Vector3 (0, 0, 0)) { //turning to new waypoint
			wantIdle = false;
			Vector3 newRotation = Quaternion.LookRotation(waypoint.transform.position - transform.position).eulerAngles;
			newRotation.x = 0.0f;
			newRotation.z = 0.0f;
			if (Mathf.Abs((float) transform.rotation.eulerAngles.y - newRotation.y) < angleError ){ //pointing towards new waypoint
				wantWalk = true;
				//StartCoroutine(RampSpeed(0,speed));//rb.velocity = transform.TransformDirection(new Vector3(0,0, StartCoroutine(RampSpeed(0,speed))));
			}
			else{
				rb.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newRotation), Time.deltaTime * turnSpeed);
			}
		}
		else if (wantWalk == true){ //moving to a waypoint
			rb.velocity = transform.TransformDirection(new Vector3(0,0,speed));
			rb.transform.LookAt (waypoint.transform.position + new Vector3(0,0.1f,0));
		}

		if (isAnimIdle == true){
			rb.velocity = transform.TransformDirection(new Vector3(0,0,0));
		}
		else if (isAnimWalk == true){
			rb.velocity = transform.TransformDirection(new Vector3(0,0,speed));
			//StartCoroutine(RampSpeed(0,speed));
		}

	}

	protected bool Reachable(Vector3 location){
		RaycastHit hit;
		Vector3 rayDirection = location - transform.position;
		Vector3 p1 = transform.position + Vector3.up * -enemyObject.transform.lossyScale.y * 0.5F;
		Vector3 p2 = p1 + Vector3.up * enemyObject.transform.lossyScale.y;

		if (Physics.CapsuleCast(p1, p2, CapsuleCastRangeCorrection*enemyObject.transform.lossyScale.x/2, rayDirection, out hit))
		{
			return (hit.transform.CompareTag("Waypoint") && (Vector3.Distance(hit.transform.position,location) < CapsuleCastErrorDistance));
		}
		return false;
	}

	int newWaypoint(){
		Reachables = new ArrayList();
		newReachables = new ArrayList();
		for (int i = 0; i < waypoints_parent.transform.childCount; i++)
		{
			if (canReach[i] == true){
				Reachables.Add(i);
				if (PositionNotCached(i)){
					newReachables.Add(i);
				}
			}
		}

		//if possible, choose waypoint not in cache
		if (newReachables.Count == 0) {
			reachindex = (int)Reachables [Random.Range (0, Reachables.Count - 1)];
		} else {
			reachindex = (int)newReachables [Random.Range (0, newReachables.Count - 1)];
		}

		return reachindex;
	}

	bool PositionNotCached(int x){

		foreach (int i in waypointcache){
			if (x == i){
				return false;
			}
		}
		return true;
	}

	IEnumerator RampSpeed(float startSpeed, float stopSpeed){
		float t = 0;
		while (t<rampUpDuration) {
			t += Time.deltaTime;
			rb.velocity = transform.TransformDirection(new Vector3(0,0, Mathf.Lerp(startSpeed,stopSpeed,t/rampUpDuration)));
			yield return null;
		}
	}

}
