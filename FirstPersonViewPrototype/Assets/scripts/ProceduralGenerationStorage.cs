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
    private GameObject kast;
    public GameObject kast1;
    public GameObject kast2;
    public GameObject kast3;
    public GameObject scare;
    private GameObject[] arrayKasten;
    private GameObject kastclone;
    public static int Instantiated;
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

    void Awake()
    {
        Instantiated = 0;
    }
    void Start() {
        
        arrayKasten = new GameObject[] { kast1, kast2, kast3 };
        kast = arrayKasten[Random.Range(0, 3)];
        rijZ = 1.6638886666666666666666666666666666666666666666666666f;
        kolomZ = 3.292858f;
        
        if (deurPositieveXRichting) {
            arrayZ[5, 0] = 1;
            arrayZ[5, 1] = 1;
            arrayX[1, 2] = 1;
            arrayX[1, 3] = 1;
        }

        if (deurNegatieveXRichting)
        {
            arrayZ[0, 0] = 1;
            arrayZ[0, 1] = 1;
            arrayX[0, 2] = 1;
            arrayX[0, 3] = 1;
        }

        if (deurPositieveZRichting)
        {
            arrayX[0, 5] = 1;
            arrayX[1, 5] = 1;
            arrayZ[2, 1] = 1;
            arrayZ[3, 1] = 1;
        }

        if (deurNegatieveZRichting)
        {
            arrayX[0, 0] = 1;
            arrayX[1, 0] = 1;
            arrayZ[2, 0] = 1;
            arrayZ[3, 0] = 1;
        }

        if (!deurNegatieveXRichting)
        {
            if (arrayZ[0, 1] != 1)
            {
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x - 2.5f * rijZ, transform.position.y, transform.position.z + kolomZ), Quaternion.Euler(0, 90, 0));
                kastclone.transform.parent = transform;
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x - 2.5f * rijZ, transform.position.y, transform.position.z), Quaternion.Euler(0, 90, 0));
                kastclone.transform.parent = transform;
                arrayZ[0, 1] = 1;
            }
            if (arrayZ[0, 0] != 1)
            {
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x - 2.5f * rijZ, transform.position.y, transform.position.z + -kolomZ), Quaternion.Euler(0, 90, 0));
                kastclone.transform.parent = transform;
                arrayZ[0, 0] = 1;
            }
        }
        if (!deurPositieveZRichting)
        {
            if (arrayX[1, 5] != 1)
            {
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x + kolomZ, transform.position.y, transform.position.z + rijZ*2.5f), Quaternion.Euler(0, 0, 0));
                kastclone.transform.parent = transform;
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x, transform.position.y, transform.position.z + rijZ * 2.5f), Quaternion.Euler(0, 0, 0));
                kastclone.transform.parent = transform;
                arrayX[1, 5] = 1;
            }
            if (arrayX[0, 5] != 1)
            {
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x - kolomZ, transform.position.y, transform.position.z + rijZ * 2.5f), Quaternion.Euler(0, 0, 0));
                kastclone.transform.parent = transform;
                arrayX[0, 5] = 1;
            }
        }

        if (!deurNegatieveZRichting)
        {
            if (arrayX[1, 0] != 1)
            {
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x + kolomZ, transform.position.y, transform.position.z - rijZ * 2.5f), Quaternion.Euler(0, 0, 0));
                kastclone.transform.parent = transform;
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x, transform.position.y, transform.position.z - rijZ * 2.5f), Quaternion.Euler(0, 0, 0));
                kastclone.transform.parent = transform;
                arrayX[1, 0] = 1;
            }
            if (arrayX[0, 0] != 1)
            {
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x - kolomZ, transform.position.y, transform.position.z - rijZ * 2.5f), Quaternion.Euler(0, 0, 0));
                kastclone.transform.parent = transform;
                arrayX[0, 0] = 1;
            }
        }
        if (!deurPositieveXRichting)
        {
            if (arrayZ[5, 1] != 1)
            {
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x + (5 - 2.5f) * rijZ, transform.position.y, transform.position.z + kolomZ), Quaternion.Euler(0, 90, 0));
                kastclone.transform.parent = transform;
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x + (5 - 2.5f) * rijZ, transform.position.y, transform.position.z), Quaternion.Euler(0, 90, 0));
                kastclone.transform.parent = transform;
                arrayZ[5, 1] = 1;
            }
            if (arrayZ[5, 0] != 1)
            {
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x + (5 - 2.5f) * rijZ, transform.position.y, transform.position.z + -kolomZ), Quaternion.Euler(0, 90, 0));
                kastclone.transform.parent = transform;
                arrayZ[5, 0] = 1;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            rijNummer = Random.Range(0,6);
            kolomNummer = Random.Range(0,2);
            if (arrayX[kolomNummer, rijNummer] != 1)
            {
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x + kolomZ * (2*kolomNummer-1), transform.position.y, transform.position.z + rijZ * (rijNummer-2.5f)), Quaternion.Euler(0, 0, 0));
                kastclone.transform.parent = transform;
                arrayX[kolomNummer, rijNummer] = 1;
                if (rijNummer != 0) { arrayX[kolomNummer, rijNummer - 1] = 1; }
                if (rijNummer != 5) { arrayX[kolomNummer, rijNummer + 1] = 1; }
                if (kolomNummer != 0) { arrayX[kolomNummer - 1, rijNummer] = 1; }
                if (kolomNummer != 1) { arrayX[kolomNummer + 1, rijNummer] = 1; }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            rijNummer = Random.Range(0, 6);
            kolomNummer = Random.Range(0, 2);
            if (arrayZ[rijNummer, kolomNummer] != 1)
            {
                kastclone = (GameObject)Instantiate(kast, new Vector3(transform.position.x + rijZ * (rijNummer-2.5f), transform.position.y, transform.position.z + kolomZ * (2 * kolomNummer - 1)), Quaternion.Euler(0, 90, 0));
                kastclone.transform.parent = transform;
                arrayZ[rijNummer, kolomNummer] = 1;
                if (rijNummer != 0) { arrayZ[rijNummer - 1, kolomNummer] = 1; }
                if (rijNummer != 5) { arrayZ[rijNummer + 1, kolomNummer] = 1; }
                if (kolomNummer != 0) { arrayZ[rijNummer, kolomNummer - 1] = 1; }
                if (kolomNummer != 1) { arrayZ[rijNummer, kolomNummer + 1] = 1; }
            }
        }

        if (Random.Range(0, 20) > 15 && (Instantiated <4)) { Instantiate(scare, new Vector3(transform.position.x,transform.position.y+0.2f,transform.position.z), Quaternion.identity);
            Instantiated = Instantiated + 1;
        }
        kastclone.transform.parent = transform;
    }
}
