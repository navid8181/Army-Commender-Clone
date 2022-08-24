using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{

    public static Vector3 angleToDireXZ(float angle, float radius)
    {
        float radAnlgle = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(radAnlgle), 0, Mathf.Sin(radAnlgle)).normalized * radius;
    }


    public static void RectangleDistribute(IDistributable[] objectDistribut, Transform target, int with, Vector3 offset)
    {
        float tempOffsetZ = offset.z;

        Vector3 center = Vector3.zero;


        Vector3 leftStartPos = Vector3.zero;

        Vector3 rightStartPos = Vector3.zero;



        int rowCount = 0;

        for (int i = 0; i < objectDistribut.Length; i++)
        {
            int index = i % (with);



            // if (!aiBases[i].validPos) continue;

            if (index != 0)
            {

                Vector3 finalPos = Vector3.Lerp(leftStartPos, rightStartPos, index / (float)with);

                objectDistribut[i].SetTraget(finalPos);

            }
            else
            {

                rowCount++;

                center = target.position - target.transform.forward * offset.z * rowCount;

                leftStartPos = center - target.transform.right * with * offset.x;
                rightStartPos = center + target.transform.right * with * offset.x;

                Vector3 finalPos = Vector3.Lerp(leftStartPos, rightStartPos, 0);

                objectDistribut[i].SetTraget(finalPos);
            }

        }
    }

}
