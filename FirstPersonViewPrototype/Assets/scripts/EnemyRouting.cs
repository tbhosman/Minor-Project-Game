using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
	public float animStopDist;
	public Vector3 lastPlayerLocation;
	public int waypointToPlayer;
	public List<int> RouteToPlayer;
	public GameObject waypointObject;
	public float reachedLastPlayerLocationDistance = 1;
	public bool goingToPlayer;
	public bool wantTurn;
	private float prevDist;

	// Use this for initialization
	void Start () {
		RouteToPlayer = new List<int>();
		//wantIdle = false;
		//wantWalk = false;
		//isAnimIdle = false;
		//isAnimWalk = false;
		Reachables = new ArrayList();
		waypoints_parent = GameObject.Find ("Waypoints");
		waypoints = new GameObject[waypoints_parent.transform.childCount];
		canReach = new bool[waypoints_parent.transform.childCount];
		for (int i = 0; i < waypoints_parent.transform.childCount; i++)
		{
			waypoints[i] = waypoints_parent.transform.GetChild(i).gameObject;
			canReach[i] = Reachable(waypoints[i].transform.position);
			//waypoints[i].name = i.ToString();
			if (canReach[i] == true){
				Reachables.Add(waypoints[i]);
			}
		}
		waypoint_index = newWaypoint ();
		waypoint = waypoints[waypoint_index];
		wantTurn = true;
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.TransformDirection(new Vector3(0,0,0));
		//rb.transform.LookAt (waypoint.transform.position + new Vector3(0,0.1f,0));

		waypointcache = new int[cacheSize];
		for (int i=0; i<cacheSize; i++) {
			waypointcache[i] = -1;
		}
		waypointToPlayer = findWaypointToPlayer();
		waypoint_index = -1;

		prevDist = Vector3.Distance (transform.position, waypoint.transform.position);
	}
	
	// Update is called once per frame
	void Update () {

		//check what animation is running and change speed accordingly
		if (isAnimIdle == true){
			rb.velocity = transform.TransformDirection(new Vector3(0,0,0));
		}
		else if (isAnimWalk == true){
			rb.velocity = transform.TransformDirection(new Vector3(0,0,speed));
			//StartCoroutine(RampSpeed(0,speed));
		}

		if (goingToPlayer && RouteToPlayer.Count == 0) {
			//wantWalk = true;
			//wantIdle = false;
			//rb.velocity = transform.TransformDirection(new Vector3(0,0,speed));
			//if enemy can walk to player directly, go there
//			if (enemyCanReachPlayer()){
//				TurnTowards(GameObject.Find("FPSController").gameObject.transform.position);
//				if (Mathf.Abs(Vector3.Distance(transform.position,GameObject.Find("FPSController").gameObject.transform.position)) < reachedLastPlayerLocationDistance){
//					goingToPlayer = false;
//					//getNewWaypoint();
//				}
//				return;
//			}
			//if enemy cannot reach player, try last found location
			if(enemyCanReachLocation(lastPlayerLocation)){
				TurnTowards (lastPlayerLocation);
				//if last found location is reached, end search
				if (Mathf.Abs(Vector3.Distance(transform.position,lastPlayerLocation)) < reachedLastPlayerLocationDistance){
					goingToPlayer = false;
					//getNewWaypoint();
				}
				return;
			} else { //if player or last location cannot be reached, end search
				goingToPlayer = false;
				getNewWaypoint();
			}
		}

		//Check if enemy can see player
		if (GetComponent<EnemySight> ().seeingPlayer) {
			lastPlayerLocation = GameObject.Find("FPSController").gameObject.transform.position;
			lastPlayerLocation.y = transform.position.y;
			waypointToPlayer = findWaypointToPlayer();
			RouteToPlayer = GameObject.Find("Waypoints").GetComponent<MapGenerator>().map.shortest_path(waypoint_index,waypointToPlayer);
		}

		//Check if enemy can hear player
		if (GetComponent<EnemySight> ().hearingPlayer) {

			lastPlayerLocation = GameObject.Find("FPSController").gameObject.transform.position;
			lastPlayerLocation.y = transform.position.y;
			waypointToPlayer = findWaypointToPlayer();

			if (!GetComponent<EnemySight> ().canHearClearly){ //enemy can hear the player vaguely, only knows closest waypoint
				lastPlayerLocation = waypoints[waypointToPlayer].transform.position;
			}

			RouteToPlayer = GameObject.Find("Waypoints").GetComponent<MapGenerator>().map.shortest_path(waypoint_index,waypointToPlayer);
		}

		if (wantTurn) { //turning to new waypoint
			TurnTowards(waypoint.transform.position);
		}
		else if (isAnimWalk == true){ //moving to a waypoint
			rb.velocity = transform.TransformDirection(new Vector3(0,0,speed));
			//rb.transform.LookAt (waypoint.transform.position + new Vector3(0,0.1f,0));
		}

	}

	void LateUpdate(){
		if (Vector3.Distance (transform.position, waypoint.transform.position) < animStopDist) {
			
			wantWalk = false;

		}

		//if a new waypoint is needed (enemy is close to current waypoint)
		if (!wantWalk && !wantTurn){//Mathf.Abs(Vector3.Distance (transform.position, waypoint.transform.position)) < reachDist) {

			if (waypoint_index == waypointToPlayer){ //if final waypoint to player is reached, do not get a new waypoint
				goingToPlayer = true;
				return;
			}

			getNewWaypoint();
			//rb.velocity = transform.TransformDirection(new Vector3(0,0,0));//StartCoroutine(RampSpeed(speed,0)); //set speed to 0 for turning to next waypoint
		}

		//debug for enemy passing a waypoint
		if ((prevDist - Vector3.Distance (transform.position, waypoint.transform.position))/Time.deltaTime < -1.9f) { //enemy going further away
			getNewWaypoint();
		}
		prevDist = Vector3.Distance (transform.position, waypoint.transform.position);
	}

	public void getNewWaypoint(){
		//check which waypoints can be reached
		for (int i = 0; i < waypoints.Length; i++)
		{
			canReach[i] = Reachable(waypoints[i].transform.position);
		}
		
		for (int i = 0; i < cacheSize-1; i++) {
			waypointcache [i] = waypointcache [i + 1]; //Shift all positions by one
		}
		waypointcache [cacheSize - 1] = waypoint_index; //Add previous waypoint to cache
		
		waypoint_index = newWaypoint ();
		
		//set as new waypoint
		waypoint = waypoints [waypoint_index];
		prevDist = Vector3.Distance (transform.position, waypoint.transform.position);
		wantTurn = true;
	}

	void TurnTowards(Vector3 location){
		Vector3 newRotation = Quaternion.LookRotation(location - transform.position).eulerAngles;
		newRotation.x = 0.0f;
		newRotation.z = 0.0f;
		if (Mathf.Abs ((float)transform.rotation.eulerAngles.y - newRotation.y) < angleError) { //pointing towards new waypoint
			wantWalk = true;
			wantTurn = false;
			wantIdle = false;
			rb.transform.LookAt (location + new Vector3(0,0.1f,0));
			//StartCoroutine(RampSpeed(0,speed));//rb.velocity = transform.TransformDirection(new Vector3(0,0, StartCoroutine(RampSpeed(0,speed))));
		} else if (rb.velocity == new Vector3 (0, 0, 0)) {
			wantIdle = true;
			wantWalk = false;
			rb.transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (newRotation), Time.deltaTime * turnSpeed);
		} else {
			wantIdle = true;
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

	void OnCollisionStay(Collision collisionInfo) {
		if (!collisionInfo.transform.CompareTag ("Waypoint") && (rb.velocity.magnitude < 0.01f) && isAnimWalk){ //enemy is now stuck
			getNewWaypoint();
			transform.position = waypoint.transform.position;
		}
	}

	int newWaypoint(){
		if (RouteToPlayer.Count == 0) { //enemy is not following a route to the player

			Reachables = new ArrayList ();
			newReachables = new ArrayList ();
			for (int i = 0; i < waypoints_parent.transform.childCount; i++) {
				if (canReach [i] == true && waypoint_index != i) {
					Reachables.Add (i);
					if (PositionNotCached (i)) {
						newReachables.Add (i);
					}
				}
			}

			//if possible, choose waypoint not in cache
			if (newReachables.Count == 0) {
				reachindex = (int)Reachables [Reachables.Count-1]; //take oldest location
			} else {
				reachindex = (int)newReachables [Random.Range (0, newReachables.Count)];
			}

		} else { //enemy is following route to player
			reachindex = RouteToPlayer[RouteToPlayer.Count-1];
			RouteToPlayer.RemoveAt(RouteToPlayer.Count-1);
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

	int findWaypointToPlayer(){
		int ans = -2;
		float dist = float.MaxValue;
		for (int i = 0; i < waypoints_parent.transform.childCount; i++){
			float wp = Vector3.Distance(waypoints[i].gameObject.transform.position,lastPlayerLocation);
			if (ReachableWaypointToPlayer(i)){
				if (Mathf.Abs(wp) < dist){
					dist = Mathf.Abs(wp);
					ans = i;
				}
			}
		}
		return ans;
	}

	protected bool ReachableWaypointToPlayer(int waypointToReach){ //check if player is reachable from a waypoint
		RaycastHit hit;
		Vector3 pos = waypoints [waypointToReach].gameObject.transform.position;
		Vector3 rayDirection = lastPlayerLocation - pos;

		if (Physics.Raycast(pos,rayDirection, out hit)){
			return hit.transform.CompareTag("Player");
		}
		return false;
	}

	protected bool enemyCanReachLocation(Vector3 location){
		GameObject temp = GameObject.Instantiate (waypointObject);
		temp.transform.position = location; //set temp waypoint at location to go to
		RaycastHit hit;
		Vector3 rayDirection = location - transform.position;
		Vector3 p1 = transform.position + Vector3.up * -enemyObject.transform.lossyScale.y * 0.5F;
		Vector3 p2 = p1 + Vector3.up * enemyObject.transform.lossyScale.y;
		
		if (Physics.CapsuleCast(p1, p2, CapsuleCastRangeCorrection*enemyObject.transform.lossyScale.x/2, rayDirection, out hit))
		{
			bool ans = ((hit.transform.CompareTag("Waypoint")  && Vector3.Distance(hit.transform.position,location) < CapsuleCastErrorDistance) || hit.transform.CompareTag("Player"));
			Destroy (temp);
			return ans;
		}
		Destroy (temp);
		return false;
	}
}
