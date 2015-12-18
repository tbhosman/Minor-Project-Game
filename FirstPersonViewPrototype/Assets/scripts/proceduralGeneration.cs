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
    public int x_coordinaatdeur;
    public int z_coordinaatdeur;
    public static int kamernummer;
    private float scalingX;
    private float scalingZ;

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
    public GameObject file;
    private GameObject computerkeuze;
    public GameObject sleutel;

    // Use this for initialization
    void Start() {

        //send raycast to obtain the scaling dimensions
        RaycastHit hit;
        RaycastHit hitZ;
        Physics.Raycast(new Vector3(transform.position.x,transform.position.y+2.3f,transform.position.z), Vector3.right, out hit);
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.3f, transform.position.z), Vector3.forward, out hitZ);
        scalingX = hit.distance / 3.58382364f;
        scalingZ = hitZ.distance/3.114922f;
       

        //remove first and last rows in small rooms to minimise overcrowding
        if (hitZ.distance <2.5f) {
            for (int i = 0; i < 7; i++)
            {
                array[i, 0] = 1;
                array[i, 5] = 1;
                array[i, 1] = 1;
                array[i, 4] = 1;
            }
        }

        //generate kamernummer;
        if (GameObject.FindWithTag("officeKey") == null)
        {
            kamernummer++;
            Debug.Log(kamernummer);
        }
        //Make sure there are no objects spawning in front of the door
        array[x_coordinaatdeur, z_coordinaatdeur] = 1;
        if (x_coordinaatdeur != 0){ array[x_coordinaatdeur - 1, z_coordinaatdeur] = 1; }
        if (x_coordinaatdeur != 6){ array[x_coordinaatdeur + 1, z_coordinaatdeur] = 1; }
        if (z_coordinaatdeur !=0) { array[x_coordinaatdeur, z_coordinaatdeur - 1] = 1; }
        if (z_coordinaatdeur != 5){ array[x_coordinaatdeur, z_coordinaatdeur + 1] = 1; }
        if (x_coordinaatdeur > 1) { array[x_coordinaatdeur - 2, z_coordinaatdeur] = 1; }
        if (x_coordinaatdeur < 5) { array[x_coordinaatdeur + 2, z_coordinaatdeur] = 1; }
        if (z_coordinaatdeur > 1) { array[x_coordinaatdeur, z_coordinaatdeur - 2] = 1; }
        if (z_coordinaatdeur < 5) { array[x_coordinaatdeur, z_coordinaatdeur + 2] = 1; }
        if (x_coordinaatdeur != 0 && z_coordinaatdeur != 0) { array[x_coordinaatdeur - 1, z_coordinaatdeur - 1] = 1; }
        if (x_coordinaatdeur != 6 && z_coordinaatdeur != 0) { array[x_coordinaatdeur + 1, z_coordinaatdeur - 1] = 1; }
        if (z_coordinaatdeur != 5 && x_coordinaatdeur != 6) { array[x_coordinaatdeur + 1, z_coordinaatdeur +1] = 1; }
        if (z_coordinaatdeur != 5 && x_coordinaatdeur !=0) { array[x_coordinaatdeur - 1, z_coordinaatdeur + 1] = 1; }


        //Generate trashcans in the corners
        for (int i = 0; i < 2; i++) {
            prullenbak = Random.Range(0, 4);
            switch (prullenbak) {
                case 0:

                    if (array[6, 5] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x + 3.23f*scalingX, transform.position.y + 0.3f, transform.position.z + 2.75f*scalingZ), Quaternion.Euler(270, 0, 0)); array[6, 5] = 1; }
                    break;
                case 1:
                    if (array[0, 0] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x - 3.23f * scalingX, transform.position.y + 0.3f, transform.position.z - 2.75f*scalingZ), Quaternion.Euler(270, 0, 0)); array[0, 0] = 1; }
                    break;
                case 2:
                    if (array[0, 5] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x - 3.23f * scalingX, transform.position.y + 0.3f, transform.position.z + 2.65f*scalingZ), Quaternion.Euler(270, 0, 0)); array[0, 5] = 1; }
                    break;
                case 3:
                    if (array[6, 0] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x + 3.23f * scalingX, transform.position.y + 0.3f, transform.position.z - 2.65f*scalingZ), Quaternion.Euler(270, 0, 0)); array[6, 0] = 1; }
                    break;
            };
        }

        //generate whiteboards on the wall
        for (int i = 0; i < 2; i++)
        {
            whiteboard = Random.Range(0, 3);
            switch (whiteboard)
            {
                case 0:

                    if (x_coordinaatdeur != 6) { Instantiate(Whiteboard, new Vector3(transform.position.x + 3.5f * scalingX, transform.position.y + 0.4f, transform.position.z), Quaternion.Euler(0, 0, 0)); }
                    break;
                case 1:
                    if (x_coordinaatdeur != 0) { Instantiate(Whiteboard, new Vector3(transform.position.x - 3.5f * scalingX, transform.position.y + 0.4f, transform.position.z), Quaternion.Euler(0, 180, 0)); }
                    break;
                case 2:
                    if (z_coordinaatdeur != 5) { Instantiate(Whiteboard, new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z + 3.04f*scalingZ), Quaternion.Euler(0, 270, 0)); }
                    break;
                case 3:
                    if (z_coordinaatdeur != 0) { Instantiate(Whiteboard, new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z - 3.00f*scalingZ), Quaternion.Euler(0, 90, 0)); }
                    break;
            };
        }

        //generate double desk in the middle
        for (int i = 0; i < 2; i++) {
            rij = Random.Range(3, 5);
            kolom = Random.Range(2, 3);
            if (array[rij,kolom]==0 && Random.Range(0,12) > 1 && hitZ.distance>2.5f) {
                //Instantiate(desk, new Vector3(transform.position.x + (rij * scalingX - 3.0f*scalingX), transform.position.y, transform.position.z + (kolom*scalingZ - 2.5f*scalingZ)), Quaternion.Euler(0, rotation, 0));
                //Instantiate(desk, new Vector3(transform.position.x + (rij * scalingX - 3.0f*scalingX), transform.position.y, transform.position.z + (kolom-1)*scalingZ - 2.5f*scalingZ), Quaternion.Euler(0, 180, 0));
                array[rij, kolom] = 1;
                array[rij, kolom-1] = 1;

                array[rij+1, kolom] = 1;
                array[rij+1, kolom-1] = 1;

                array[rij-1, kolom] = 1;
                array[rij - 1, kolom + 1] = 1;

                array[rij, kolom - 2] = 1;
                array[rij, kolom + 1] = 1;

                array[rij - 2, kolom + 1] = 1;
                array[rij - 2, kolom] = 1;

                array[rij + 2, kolom] = 1;
                array[rij + 2, kolom - 1] = 1;

                array[rij + 1, kolom + 1] = 1;
                array[rij + 1, kolom - 2] = 1;

                array[rij - 1, kolom + 1] = 1;
                array[rij - 1, kolom - 2] = 1;
            }

        }

        //generate desks with chairs elsewhere in the office
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

                

                Instantiate(desk, new Vector3(transform.position.x + ((rij + deskoffsetRij*0.5f) *scalingX - 3.0f*scalingX), transform.position.y, transform.position.z + ((kolom + deskoffsetKolom*0.5f)*scalingZ - 2.5f*scalingZ)), Quaternion.Euler(0, rotation, 0));
                Instantiate(computerkeuze, new Vector3(transform.position.x + (rij  * scalingX - 3.0f*scalingX + stoeloffsetx * -0.2f), transform.position.y+0.86f, transform.position.z + (kolom - 2.5f+stoeloffsetz * -0.2f)*scalingZ), Quaternion.Euler(0, rotation, 0));
                Instantiate(file, new Vector3(transform.position.x + (rij * scalingX - 3.0f*scalingX+deskoffsetRij + Random.Range(-0.3f,0.3f)), transform.position.y + 0.866f, transform.position.z + (kolom - 2.5f + deskoffsetKolom)*scalingZ + Random.Range(-0.3f, 0.3f)), Quaternion.Euler(180, Random.Range(0,360), 0));
                if (array[(int)(rij+stoeloffsetx),(int)( kolom+stoeloffsetz)] ==0) { Instantiate(stoel, new Vector3(transform.position.x + ((rij) * scalingX - 3.0f * scalingX + stoeloffsetx * 0.75f), transform.position.y, transform.position.z + ((kolom) - 2.5f + stoeloffsetz * 0.75f)*scalingZ), Quaternion.Euler(0, rotation - 90 + Random.Range(-20, 20), 0)); }

                
                if (GameObject.FindWithTag("officeKey")==null&&(Random.Range(0,50)<1||kamernummer==26))
                {
                    Instantiate(sleutel, new Vector3(transform.position.x + (rij * scalingX - 3.0f * scalingX + deskoffsetRij*1.1f + Random.Range(-0.3f, 0.3f)), transform.position.y + 0.873f, transform.position.z + (kolom - 2.5f + deskoffsetKolom*1.1f) * scalingZ + Random.Range(-0.3f, 0.3f)), Quaternion.Euler(180, 0, 0));
                }
               

                if (deskoffsetRij != 0)
                {
                    if (kolom != 5) { array[rij, kolom + 1] = 1; array[rij + deskoffsetRij, kolom + 1] = 1; }
                    if (kolom != 0) { array[rij, kolom - 1] = 1; array[rij + deskoffsetRij, kolom - 1] = 1; }
                    if (rij + 2 * deskoffsetRij != 7 && rij + 2 * deskoffsetRij >= 0) { array[rij + 2 * deskoffsetRij, kolom] = 1; }
                    if (rij - deskoffsetRij != -1 && rij - deskoffsetRij != 7) { array[rij - deskoffsetRij, kolom] = 1; }
                }
                if (deskoffsetKolom != 0)
                {
                    if (rij != 6) { array[rij + 1, kolom] = 1; array[rij + 1, kolom + deskoffsetKolom] = 1; }
                    if (rij != 0) { array[rij - 1, kolom] = 1; array[rij - 1, kolom + deskoffsetKolom] = 1; }
                    if (kolom + 2 * deskoffsetKolom != 6 && kolom + 2 * deskoffsetKolom >= 0) { array[rij, kolom + 2 * deskoffsetKolom] = 1; }
                    if (kolom - deskoffsetKolom != -1 && kolom - deskoffsetKolom != 6) { array[rij, kolom - deskoffsetKolom] = 1; }
                }
            }
            stoeloffsetx = 0;
            stoeloffsetz = 0;
        }

        //generate archiveclosets
        for (int i = 0;i<6;i++) {
            
            if (array[i, 0] == 0)
            {
                Instantiate(archiefkast, new Vector3(transform.position.x+i*scalingX-3.0f*scalingX, transform.position.y, transform.position.z-2.4f*scalingZ), Quaternion.Euler(0, 0, 0));
                array[i, 0] = 1;
            }
            if (array[i, 5] == 0)
            {
                Instantiate(archiefkast, new Vector3(transform.position.x + i * scalingX - 3.0f*scalingX, transform.position.y, transform.position.z + (2.4f*scalingZ)), Quaternion.Euler(0, 180, 0));
                array[i, 5] = 1;
            }
        }
        for (int i = 0; i < 5; i++)
        {

            if (array[0, i] == 0)
            {
                Instantiate(archiefkast, new Vector3(transform.position.x + 0.1f * scalingX - 3.0f*scalingX, transform.position.y, transform.position.z - 2.5f*scalingZ+i*scalingZ), Quaternion.Euler(0, 90, 0));
                array[0, i] = 1;
            }
            if (array[6, i] == 0)
            {
                Instantiate(archiefkast, new Vector3(transform.position.x + 5.9f * scalingX - 3.0f*scalingX, transform.position.y, transform.position.z - 2.5f*scalingZ+ i*scalingZ), Quaternion.Euler(0, 270, 0));
                array[6, i] = 1;
            }
        }
    }

    //draw a matrix for testing
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
                Gizmos.DrawSphere(new Vector3(transform.position.x+scalingX*i-3.0f*scalingX,transform.position.y,transform.position.z+(j-2.5f)*scalingZ), 0.1f);
            }
        }
    }
}
