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


    public static void RectangleDistribute(IDistributable[] objectDistribut, Transform target, int with, Vector3 offset,int i)
    {
        int rowCount = (i / with) + 1;



        int index = i % (with);
        float tempOffsetZ = offset.z;


        Vector3 flatfwd = target.transform.forward;
        flatfwd.y = 0;


        Vector3 flatrgh = target.transform.right;
        flatrgh.y = 0;


        Vector3 center = target.position - flatfwd * offset.z * rowCount;


        Vector3 leftStartPos = center - flatrgh * with * offset.x;

        Vector3 rightStartPos = center + flatrgh * with * offset.x;

        Vector3 finalPos = Vector3.Lerp(leftStartPos, rightStartPos, index / (float)with);

        objectDistribut[i].SetTraget(finalPos);







    }


}
