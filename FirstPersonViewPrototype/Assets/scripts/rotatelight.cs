/// <summary>
/// Rotates the alarm light in the reactor
/// </summary>

using UnityEngine;
using System.Collections;

public class rotatelight : MonoBehaviour {
    private int rotateValue;
	// Use this for initialization
	void Start () {
        rotateValue = 1;
    }
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(90,rotateValue*4,0);
        rotateValue++;
    }
}
