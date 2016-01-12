using UnityEngine;
using System.Collections;

public class ventilationSmoke : MonoBehaviour {
    public ParticleSystem ventilatie;

	// Use this for initialization
	void Start () {
        Rook();
        InvokeRepeating("Rook", 3, 3);
    }
	
	// Update is called once per frame
	void Rook () {
        if (Random.Range(1, 5) <= 1)
        {
            ventilatie.Play();
        }
    }
}
