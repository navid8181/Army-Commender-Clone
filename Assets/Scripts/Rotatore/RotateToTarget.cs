using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    public Transform target;



    private void Update()
    {

        Vector3 dire = target.position - transform.position;

        dire.y = 0;

        dire.Normalize();

        transform.rotation = Quaternion.LookRotation(dire, Vector3.up);
    }

}
