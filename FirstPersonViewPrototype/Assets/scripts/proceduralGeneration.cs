using UnityEngine;
using System.Collections;

public class proceduralGeneration : MonoBehaviour {
    private int rij;
    private int kolom;
    private int[,] array = new int[7,6];
    private int rotation;
    private float stoeloffsetx = 0;
    private float stoeloffsetz = 0;
    public GameObject stoel;
    public GameObject desk;
    // Use this for initialization
	void Start () {
        for (int l = 0; l < 6; l++) {
            array[0, l] = 1;
        }
        for (int p = 0; p < 7; p++)
        {
            array[p, 0] = 1;
        }

        for (int i = 0; i < 100; i++)
        {
            rij = Random.Range(0, 7);
            kolom = Random.Range(0, 6);
            rotation = Random.Range(0, 4);
            rotation = rotation * 90;
            if (rotation == 90 || rotation == 270) { stoeloffsetx = 1 - rotation / 180; }
            if (rotation == 0 || rotation == 180) { stoeloffsetz = 0.5f - rotation / 180; }
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
                Instantiate(desk, new Vector3(transform.position.x + (rij - 3.5f), transform.position.y - 0.45f, transform.position.z + (kolom - 3)), Quaternion.Euler(0,rotation,0));
                Instantiate(stoel, new Vector3(transform.position.x + (rij - 3.5f + stoeloffsetx), transform.position.y - 0.45f, transform.position.z + (kolom - 3 + stoeloffsetz)), Quaternion.Euler(0, rotation, 0));
            }
         }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
