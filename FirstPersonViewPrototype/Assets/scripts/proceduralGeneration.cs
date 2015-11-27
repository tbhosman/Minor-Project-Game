using UnityEngine;
using System.Collections;

public class proceduralGeneration : MonoBehaviour {
    private int rij;
    private int kolom;
    private int[,] array = new int[7,6];
    private int rotation;
    private int rotationStoel;
    private int prullenbak;
    private int whiteboard;
    private float stoeloffsetx = 0;
    private float stoeloffsetz = 0;
    public GameObject stoel;
    public GameObject desk;
    public GameObject Prullenbak;
    public GameObject Whiteboard;
   
    // Use this for initialization
	void Start () {
        for (int l = 0; l < 6; l++) {
            array[0, l] = 1;
        }
        for (int p = 0; p < 7; p++)
        {
            array[p, 0] = 1;
        }

        for (int i = 0; i < 2; i++) {
            prullenbak = Random.Range(0, 4);
            switch (prullenbak) {
                case 0:

                    if (array[6, 5] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x + 3.5f, transform.position.y+0.3f, transform.position.z + 3), Quaternion.Euler(270, 0, 0)); array[6, 5] = 1; }
                    break;
                case 1:
                    if (array[1, 1] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x - 3.5f, transform.position.y+0.3f, transform.position.z - 2), Quaternion.Euler(270, 0, 0)); array[1, 1] = 1;  }
                    break;
                case 2:
                    if (array[1, 5] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x - 3.5f, transform.position.y+0.3f, transform.position.z + 3), Quaternion.Euler(270, 0, 0)); array[1, 5] = 1; }
                    break;
                case 3:
                    if (array[6, 1] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x + 3.5f, transform.position.y+0.3f, transform.position.z - 2), Quaternion.Euler(270, 0, 0)); array[6, 1] = 1; }
                    break;
            };
        }

        for (int i = 0; i < 2; i++)
        {
            whiteboard = Random.Range(0, 4);
            switch (whiteboard)
            {
                case 0:

                   Instantiate(Whiteboard, new Vector3(transform.position.x + 3.75f, transform.position.y -0.5f, transform.position.z), Quaternion.Euler(0, 0, 0));
                    break;
                case 1:
                   Instantiate(Whiteboard, new Vector3(transform.position.x - 3.75f, transform.position.y -0.5f, transform.position.z), Quaternion.Euler(0, 180, 0));
                    break;
                case 2:
                   Instantiate(Whiteboard, new Vector3(transform.position.x, transform.position.y -0.5f, transform.position.z + 3.38f), Quaternion.Euler(0, 270, 0));
                    break;
                case 3:
                    Instantiate(Whiteboard, new Vector3(transform.position.x, transform.position.y -0.5f, transform.position.z - 2.74f), Quaternion.Euler(0, 90, 0));
                    break;
            };
        }

        for (int i = 0; i < 10; i++)
        {
            rij = Random.Range(0, 7);
            kolom = Random.Range(0, 6);
            rotation = Random.Range(0, 4);
            rotation = rotation * 90;
            rotationStoel = Random.Range(0, 4) * 90; 
            if (rotation == 90 || rotation == 270) { stoeloffsetx = (2 - (rotation / 90)); }
            if (rotation == 0 || rotation == 180) { stoeloffsetz = (1 - (rotation / 90)); }
            if (array[rij, kolom] == 0)
            {
                array[rij, kolom] = 1;
                if (rij != 0) { array[rij - 1, kolom] = 1; }
                if (kolom != 0) { array[rij, kolom - 1] = 1; }
                if (rij != 6) { array[rij + 1, kolom] = 1; }
                if (kolom != 5) { array[rij, kolom + 1] = 1; }
                if (kolom < 4) { array[rij, kolom + 2] = 1; }
                if (kolom > 1) { array[rij, kolom - 2] = 1; }
                if (rij > 1) { array[rij-2, kolom] = 1; }
                if (rij < 5) { array[rij+2, kolom] = 1; }
                if (rij != 6&&kolom!=5) { array[rij +1, kolom+1] = 1; }
                if (rij != 6 && kolom != 0) { array[rij + 1, kolom - 1] = 1; }
                if (rij != 0 && kolom != 5) { array[rij -1, kolom + 1] = 1; }
                if (rij != 0 && kolom != 0) { array[rij - 1, kolom - 1] = 1; }
                Instantiate(desk, new Vector3(transform.position.x + (rij - 3.5f), transform.position.y, transform.position.z + (kolom - 2.75f)), Quaternion.Euler(0,rotation,0));
                Instantiate(stoel, new Vector3(transform.position.x + (rij - 3.5f + stoeloffsetx*0.9f), transform.position.y , transform.position.z + (kolom - 2.75f + stoeloffsetz*0.9f)), Quaternion.Euler(0, rotationStoel, 0));
            }
            stoeloffsetx = 0;
            stoeloffsetz = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
