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

    private int rij;
    private int kolom;
    private int rotation;

    private float[] matrixDeurY;
    private int aantaldozen;

    public Vector2[] matrixDeur;
    public GameObject buro;
    public GameObject stoel;
    private GameObject computerkeuze;
    public GameObject computer1;
    public GameObject computer2;
    public GameObject computer3;
    public GameObject computer4;
    public GameObject computer5;
    public GameObject file;

    void Start () {
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
        Debug.Log("X: " + matrixHokjeX + "Z: " + matrixHokjeZ);
        matrix = new int[matrixGrootteX, matrixGrootteZ];

        matrixDeurY = new float[matrixDeur.Length];
        foreach (Vector2 element in matrixDeur)
        {
            matrix[(int)element.x, (int)element.y] = 1;

        }
        for (int i = 0; i < matrixDeur.Length; i++)
        {
            matrixDeurY[i] = matrixDeur[i].y;
        }


        for (int i = 0; i < 9; i++)
        {
            rij = 0;
            kolom = Random.Range(0, matrixGrootteX);
            rotation = Random.Range(-1, 2)*90;

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

            if (matrix[kolom, rij] == 0)
            {
                if (rotation == 0 && kolom < matrixGrootteX - 1 && matrix[kolom + 1, rij] == 0 && matrix[kolom + 1, rij + 1] == 0)
                {
                    GameObject cloneburo = (GameObject)Instantiate(buro, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(0, rotation, 0));
                    GameObject clonestoel = (GameObject)Instantiate(stoel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ + 3 * matrixHokjeZ / 2), Quaternion.Euler(0, 270 + Random.Range(-20, 20), 0));
                    Instantiate(computerkeuze, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + 3*matrixHokjeX/2, transform.position.y+0.9f, transform.position.z + rij * matrixHokjeZ - scalingZ +  matrixHokjeZ / 2+Random.Range(-0.2f,-0.4f)),Quaternion.Euler(0,0,0));
                    Instantiate(file, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2 - Random.Range(-0.2f, -0.4f)), Quaternion.Euler(0, Random.Range(0,359), 180));

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
                    GameObject clonestoel = (GameObject)Instantiate(stoel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX * 3 / 2, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(0, 0 + Random.Range(-20, 20), 0));
                    Instantiate(computerkeuze, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2+Random.Range(-0.2f,-0.4f), transform.position.y+0.9f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ/2), Quaternion.Euler(0, 90, 0));
                    Instantiate(file, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 - Random.Range(-0.2f, -0.4f), transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ + 3*matrixHokjeZ / 2), Quaternion.Euler(0, Random.Range(0,359), 180));
                    cloneburo.transform.localScale = new Vector3(0.395f * matrixHokjeX, 0.5f, 0.5f * matrixHokjeZ);
                    clonestoel.transform.localScale = new Vector3(0.03f * matrixHokjeX, 0.03f, 0.03f * matrixHokjeZ);
                    matrix[kolom, rij] = 1;
                    matrix[kolom, 1] = 1;
                    matrix[kolom + 1, rij] = 1;
                    matrix[kolom + 1, 1] = 1;

                }
                else if (rotation == -90 && kolom >0  && matrix[kolom, 1] == 0 && matrix[kolom -1 , 1] == 0)
                {
                    GameObject cloneburo = (GameObject)Instantiate(buro, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ), Quaternion.Euler(0, rotation, 0));
                    GameObject clonestoel = (GameObject)Instantiate(stoel, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX - matrixHokjeX / 2, transform.position.y, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(0, 180 + Random.Range(-20, 20), 0));
                    Instantiate(computerkeuze, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 - Random.Range(-0.2f, -0.4f), transform.position.y + 0.9f, transform.position.z + rij * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(0, -90, 0));
                    Instantiate(file, new Vector3(transform.position.x + kolom * matrixHokjeX - scalingX + matrixHokjeX / 2 - Random.Range(-0.2f, -0.4f), transform.position.y + 0.865f, transform.position.z + rij * matrixHokjeZ - scalingZ + 3 * matrixHokjeZ / 2), Quaternion.Euler(0, Random.Range(0, 359), 180));
                    cloneburo.transform.localScale = new Vector3(0.395f * matrixHokjeX, 0.5f, 0.5f * matrixHokjeZ);
                    clonestoel.transform.localScale = new Vector3(0.03f * matrixHokjeX, 0.03f, 0.03f * matrixHokjeZ);
                    matrix[kolom, rij] = 1;
                    matrix[kolom, 1]   = 1;
                    matrix[kolom - 1, rij] = 1;
                    matrix[kolom - 1, 1] = 1;
                }
            }
        }


 
        }
	
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
