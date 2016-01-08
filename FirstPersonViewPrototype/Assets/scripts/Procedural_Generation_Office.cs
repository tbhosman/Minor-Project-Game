using UnityEngine;
using System.Collections;


public class Procedural_Generation_Office : MonoBehaviour {
    private float scalingX;
    private float scalingZ;
    private int[,] matrix;
    private int matrixGrootteX;
    private int matrixGrootteZ;
    private float matrixHokjeX;
    private float matrixHokjeZ;
    private int rij_open;
    public static int kamernummer;

    private int rij;
    private int kolom;
    private int rotation;
    private int whiteboard;
    private int aantaldozen;
    private int kanssleutel;
    private int kamernummersleutel;


    public bool XHighSide;
    public bool XLowSide;
    public bool ZHighSide;
    public bool ZLowSide;

    public GameObject buro;
    public GameObject stoel;
    private GameObject computerkeuze;
    public GameObject computer1;
    public GameObject computer2;
    public GameObject computer3;
    public GameObject computer4;
    public GameObject computer5;
    public GameObject file;
    public GameObject Prullenbak;
    public GameObject Whiteboard;
    public GameObject archiefkast;
    public GameObject sleutel;
    public GameObject pennenbakje;
    public GameObject boek;
    private GameObject kantoorartikel;

