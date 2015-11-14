using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public GameObject enemyObject;
	public float CapsuleCastErrorDistance;
	public ArrayList[] nodes; //Array of all nodes (nodes are ArrayLists)

	// Use this for initialization
	void Start () {
	
		nodes = new ArrayList[transform.childCount];

		for (int i = 0; i < transform.childCount; i++) {
			nodes[i] = new ArrayList(); //set an empty ArrayList for all nodes
		}

		for (int i = 0; i < transform.childCount; i++) {
			for (int j = i; j < transform.childCount; j++){
				if (!(i == j)){
					Vector3 p1 = transform.GetChild(i).gameObject.transform.position; 
					Vector3 p2 = transform.GetChild(j).gameObject.transform.position;
					if (ReachableFromTo(p1, p2)){
						nodes[i].Add(j);
						nodes[j].Add(i);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected bool ReachableFromTo(Vector3 location1, Vector3 location2){
		RaycastHit hit;
		Vector3 rayDirection = location1 - location2;
		Vector3 p1 = location1 + Vector3.up * -enemyObject.transform.localScale.y * 0.5F;
		Vector3 p2 = p1 + Vector3.up * enemyObject.transform.localScale.y;
		
		if (Physics.CapsuleCast(p1, p2, enemyObject.transform.localScale.x/2, rayDirection, out hit))
		{
			return ((hit.transform.CompareTag("Waypoint")) && (Vector3.Distance(hit.transform.position,location2) < CapsuleCastErrorDistance));
		}
		return false;
	}
}
