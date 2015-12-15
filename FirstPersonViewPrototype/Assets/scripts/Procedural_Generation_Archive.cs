﻿using UnityEngine;
using System.Collections;

public class Procedural_Generation_Archive : MonoBehaviour
{
    private float scalingX;
    private float scalingZ;
    private int[,] matrix;
    private int matrixGrootteX;
    private int matrixGrootteZ;
    private float matrixHokjeX;
    private float matrixHokjeZ;

    public GameObject shelf;
   

    void Start()
    {
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

        
        for (int i = 0; i < matrixGrootteX; i++)
        {
            for (int j = 0; j < matrixGrootteZ; j++)
            {
                if (Random.Range(0, 10) < 8) { GameObject scaledkast = (GameObject)Instantiate(shelf, new Vector3(transform.position.x + i * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z + j * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), Quaternion.Euler(0, 0, 0)); 
                    scaledkast.transform.localScale = new Vector3(matrixHokjeX / 2, 1, 1);
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