    void Start () {

        kanssleutel = 50;
        kamernummersleutel = 41;

        //sends a raycast to obtain information about the dimensions of the room and scale the generation matrix accordingly
        RaycastHit hitX;
        RaycastHit hitZ;
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), Vector3.right, out hitX);
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), Vector3.back, out hitZ);
        scalingX = hitX.distance;
        scalingZ = hitZ.distance;

        matrixGrootteX = (int)(scalingX*2);
        matrixGrootteZ = (int)(scalingZ*2);
        matrixHokjeX = scalingX * 2 / matrixGrootteX;
        matrixHokjeZ = scalingZ * 2 / matrixGrootteZ;
        matrix = new int[matrixGrootteX, matrixGrootteZ];

        //make sure nothing spawns in front of the door
        if (XHighSide) { matrix[matrixGrootteX-1, (int)((matrixGrootteZ - 1) / 2)] = 1; if (matrixGrootteZ % 2 == 0) { matrix[matrixGrootteX - 1,1 + (int)((matrixGrootteZ - 1) / 2)] = 1; } }
        if (ZHighSide) { matrix[(int)((matrixGrootteX - 1) / 2), matrixGrootteZ-1 ] = 1; if (matrixGrootteX % 2 == 0) { matrix[1+(int)((matrixGrootteX - 1) / 2), (matrixGrootteZ - 1)] = 1; } }
        if (XLowSide) { matrix[0, (int)((matrixGrootteZ - 1) / 2)] = 1; if (matrixGrootteZ % 2 == 0) { matrix[0, 1 + (int)((matrixGrootteZ - 1) / 2)] = 1; } }
        if (ZLowSide) { matrix[(int)((matrixGrootteX - 1) / 2), 0] = 1; if (matrixGrootteX % 2 == 0) { matrix[1 + (int)((matrixGrootteX - 1) / 2), 0] = 1; } }

        //initialises a room number
        if (GameObject.FindWithTag("officeKey") == null)
        {
            kamernummer++;
        }

        //generates desks on the south side of the room
        for (int i = 0; i < 5; i++)
        {
            rij = 0;
            kolom = Random.Range(0, matrixGrootteX);
            rotation = Random.Range(-1, 2)*90;

            //chooses a computer out of 5 models
            switch (Random.Range(0, 5))
            {
                case 0:
                    computerkeuze = computer1;
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

            //chooses a officearticle out of 2 models: de pennenbak and the books
            switch (Random.Range(0, 2)) {
                case 0:
                    kantoorartikel = boek;
                    break;
                case 1:
                    kantoorartikel = pennenbakje;
                    break;
            }

            //generate the desk according to the rotation of the desk
            if (matrix[kolom, rij] == 0)
            {
                if (rotation == 0 && kolom < matrixGrootteX - 1 && matrix[kolom + 1, rij] == 0 && matrix[kolom + 1, rij + 1] == 0)
                {
                    GameObject cloneburo = (GameObject)Instantiate(buro, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(0, rotation, 0));
                    GameObject clonestoel = (GameObject)Instantiate(stoel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ + 21 * matrixHokjeZ / 20), Quaternion.Euler(0, 270 + Random.Range(-20, 20), 0));
                    Instantiate(computerkeuze, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + 3*matrixHokjeX/2, transform.position.y+0.9f, transform.position.z + rij * matrixHokjeZ - scalingZ +  matrixHokjeZ / 2+Random.Range(-0.2f,-0.4f)),Quaternion.Euler(0,0,0));
                    Instantiate(file, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2 - Random.Range(-0.2f, -0.4f)), Quaternion.Euler(0, Random.Range(0,359), 180));
                    Instantiate(kantoorartikel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 6, transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2 - Random.Range(0.2f, 0.4f)), Quaternion.Euler(0, Random.Range(0, 359), 0));

                    //een kans om een sleutel te genereren
                    if (GameObject.FindWithTag("officeKey") == null && (Random.Range(0, kanssleutel) < 1 || kamernummer > kamernummersleutel))
                    {
                        Instantiate(sleutel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX  + matrixHokjeX, transform.position.y + 0.865f + matrixHokjeX, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ/2), Quaternion.Euler(180, 0, 0));
                    }

                    cloneburo.transform.localScale = new Vector3(0.395f * matrixHokjeX, 0.5f, 0.5f * matrixHokjeZ);
                    clonestoel.transform.localScale = new Vector3(0.03f * matrixHokjeX, 0.03f, 0.03f * matrixHokjeZ);
                    matrix[kolom, rij] = 1;
                    matrix[kolom + 1, rij] = 1;
                    matrix[kolom, 1] = 1;
                    matrix[kolom + 1, rij + 1] = 1;
                }
                else if (rotation == 90 && kolom < matrixGrootteX - 1 && matrix[kolom, 1] == 0 && matrix[kolom + 1, 1] == 0)
                {
                    GameObject cloneburo = (GameObject)Instantiate(buro, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ), Quaternion.Euler(0, rotation, 0));
                    GameObject clonestoel = (GameObject)Instantiate(stoel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX * 21 / 20, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ), Quaternion.Euler(0, 0 + Random.Range(-20, 20), 0));
                    Instantiate(computerkeuze, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2+Random.Range(-0.2f,-0.4f), transform.position.y+0.9f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ/2), Quaternion.Euler(0, 90, 0));
                    Instantiate(file, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 - Random.Range(-0.2f, -0.4f), transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ + 3*matrixHokjeZ / 2), Quaternion.Euler(0, Random.Range(0,359), 180));
                    Instantiate(kantoorartikel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2-Random.Range(-0.1f,0.1f), transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ + 3*matrixHokjeZ / 2), Quaternion.Euler(0, Random.Range(0, 359), 0));

                    cloneburo.transform.localScale = new Vector3(0.395f * matrixHokjeX, 0.5f, 0.5f * matrixHokjeZ);
                    clonestoel.transform.localScale = new Vector3(0.03f * matrixHokjeX, 0.03f, 0.03f * matrixHokjeZ);

                    //een kans om een sleutel te genereren
                    if (GameObject.FindWithTag("officeKey") == null && (Random.Range(0, kanssleutel) < 1 || kamernummer > kamernummersleutel))
                    {
                        Instantiate(sleutel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX/2, transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ +  matrixHokjeZ), Quaternion.Euler(180, 0, 0));
                    }

                    matrix[kolom, rij] = 1;
                    matrix[kolom, 1] = 1;
                    matrix[kolom + 1, rij] = 1;
                    matrix[kolom + 1, 1] = 1;
                    if (rij < matrixGrootteZ - 2) { matrix[kolom, 2]=1; matrix[kolom + 1, 2] = 1; if (kolom > 0) { matrix[kolom - 1, 2] = 1; } }
                }
                else if (rotation == -90 && kolom >0  && matrix[kolom, 1] == 0 && matrix[kolom -1 , 1] == 0)
                {
                    GameObject cloneburo = (GameObject)Instantiate(buro, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ), Quaternion.Euler(0, rotation, 0));
                    GameObject clonestoel = (GameObject)Instantiate(stoel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX - matrixHokjeX / 20, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ), Quaternion.Euler(0, 180 + Random.Range(-20, 20), 0));
                    Instantiate(computerkeuze, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 - Random.Range(-0.2f, -0.4f), transform.position.y + 0.9f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(0, -90, 0));
                    Instantiate(file, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 - Random.Range(-0.2f, -0.4f), transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ + 3 * matrixHokjeZ / 2), Quaternion.Euler(0, Random.Range(0, 359), 180));
                    Instantiate(kantoorartikel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 - Random.Range(-0.1f, 0.1f), transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ + 5 * matrixHokjeZ / 4), Quaternion.Euler(0, Random.Range(0, 359), 0));

                    cloneburo.transform.localScale = new Vector3(0.395f * matrixHokjeX, 0.5f, 0.5f * matrixHokjeZ);
                    clonestoel.transform.localScale = new Vector3(0.03f * matrixHokjeX, 0.03f, 0.03f * matrixHokjeZ);

                    //een kans om een sleutel te genereren
                    if (GameObject.FindWithTag("officeKey") == null && (Random.Range(0, kanssleutel) < 1 || kamernummer > kamernummersleutel))
                    {
                        Instantiate(sleutel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX/2, transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ), Quaternion.Euler(180, 0, 0));
                    }

                    matrix[kolom, rij] = 1;
                    matrix[kolom, 1]   = 1;
                    matrix[kolom - 1, rij] = 1;
                    matrix[kolom - 1, 1] = 1;
                    if (rij < matrixGrootteZ - 2) { matrix[kolom, 2] = 1; matrix[kolom - 1, 2] = 1; if (kolom < matrixGrootteX-1) { matrix[kolom + 1, 2] = 1; } }
                }
            }
        }

        //generates the north side of the office
        for (int i = 0; i < 6; i++)
        {
            rij = matrixGrootteZ-1;
            kolom = Random.Range(0, matrixGrootteX);
            rotation = Random.Range(1, 4) * 90;

            switch (Random.Range(0, 5))
            {
                case 0:
                    computerkeuze = computer1;
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

            //chooses a officearticle out of 2 models: de pennenbak and the books
            switch (Random.Range(0, 2))
            {
                case 0:
                    kantoorartikel = boek;
                    break;
                case 1:
                    kantoorartikel = pennenbakje;
                    break;
            }

            //generates the desks with office equipment according to the direction
            if (matrix[kolom, rij] == 0)
            {
                if (rotation == 180 && kolom < matrixGrootteX - 1 && matrix[kolom + 1, rij] == 0 && matrix[kolom + 1, rij - 1] == 0)
                {
                    GameObject cloneburo = (GameObject)Instantiate(buro, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(0, rotation, 0));
                    GameObject clonestoel = (GameObject)Instantiate(stoel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ - matrixHokjeZ / 20), Quaternion.Euler(0, 90 + Random.Range(-20, 20), 0));
                    Instantiate(computerkeuze, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + 3 * matrixHokjeX / 2, transform.position.y + 0.9f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2 - Random.Range(-0.2f, -0.4f)), Quaternion.Euler(0, 180, 0));
                    Instantiate(file, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2 - Random.Range(-0.2f, -0.4f)), Quaternion.Euler(0, Random.Range(0, 359), 180));
                    Instantiate(kantoorartikel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2 - Random.Range(0, 0.2f)), Quaternion.Euler(0, Random.Range(0, 359), 0));

                    //een kans om een sleutel te genereren
                    if (GameObject.FindWithTag("officeKey") == null && (Random.Range(0, kanssleutel) < 1 || kamernummer > kamernummersleutel))
                    {
                        Instantiate(sleutel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX, transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ+matrixHokjeZ/2), Quaternion.Euler(180, 0, 0));
                    }

                    cloneburo.transform.localScale = new Vector3(0.395f * matrixHokjeX, 0.5f, 0.5f * matrixHokjeZ);
                    clonestoel.transform.localScale = new Vector3(0.03f * matrixHokjeX, 0.03f, 0.03f * matrixHokjeZ);
                    matrix[kolom, rij] = 1;
                    matrix[kolom + 1, rij] = 1;
                    matrix[kolom, rij-1] = 1;
                    matrix[kolom + 1, rij - 1] = 1;
                    if (rij < matrixGrootteZ - 2) { matrix[kolom, 2] = 1; }
                }
                else if (rotation == 90 && kolom < matrixGrootteX - 1 && matrix[kolom, rij-1] == 0 && matrix[kolom + 1, rij-1] == 0)
                {
                    GameObject cloneburo = (GameObject)Instantiate(buro, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ), Quaternion.Euler(0, rotation, 0));
                    GameObject clonestoel = (GameObject)Instantiate(stoel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX * 21 / 20, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ), Quaternion.Euler(0, 0 + Random.Range(-20, 20), 0));
                    Instantiate(computerkeuze, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 + Random.Range(-0.2f, -0.4f), transform.position.y + 0.9f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(0, 90, 0));
                    Instantiate(file, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 - Random.Range(-0.2f, -0.4f), transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ - matrixHokjeZ / 2), Quaternion.Euler(0, Random.Range(0, 359), 180));
                    Instantiate(kantoorartikel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 - Random.Range(0, 0.2f), transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ - matrixHokjeZ / 2), Quaternion.Euler(0, Random.Range(0, 359), 0));

                    //een kans om een sleutel te genereren
                    if (GameObject.FindWithTag("officeKey") == null && (Random.Range(0, kanssleutel) < 1 || kamernummer > kamernummersleutel))
                    {
                        Instantiate(sleutel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX+matrixHokjeX/2, transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ), Quaternion.Euler(180, 0, 0));
                    }

                    cloneburo.transform.localScale = new Vector3(0.395f * matrixHokjeX, 0.5f, 0.5f * matrixHokjeZ);
                    clonestoel.transform.localScale = new Vector3(0.03f * matrixHokjeX, 0.03f, 0.03f * matrixHokjeZ);
                    matrix[kolom, rij] = 1;
                    matrix[kolom, rij-1] = 1;
                    matrix[kolom + 1, rij] = 1;
                    matrix[kolom + 1, rij-1] = 1;
                    if (matrixGrootteZ>1) { matrix[kolom, rij-2] = 1; matrix[kolom + 1, rij - 2] = 1; if (kolom > 0) { matrix[kolom - 1, rij - 2] = 1; } }
                }
                else if (rotation == 270 && kolom > 0 && matrix[kolom, 1] == 0 && matrix[kolom - 1, rij-1] == 0)
                {
                    GameObject cloneburo = (GameObject)Instantiate(buro, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ), Quaternion.Euler(0, rotation, 0));
                    GameObject clonestoel = (GameObject)Instantiate(stoel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX - matrixHokjeX / 20, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ), Quaternion.Euler(0, 180 + Random.Range(-20, 20), 0));
                    Instantiate(computerkeuze, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 - Random.Range(-0.2f, -0.4f), transform.position.y + 0.9f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(0, -90, 0));
                    Instantiate(file, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 - Random.Range(-0.2f, -0.4f), transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ - matrixHokjeZ / 2), Quaternion.Euler(0, Random.Range(0, 359), 180));
                    Instantiate(kantoorartikel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 - Random.Range(0, 0.2f), transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ - matrixHokjeZ / 2), Quaternion.Euler(0, Random.Range(0, 359), 0));


                    //een kans om een sleutel te genereren
                    if (GameObject.FindWithTag("officeKey") == null && (Random.Range(0, kanssleutel) < 1 || kamernummer > kamernummersleutel))
                    {
                        Instantiate(sleutel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ+matrixHokjeZ/2), Quaternion.Euler(180, 0, 0));
                    }

                    cloneburo.transform.localScale = new Vector3(0.395f * matrixHokjeX, 0.5f, 0.5f * matrixHokjeZ);
                    clonestoel.transform.localScale = new Vector3(0.03f * matrixHokjeX, 0.03f, 0.03f * matrixHokjeZ);
                    matrix[kolom, rij] = 1;
                    matrix[kolom, rij-1] = 1;
                    matrix[kolom - 1, rij] = 1;
                    matrix[kolom - 1, rij-1] = 1;
                    if (matrixGrootteZ > 1) { matrix[kolom, rij - 2] = 1; matrix[kolom - 1, rij - 2] = 1; if (kolom < matrixGrootteX-1) { matrix[kolom + 1, rij - 2] = 1; } }
                }
            }
        }

        //generates trashcans in the corners if the space is not allready occupied
        if (matrix[0, 0] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x - scalingX + matrixHokjeX / 2, transform.position.y + 0.32f, transform.position.z - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(270, 0, 0)); matrix[0, 0] = 1; }
        if (matrix[matrixGrootteX-1, 0] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x + (matrixGrootteX-1)*matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y + 0.32f, transform.position.z - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(270, 0, 0)); matrix[matrixGrootteX-1, 0] = 1; }
        if (matrix[matrixGrootteX-1, matrixGrootteZ-1] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x+(matrixGrootteX-1)*matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y + 0.32f, transform.position.z+(matrixGrootteZ-1)*matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(270, 0, 0)); matrix[matrixGrootteX-1, matrixGrootteZ-1] = 1; }
        if (matrix[0, matrixGrootteZ - 1] == 0) { Instantiate(Prullenbak, new Vector3(transform.position.x  - scalingX + matrixHokjeX / 2, transform.position.y + 0.32f, transform.position.z + (matrixGrootteZ - 1) * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(270, 0, 0)); matrix[0, matrixGrootteZ - 1] = 1; }


        for (int i = 0; i < 2; i++)
        {
            whiteboard = Random.Range(0, 3);
            switch (whiteboard)
            {
                case 0:

                    if (!XHighSide) { Instantiate(Whiteboard, new Vector3(transform.position.x + scalingX-matrixHokjeX/30, transform.position.y + 0.4f, transform.position.z), Quaternion.Euler(0, 0, 0)); }
                    break;
                case 1:
                    if (!XLowSide) { Instantiate(Whiteboard, new Vector3(transform.position.x - scalingX+matrixHokjeX/30, transform.position.y + 0.4f, transform.position.z), Quaternion.Euler(0, 180, 0)); }
                    break;
                case 2:
                    if (!ZHighSide) { Instantiate(Whiteboard, new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z + scalingZ-matrixHokjeZ/30), Quaternion.Euler(0, 270, 0)); }
                    break;
                case 3:
                    if (!ZLowSide) { Instantiate(Whiteboard, new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z - scalingZ+matrixHokjeZ/30), Quaternion.Euler(0, 90, 0)); }
                    break;
            };
        }


        //generate archiveclosets
        for (int i = 0; i < matrixGrootteX; i++)
        {

            if (matrix[i, 0] == 0)
            {
                Instantiate(archiefkast, new Vector3(transform.position.x - scalingX + matrixHokjeX / 2 + i*matrixHokjeX, transform.position.y, transform.position.z - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(0, 0, 0));
                matrix[i, 0] = 1;
            }
            if (matrix[i, matrixGrootteZ-1] == 0)
            {
                Instantiate(archiefkast, new Vector3(transform.position.x - scalingX + matrixHokjeX / 2 + i * matrixHokjeX, transform.position.y, transform.position.z + scalingZ - matrixHokjeZ / 2), Quaternion.Euler(0, 180, 0));
                matrix[i, matrixGrootteZ-1] = 1;
            }
        }
        for (int i = 0; i < matrixGrootteZ; i++)
        {

            if (matrix[0, i] == 0)
            {
                Instantiate(archiefkast, new Vector3(transform.position.x  - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z - scalingZ + matrixHokjeZ / 2 + i*matrixHokjeZ), Quaternion.Euler(0, 90, 0));
                matrix[0, i] = 1;
            }
            if (matrix[matrixGrootteX-1, i] == 0)
            {
                Instantiate(archiefkast, new Vector3(transform.position.x + scalingX - matrixHokjeX / 2, transform.position.y, transform.position.z - scalingZ + matrixHokjeZ / 2 + i * matrixHokjeZ), Quaternion.Euler(0, 270, 0));
                matrix[matrixGrootteX-1, i] = 1;
            }
        }

    }
	

    //show the generation matrix in the scene. is only used for debugging purposes
    void OnDrawGizmos()
    {
        for (int i = 0; i < matrixGrootteX; i++)
        {
            for (int j = 0; j < matrixGrootteZ; j++)
            {
                if (matrix[i, j] == 0)
                {
                    Gizmos.color = Color.blue;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawSphere(new Vector3(transform.position.x + i * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z + j * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), 0.1f);
                
                    Gizmos.color = Color.green;
                
                
                Gizmos.DrawWireCube(new Vector3(transform.position.x + i * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z + j * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), new Vector3(matrixHokjeX, 1, matrixHokjeZ));
            }
        }
    }
}
