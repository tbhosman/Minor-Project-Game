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
    public bool deurPositieveZRichting;
    public bool deurNegatieveZRichting;
    public bool deurPositieveXRichting;
    public bool deurNegatieveXRichting;
    private float rijZ;
    private float kolomZ;
    private int [,] arrayZ = new int[6, 2];
    private int [,] arrayX = new int[2, 6];
    private int rijNummer;
    private int kolomNummer;
    void Start() {
        rijZ = 1.6638886666666666666666666666666666666666666666666666f;
        kolomZ = 4.939287f;
        //for (int i = 0; i < arrayZ.GetLength(0); i++) {
        //if (arrayZ[i, 1] != 1) {
        //Instantiate(kast, new Vector3(transform.position.x  + (i-2.5f)*rijZ, transform.position.y, transform.position.z +kolomZ/2) , Quaternion.Euler(0, 90, 0));
        //Instantiate(kast, new Vector3(transform.position.x  + (i-2.5f)*rijZ, transform.position.y, transform.position.z+ - kolomZ/2), Quaternion.Euler(0, 90, 0));
        //}
        //}
        if (deurNegatieveXRichting == false)
        {
            if (arrayZ[0, 1] != 1)
            {
                Instantiate(kast, new Vector3(transform.position.x - 2.5f * rijZ, transform.position.y, transform.position.z + kolomZ / 2), Quaternion.Euler(0, 90, 0));
                arrayZ[0, 1] = 1;
            }
            if (arrayZ[0, 0] != 1)
            {
                Instantiate(kast, new Vector3(transform.position.x - 2.5f * rijZ, transform.position.y, transform.position.z + -kolomZ / 2), Quaternion.Euler(0, 90, 0));
                arrayZ[0, 0] = 1;
            }
        }
        if (deurPositieveZRichting == false)
        {
            if (arrayX[1, 5] != 1)
            {
                Instantiate(kast, new Vector3(transform.position.x + (kolomZ*0.5f), transform.position.y, transform.position.z + rijZ*2.5f), Quaternion.Euler(0, 0, 0));
                arrayX[1, 5] = 1;
            }
            if (arrayX[0, 5] != 1)
            {
                Instantiate(kast, new Vector3(transform.position.x - (kolomZ * 0.5f), transform.position.y, transform.position.z + rijZ * 2.5f), Quaternion.Euler(0, 0, 0));
                arrayX[0, 5] = 1;
            }
        }

        if (deurNegatieveZRichting == false)
        {
            if (arrayX[1, 0] != 1)
            {
                Instantiate(kast, new Vector3(transform.position.x + (kolomZ * 0.5f), transform.position.y, transform.position.z - rijZ * 2.5f), Quaternion.Euler(0, 0, 0));
                arrayX[1, 0] = 1;
            }
            if (arrayX[0, 0] != 1)
            {
                Instantiate(kast, new Vector3(transform.position.x - (kolomZ * 0.5f), transform.position.y, transform.position.z - rijZ * 2.5f), Quaternion.Euler(0, 0, 0));
                arrayX[0, 0] = 1;
            }
        }
        if (deurPositieveXRichting == false)
        {
            if (arrayZ[5, 1] != 1)
            {
                Instantiate(kast, new Vector3(transform.position.x + (5 - 2.5f) * rijZ, transform.position.y, transform.position.z + kolomZ / 2), Quaternion.Euler(0, 90, 0));
                arrayZ[5, 1] = 1;
            }
            if (arrayZ[5, 0] != 1)
            {
                Instantiate(kast, new Vector3(transform.position.x + (5 - 2.5f) * rijZ, transform.position.y, transform.position.z + -kolomZ / 2), Quaternion.Euler(0, 90, 0));
                arrayZ[5, 0] = 1;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            rijNummer = Random.Range(0,6);
            kolomNummer = Random.Range(0,2);
            if (arrayX[kolomNummer, rijNummer] != 1)
            {
                Instantiate(kast, new Vector3(transform.position.x + kolomZ * (kolomNummer-0.5f), transform.position.y, transform.position.z + rijZ * (rijNummer-2.5f)), Quaternion.Euler(0, 0, 0));
                arrayX[kolomNummer, rijNummer] = 1;
                if (rijNummer != 0) { arrayX[kolomNummer, rijNummer - 1] = 1; }
                if (rijNummer != 5) { arrayX[kolomNummer, rijNummer + 1] = 1; }
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
