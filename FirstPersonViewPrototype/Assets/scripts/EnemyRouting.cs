using UnityEngine;
using System.Collections;

public class EnemyRouting : MonoBehaviour {

	public GameObject[] waypoints;
	private GameObject waypoints_parent;
	public bool[] canReach;
	public ArrayList Reachables;
	public GameObject waypoint;
	public float reachDist;
	public float speed;
	private Rigidbody rb;

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
		rb.transform.LookAt (waypoint.transform.position);
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < waypoints_parent.transform.childCount; i++)
		{
			canReach[i] = Reachable(waypoints[i].transform.position);
		}

		if (Vector3.Distance (transform.position, waypoint.transform.position) < reachDist) {
			waypoint = waypoints[newWaypoint()];
		}

		rb.velocity = transform.TransformDirection(new Vector3(0,0,speed));
		rb.transform.LookAt (waypoint.transform.position);

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
		for (int i = 0; i < waypoints_parent.transform.childCount; i++)
		{
			if (canReach[i] == true){
				Reachables.Add(i);
			}
		}

		return (int) Reachables[Random.Range (0, Reachables.Capacity-1)];
	}
}
