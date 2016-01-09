/// <summary>
/// Script for the procedural generation of the archives
/// </summary>

using UnityEngine;
using System.Collections;
using System;

public class Procedural_Generation_Archive : MonoBehaviour
{
    private float scalingX;
    private float scalingZ;
    private int[,] matrix;
    private int matrixGrootteX;
    private int matrixGrootteZ;
    private float matrixHokjeX;
    private float matrixHokjeZ;
    private int rij_open;
    public Vector2[] matrixDeur;
    private float[] matrixDeurY;
    private int aantaldozen;

    public GameObject shelf;
    public GameObject doosH;
    public GameObject doosS;
    public GameObject note;
    private GameObject[] doosKeuze = new GameObject[3];

	public GameObject SaveLoadManager;

    void Start()
    {
		SaveLoadManager = GameObject.Find ("SaveLoadManager");
        doosKeuze[0] = doosH;
        doosKeuze[1] = doosS;
        doosKeuze[2] = note;
        RaycastHit hitX;
        RaycastHit hitZ;
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), Vector3.right, out hitX);
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), Vector3.back, out hitZ);
        scalingX = hitX.distance;
        scalingZ = hitZ.distance;

        matrixGrootteX = (int)(scalingX);
        matrixGrootteZ = (int)(scalingZ);
        matrixHokjeX = scalingX * 2 / matrixGrootteX;
        matrixHokjeZ = scalingZ * 2 / matrixGrootteZ;
        matrix = new int[matrixGrootteX, matrixGrootteZ];
    
        matrixDeurY = new float[matrixDeur.Length];
        foreach (Vector2 element in matrixDeur)
        {
            matrix[(int)element.x,(int)element.y]=1   ;

        }
        for (int i = 0; i < matrixDeur.Length; i++)
        {
            matrixDeurY[i] = matrixDeur[i].y;
        }
        for (int j = 0; j < matrixGrootteZ; j++)
        {
            if (Array.IndexOf(matrixDeurY,j)==-1) {
                rij_open = UnityEngine.Random.Range(0, matrixGrootteX);
            }
            else
            {
                rij_open = -1;
            };
            for (int i = 0; i < matrixGrootteX; i++)
            {
                if (i!=rij_open&&matrix[i,j]==0) { GameObject scaledkast = (GameObject)Instantiate(shelf, new Vector3(transform.position.x + i * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z + j * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(0, 0, 0)); 
                    scaledkast.transform.localScale = new Vector3(matrixHokjeX / 2, 1, 1);
                    aantaldozen = UnityEngine.Random.Range(2,5);
                    for (int k = 0; k < aantaldozen; k++)
                    {
                        if (UnityEngine.Random.Range(0, 100) > 10)
                        {
                            if (UnityEngine.Random.Range(0, 10) < 8) { GameObject doos = (GameObject)Instantiate(doosKeuze[UnityEngine.Random.Range(0, 2)], new Vector3(scaledkast.transform.position.x - matrixHokjeX / 2 + 0.4f + k * (matrixHokjeX / (aantaldozen) - 0.05f) + UnityEngine.Random.Range(-0.05f, 0.05f), scaledkast.transform.position.y + 0.867f, scaledkast.transform.position.z + UnityEngine.Random.Range(-0.1f, 0.1f)), Quaternion.Euler(270, UnityEngine.Random.Range(-90, 90), 0)); }
                        }
                        else if(!GameObject.Find("SecurityNote(Clone)")&&!SaveLoadManager.GetComponent<SaveLoadScript>().keyObjectsPickedUp[2])
                        {
                            GameObject noteClone = (GameObject)Instantiate(doosKeuze[2], new Vector3(scaledkast.transform.position.x - matrixHokjeX / 2 + 0.4f + k * (matrixHokjeX / (aantaldozen) - 0.05f) + UnityEngine.Random.Range(-0.05f, 0.05f), scaledkast.transform.position.y + 0.88f, scaledkast.transform.position.z + UnityEngine.Random.Range(-0.1f, 0.1f)), Quaternion.Euler(0, 5, 0));
                        }
                    }
                    aantaldozen = UnityEngine.Random.Range(2, 5);
                    for (int k = 0; k < aantaldozen; k++)
                    {
                        if (UnityEngine.Random.Range(0, 10) < 8) { GameObject doos = (GameObject)Instantiate(doosKeuze[UnityEngine.Random.Range(0, 2)], new Vector3(scaledkast.transform.position.x - matrixHokjeX / 2 + 0.4f + k * (matrixHokjeX / (aantaldozen)-0.05f) + UnityEngine.Random.Range(-0.05f, 0.05f), scaledkast.transform.position.y + 0.073f, scaledkast.transform.position.z + UnityEngine.Random.Range(-0.1f, 0.1f)), Quaternion.Euler(270, UnityEngine.Random.Range(-90, 90), 0)); }
                    }

                }

            }
         }
   }


    void Update()
    {

    }
    void OnDrawGizmos()
    {
        for (int i = 0; i < matrixGrootteX; i++)
        {
            for (int j = 0; j < matrixGrootteZ; j++)
            {

                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(new Vector3(transform.position.x + i * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z + j * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), 0.1f);
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(new Vector3(transform.position.x + i*matrixHokjeX-scalingX+matrixHokjeX/2, transform.position.y, transform.position.z + j*matrixHokjeZ- scalingZ + matrixHokjeZ / 2), new Vector3(matrixHokjeX,1, matrixHokjeZ));
            }
        }
    }
}
