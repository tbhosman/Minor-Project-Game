using UnityEngine;
using System.Collections;

public class space_checker : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RaycastHit hit;
        RaycastHit hit2;
        RaycastHit hitZ;
        RaycastHit hitZ2;
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.3f, transform.position.z), Vector3.right, out hit);
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.3f, transform.position.z), Vector3.forward, out hitZ);
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.3f, transform.position.z), Vector3.left, out hit2);
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.3f, transform.position.z), Vector3.back, out hitZ2);
        Debug.Log("x_dimensies: " + hit.distance + " " + hit2.distance + "z_dimensies " + hitZ.distance + " " + hitZ2.distance);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
