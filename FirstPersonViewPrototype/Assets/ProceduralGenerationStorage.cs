using UnityEngine;
using System.Collections;

public class ProceduralGenerationStorage : MonoBehaviour {
    //height: 2.6; width(z-richting):2.02143111206, length:5.82130517219
    //room: x_dimensie: 6.41793+3.460644 = 9.878574, z_dimensie: 7.346245+2.637087= 9.983332
    //width:1.6638886666666666666666666666666666666666666666666666
    //length: 4.939287    
    //width-scale:0.8231241009103847317316067819395322830819746132816759
    //length-scale:0.8484844642050983599938696551126567129108748989919702
    // Use this for initialization
    public GameObject kast;

    private float rijZ;
    private float kolomZ;
    private int [,] arrayZ = new int[6, 2];
    private int [,] arrayX = new int[6, 2];
    void Start() {
        rijZ = 1.6638886666666666666666666666666666666666666666666666f;
        kolomZ = 4.939287f;
        for (int i = 0; i < arrayZ.GetLength(0); i++) {
            if (arrayZ[i, 1] != 1) {
                Instantiate(kast, new Vector3(transform.position.x -3.5f*rijZ + i*rijZ, transform.position.y, transform.position.z +kolomZ/2) , Quaternion.Euler(0, 90, 0));
                Instantiate(kast, new Vector3(transform.position.x - 3.5f*rijZ + i*rijZ, transform.position.y, transform.position.z+ - kolomZ/2), Quaternion.Euler(0, 90, 0));
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
