using UnityEngine;
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


    void Start()
    {
        RaycastHit hitX;
        RaycastHit hitZ;
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.3f, transform.position.z), Vector3.right, out hitX);
        Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2.3f, transform.position.z), Vector3.forward, out hitZ);
        scalingX = hitX.distance;
        scalingZ = hitZ.distance;

        matrixGrootteX = (int)(scalingX);
        matrixGrootteZ = (int)(scalingZ);
        matrixHokjeX = scalingX * 2 / matrixGrootteX;
        matrixHokjeZ = scalingZ * 2 / matrixGrootteZ;
        matrix = new int[matrixGrootteX, matrixGrootteZ];

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
                Gizmos.DrawSphere(new Vector3(transform.position.x - i * ((2*scalingX)/matrixGrootteX) , transform.position.y, transform.position.z - j* ((2 * scalingZ) / matrixGrootteZ)), 0.1f);
                Gizmos.DrawWireCube(new Vector3(transform.position.x + (i*matrixHokjeX)+matrixGrootteX/2-scalingX, transform.position.y, transform.position.z + (j*matrixHokjeZ)+matrixGrootteZ/2-scalingZ), new Vector3(matrixHokjeX,1, scalingZ * 2 / matrixHokjeZ));
            }
        }
    }
}
