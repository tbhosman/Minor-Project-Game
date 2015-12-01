using UnityEngine;
using System.Collections;

public class proceduralGeneration : MonoBehaviour {
    private int rij;
    private int kolom;
    private int[,] array = new int[7, 6];
    private int rotation;
    private int rotationStoel;
    private int prullenbak;
    private int whiteboard;
    private float stoeloffsetx = 0;
    private float stoeloffsetz = 0;
    private int deskoffsetRij = 0;
    private int deskoffsetKolom = 0;

    public GameObject archiefkast;
    public GameObject stoel;
    public GameObject desk;
    public GameObject Prullenbak;
    public GameObject Whiteboard;
    public GameObject computer1;
    public GameObject computer2;
    public GameObject computer3;
    public GameObject computer4;
    public GameObject computer5;
    private GameObject computerkeuze;
    // Use this for initialization
    void Start() {

        for (int i = 0; i < 2; i++) {
            prullenbak = Random.Range(0, 4);
            switch (prullenbak) {
                case 0:

                    if (array[6, 5] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x + 3.55f, transform.position.y + 0.3f, transform.position.z + 2.75f), Quaternion.Euler(270, 0, 0)); array[6, 5] = 1; }
                    break;
                case 1:
                    if (array[0, 0] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x - 3.55f, transform.position.y + 0.3f, transform.position.z - 2.75f), Quaternion.Euler(270, 0, 0)); array[0, 0] = 1; }
                    break;
                case 2:
                    if (array[0, 5] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x - 3.55f, transform.position.y + 0.3f, transform.position.z + 2.65f), Quaternion.Euler(270, 0, 0)); array[0, 5] = 1; }
                    break;
                case 3:
                    if (array[6, 0] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x + 3.55f, transform.position.y + 0.3f, transform.position.z - 2.65f), Quaternion.Euler(270, 0, 0)); array[6, 0] = 1; }
                    break;
            };
        }

        for (int i = 0; i < 2; i++)
        {
            whiteboard = Random.Range(0, 4);
            switch (whiteboard)
            {
                case 0:

                    Instantiate(Whiteboard, new Vector3(transform.position.x + 3.85f, transform.position.y-0.2f, transform.position.z), Quaternion.Euler(0, 0, 0));
                    break;
                case 1:
                    Instantiate(Whiteboard, new Vector3(transform.position.x - 3.85f, transform.position.y - 0.2f, transform.position.z), Quaternion.Euler(0, 180, 0));
                    break;
                case 2:
                    Instantiate(Whiteboard, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z + 3.14f), Quaternion.Euler(0, 270, 0));
                    break;
                case 3:
                    Instantiate(Whiteboard, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z - 3.08f), Quaternion.Euler(0, 90, 0));
                    break;
            };
        }

        for (int i = 0; i < 2; i++) {
            rij = Random.Range(3, 5);
            kolom = Random.Range(2, 3);
            if (array[rij,kolom]==0) {
                Instantiate(desk, new Vector3(transform.position.x + (rij * 1.1f - 3.3f), transform.position.y, transform.position.z + (kolom - 2.5f)), Quaternion.Euler(0, rotation, 0));
                Instantiate(desk, new Vector3(transform.position.x + (rij) * 1.1f - 3.3f, transform.position.y, transform.position.z + (kolom-1) - 2.5f), Quaternion.Euler(0, 180, 0));
                array[rij, kolom] = 1;
                array[rij-1, kolom] = 1;
                array[rij+1, kolom] = 1;
                array[rij - 2, kolom] = 1;
                array[rij, kolom+1] = 1;
                array[rij - 1, kolom + 1] = 1;
                array[rij, kolom - 1] = 1;
                array[rij - 1, kolom - 1] = 1;
                array[rij - 2, kolom + 1] = 1;
                array[rij, kolom + 2] = 1;
                array[rij - 1, kolom + 2] = 1;
                array[rij - 3, kolom] = 1;
                array[rij - 3, kolom + 1] = 1;
                array[rij + 2, kolom] = 1;
                array[rij + 2, kolom + 1] = 1;
                array[rij, kolom - 2] = 1;
                array[rij - 1, kolom - 2] = 1;
                array[rij, kolom + 3] = 1;
                array[rij - 1, kolom + 3] = 1;
            }

        }

        for (int i = 0; i < 10; i++)
        {
            rij = Random.Range(0, 7);
            kolom = Random.Range(0, 6);
            rotation = Random.Range(0, 4);
            rotation = rotation * 90;
            rotationStoel = Random.Range(0, 4) * 90;
            deskoffsetKolom = 0;
            deskoffsetRij = 0;
            switch (Random.Range(0, 5))
            {
                case 0:
                    computerkeuze=computer1;
                    break;
                case 1:
                    computerkeuze = computer2;
                    break;
                case 2:
                    computerkeuze = computer3;
                    break;
                case 3:
                    computerkeuze = computer4;
                    break;
                case 4:
                    computerkeuze = computer5;
                    break;
            }


            if (kolom == 0) { rotation = 0; }
            else if (kolom == 5) { rotation = 180; }
            else if (rij == 0) { rotation = 90; }
            else if (rij == 6) { rotation = 270; }

            if (rotation == 270 || rotation == 90)
            {
                if (kolom != 5)
                {
                    deskoffsetKolom = 1;
                }
                else {
                    deskoffsetKolom = -1;
                }


            }
            if (rotation == 0 || rotation == 180) {
                if (rij != 6)
                {
                    deskoffsetRij = 1;
                }
                else
                {
                    deskoffsetRij = -1;
                }
            }


            if (rotation == 90 || rotation == 270) { stoeloffsetx = (2 - (rotation / 90)); }
            if (rotation == 0 || rotation == 180) { stoeloffsetz = (1 - (rotation / 90)); }
            if (array[rij, kolom] == 0)
            {
                array[rij, kolom] = 1;
                array[rij + deskoffsetRij, kolom + deskoffsetKolom] = 1;

                if (deskoffsetRij != 0)
                {
                    if (kolom!=5) { array[rij, kolom + 1] = 1; array[rij + deskoffsetRij, kolom + 1] = 1; }
                    if (kolom != 0) { array[rij, kolom - 1] = 1; array[rij + deskoffsetRij, kolom - 1] = 1; }
                    if (rij + 2 * deskoffsetRij != 7 && rij + 2 * deskoffsetRij >= 0) { array[rij + 2 * deskoffsetRij, kolom] = 1; }
                    if (rij - deskoffsetRij != -1 && rij - deskoffsetRij != 7) { array[rij - deskoffsetRij, kolom] = 1; }
                }
                if (deskoffsetKolom != 0)
                {
                    if (rij != 6) { array[rij + 1, kolom] = 1; array[rij + 1, kolom + deskoffsetKolom] = 1; }
                    if (rij != 0) { array[rij - 1, kolom] = 1; array[rij - 1, kolom + deskoffsetKolom] = 1; }
                    if (kolom + 2 * deskoffsetKolom != 6 && kolom + 2 * deskoffsetKolom >=0) { array[rij, kolom + 2 * deskoffsetKolom] = 1; }
                    if (kolom - deskoffsetKolom != -1 && kolom - deskoffsetKolom != 6) { array[rij, kolom - deskoffsetKolom] = 1; }
                }

                Instantiate(desk, new Vector3(transform.position.x + ((rij + deskoffsetRij*0.5f) *1.1f - 3.3f), transform.position.y, transform.position.z + ((kolom + deskoffsetKolom*0.5f) - 2.5f)), Quaternion.Euler(0, rotation, 0));
                Instantiate(computerkeuze, new Vector3(transform.position.x + (rij  * 1.1f - 3.3f + stoeloffsetx * -0.2f), transform.position.y+0.86f, transform.position.z + kolom - 2.5f+stoeloffsetz * -0.2f), Quaternion.Euler(0, rotation, 0));
                Instantiate(stoel, new Vector3(transform.position.x + ((rij + deskoffsetRij*0.5f) * 1.1f - 3.3f + stoeloffsetx * 0.75f), transform.position.y, transform.position.z + ((kolom + deskoffsetKolom*0.5f) - 2.5f + stoeloffsetz * 0.75f)), Quaternion.Euler(0, rotation-90, 0));
                Debug.Log(deskoffsetRij + " "  + deskoffsetKolom);
            }
            stoeloffsetx = 0;
            stoeloffsetz = 0;
        }
    }
    
void OnDrawGizmos()
{
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (array[i, j] == 0)
                {
                    Gizmos.color = Color.cyan;
                }else { Gizmos.color = Color.red; }
                Gizmos.DrawSphere(new Vector3(transform.position.x+1.1f*i-3.3f,transform.position.y,transform.position.z+j-2.5f), 0.1f);
            }
        }
}

// Update is called once per frame
void Update () {
	
	}
}
