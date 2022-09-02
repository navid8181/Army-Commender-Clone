using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Random = UnityEngine.Random;

public class CoinInstancer : CircularInstancer ,IDisposable
{







    bool disposed;

    public CoinInstancer(string name,float minRadius, float maxRadius, float minAngle, float maxAngle) : base(name,minRadius, maxRadius, minAngle, maxAngle)
    {
    }



    public override void Clone(Vector3 pos)
    {
        float angle = Random.Range(minAngle, maxAngle);

        float radius = Random.Range(minRadius, maxRadius);


        Vector3 clonePos = pos + Utility.angleToDireXZ(angle, radius);

        string objectInstanceName = instancerType();

        GameObject pool = MasterManager.Instance.PoolManager.requestPool(objectInstanceName);

        Coin coin = pool.GetComponent<Coin>();

        coin.transform.position = clonePos;
        coin.NoneDistributeTarget = clonePos;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                
              
            }
        }
        //dispose unmanaged resources
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public override string instancerType()
    {
        return name;
    }
}
