using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public GameObject enemyObject;
	public float CapsuleCastErrorDistance;

	// Use this for initialization
	void Start () {
	
		Graph map = new Graph ();

		for (int a = 0; a < transform.childCount; a++) {
			map.add_vertex(a, new Dictionary<int, float>());
		}

		for (int i = 0; i < transform.childCount; i++) {

			for (int j = i; j < transform.childCount; j++){
				if (!(i == j)){
					Vector3 p1 = transform.GetChild(i).gameObject.transform.position; 
					Vector3 p2 = transform.GetChild(j).gameObject.transform.position;
					if (ReachableFromTo(p1, p2)){ //if there is a connection possible between the two waypoints
						map.add_edge(i, j, Vector3.Distance(p1,p2)); //
						map.add_edge(j, i, Vector3.Distance(p1,p2));
					}
				}
			}
		}

		List<int> ans = map.shortest_path (0, 5); //test calculation
	}
	
	// Update is called once per frame
	void Update () {
	}

	protected bool ReachableFromTo(Vector3 location1, Vector3 location2){
		RaycastHit hit;
		Vector3 rayDirection = location2 - location1;
		Vector3 p1 = location1 + Vector3.up * -enemyObject.transform.localScale.y * 0.5F;
		Vector3 p2 = p1 + Vector3.up * enemyObject.transform.localScale.y;
		
		if (Physics.CapsuleCast(p1, p2, enemyObject.transform.localScale.x/2, rayDirection, out hit)) //cast a rough enemy size to other waypoint
		{
			return ((hit.transform.CompareTag("Waypoint")) && (Vector3.Distance(hit.transform.position,location2) < CapsuleCastErrorDistance));
		}
		return false;
	}

	class Graph
	{
		Dictionary<int, Dictionary<int, float>> vertices = new Dictionary<int, Dictionary<int, float>>();
		
		public void add_vertex(int name, Dictionary<int, float> edges) //add a waypoint to the map
		{
			vertices[name] = edges;
		}		

		public void add_edge(int name, int neighbor, float distance) //add a connection between waypoints
		{
			this.vertices [name].Add(neighbor,distance);
		}
		
		public List<int> shortest_path(int start, int finish)
		{
			var previous = new Dictionary<int, int>();
			var distances = new Dictionary<int, float>();
			var nodes = new List<int>();
			
			List<int> path = null;
			
			foreach (var vertex in vertices) //initialize distances
			{
				if (vertex.Key == start)
				{
					distances[vertex.Key] = 0;
				}
				else
				{
					distances[vertex.Key] = float.MaxValue;
				}
				
				nodes.Add(vertex.Key);
			}

			while (nodes.Count != 0) //go through all waypoints
			{
				nodes.Sort((x, y) => distances[x] < distances[y] ? -1 : (distances[x] > distances[y] ? 1 : 0)); //search for shortest distance

				var smallest = nodes[0];
				nodes.Remove(smallest);
				
				if (smallest == finish) //check if next waypoint is the finish
				{
					path = new List<int>();
					while (previous.ContainsKey(smallest))
					{
						path.Add(smallest);
						smallest = previous[smallest];
					}
					
					break;
				}
				
				if (distances[smallest] == float.MaxValue) //check if there are any connections left
				{
					break;
				}
				
				foreach (var neighbor in vertices[smallest]) //loop through all neighbors
				{
					var alt = distances[smallest] + neighbor.Value;
					if (alt < distances[neighbor.Key]) //if alternative is smaller than current record: replace
					{
						distances[neighbor.Key] = alt;
						previous[neighbor.Key] = smallest;
					}
				}
			}
			
			return path;
		}
	}
}