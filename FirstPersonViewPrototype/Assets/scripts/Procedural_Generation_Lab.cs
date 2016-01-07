using UnityEngine;
using System.Collections;

public class Procedural_Generation_Lab : MonoBehaviour {
    private float scalingX;
    private float scalingZ;
    private int[,] matrix;
    private int matrixGrootteX;
    private int matrixGrootteZ;
    private float matrixHokjeX;
    private float matrixHokjeZ;
    
    void Start () {

        //sends a raycast to obtain information about the dimensions of the room and scale the generation matrix accordingly
        RaycastHit hitX;
        RaycastHit hitZ;
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), Vector3.right, out hitX);
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z), Vector3.back, out hitZ);
        scalingX = hitX.distance;
        scalingZ = hitZ.distance;

        Debug.Log("scalingX: " + scalingX + "  scalingz: " + scalingZ);
        matrixGrootteX = (int)(scalingX / 2);
        matrixGrootteZ = (int)(scalingZ / 2);
        matrixHokjeX = scalingX  / matrixGrootteX;
        matrixHokjeZ = scalingZ / matrixGrootteZ;
        matrix = new int[matrixGrootteX, matrixGrootteZ];
    }

    //draws the matrix, only used for debugging purposes
    void OnDrawGizmos()
    {
        for (int i = 0; i < matrixGrootteX; i++)
        {
            for (int j = 0; j < matrixGrootteZ; j++)
            {
                if (matrix[i, j] == 0)
                {
                    Gizmos.color = Color.yellow;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawSphere(new Vector3(transform.position.x + i * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z + j * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), 0.1f);

                Gizmos.color = Color.magenta;


                Gizmos.DrawWireCube(new Vector3(transform.position.x + i * matrixHokjeX - scalingX + matrixHokjeX / 2, transform.position.y, transform.position.z + j * matrixHokjeZ - scalingZ + matrixHokjeZ / 2), new Vector3(matrixHokjeX, 1, matrixHokjeZ));
            }
        }
    }
}
