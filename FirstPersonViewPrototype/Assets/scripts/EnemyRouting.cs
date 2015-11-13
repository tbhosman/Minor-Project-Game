using UnityEngine;
using System.Collections;

public class EnemyRouting : MonoBehaviour {

	public GameObject[] waypoints;
	private GameObject waypoints_parent;
	public bool[] canReach;

	// Use this for initialization
	void Start () {
		waypoints_parent = GameObject.Find ("Waypoints");
		waypoints = new GameObject[waypoints_parent.transform.childCount];
		canReach = new bool[waypoints_parent.transform.childCount];
		for (int i = 0; i < waypoints_parent.transform.childCount; i++)
		{
			waypoints[i] = waypoints_parent.transform.GetChild(i).gameObject;
			canReach[i] = Reachable(waypoints[i].transform.position);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < waypoints_parent.transform.childCount; i++)
		{
			canReach[i] = Reachable(waypoints[i].transform.position);
		}
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
}
