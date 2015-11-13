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
	private Rigidbody rb;
	public int cacheSize;
	public int[] waypointcache;
	private int reachindex;
	private int waypoint_index;

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
		rb.velocity = transform.TransformDirection(new Vector3(0,0,speed));
		rb.transform.LookAt (waypoint.transform.position + new Vector3(0,0.1f,0));

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

			for (int i = 0; i < cacheSize-1; i++)
			{
				waypointcache[i] = waypointcache[i + 1]; //Shift all positions by one
			}
			waypointcache[cacheSize-1] = waypoint_index; //Add previous waypoint to cache

			waypoint_index = newWaypoint ();

			//set as new waypoint
			waypoint = waypoints[newWaypoint()];
		}

		rb.velocity = transform.TransformDirection(new Vector3(0,0,speed));
		rb.transform.LookAt (waypoint.transform.position + new Vector3(0,0.1f,0));

	}

	protected bool Reachable(Vector3 location){
		RaycastHit hit;
		Vector3 rayDirection = location - transform.position;

		if (Physics.Raycast(transform.position, rayDirection, out hit))
		{
			return (hit.transform.CompareTag("Waypoint"));
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

}
