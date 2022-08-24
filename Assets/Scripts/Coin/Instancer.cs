using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public abstract class Instancer
{

    protected String name;

    public Instancer(String name)
    {
        this.name = name;
    }

    protected int TotalInstance { get; private set; }



    public abstract void Clone(Vector3 pos);
    public abstract string instancerType();
}


public abstract class CircularInstancer : Instancer
{
    protected CircularInstancer(String name,float minRadius, float maxRadius, float minAngle, float maxAngle) : base(name)
    {
        this.minRadius = minRadius;
        this.maxRadius = maxRadius;

        this.minAngle = minAngle;
        this.maxAngle = maxAngle;
    }

    public float minRadius { get; private set; }
    public float maxRadius { get; private set; }

    public float minAngle { get; private set; }
    public float maxAngle { get; private set; }

    public override void Clone(Vector3 pos)
    {

        float angle = Random.Range(minAngle, maxAngle);

        float radius = Random.Range(minRadius, maxRadius);


        Vector3 clonePos =  pos + Utility.angleToDireXZ(angle, radius);

        string objectInstanceName = instancerType();

       GameObject pool =  MasterManager.Instance.PoolManager.requestPool(objectInstanceName);


        pool.transform.position = clonePos;

    }


}
