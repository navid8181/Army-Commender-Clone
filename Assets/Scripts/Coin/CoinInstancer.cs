using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public class CoinInstancer : CircularInstancer ,IDisposable
{







    bool disposed;

    public CoinInstancer(string name,float minRadius, float maxRadius, float minAngle, float maxAngle) : base(name,minRadius, maxRadius, minAngle, maxAngle)
    {
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
