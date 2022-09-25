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



    public static void CubicDistribute(IDistributable[] objectDistribut, Transform target, int with,int height, Vector3 offset, int i)
    {
        int rowCount = ((i / with) )% height;

        int heightCount = (i / (with * height)) ;

   

        int index = i % (with);



        Vector3 flatfwd = target.transform.forward;
        flatfwd.y = 0;


        Vector3 flatrgh = target.transform.right;
        flatrgh.y = 0;


        Vector3 center = target.position - flatfwd * offset.z * rowCount + target.transform.up * heightCount * offset.y;


        Vector3 leftStartPos = center - flatrgh * with * offset.x;

        Vector3 rightStartPos = center + flatrgh * with * offset.x;

        Vector3 finalPos = Vector3.Lerp(leftStartPos, rightStartPos, index / (float)with);

        objectDistribut[i].SetTraget(finalPos);







    }


    public static Quaternion smothlyRoationToTarget(Transform from,Transform to,float time)
    {
        Vector3 dire = to.position - from.position;

        dire.Normalize();

        dire.y = 0;
        Quaternion rot = Quaternion.LookRotation(dire, Vector3.up);


        return Quaternion.Slerp(from.rotation,rot,time * Time.deltaTime);
    }
}
