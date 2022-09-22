using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Changeable<T> : IDisposable
{

    public delegate void OnChangeValue(T lastValue, T CurrentValue);

    public OnChangeValue onChangeValue;
    public Changeable(T value)
    {
        Value = value;

    }
    private T Tvalue;
    public T Value
    {
        get => Tvalue; set
        {
            Tvalue = value;

            if (Tvalue == null)
            {
                if (previousValue == null)
                {

                }
                else
                {
                    onChangeValue?.Invoke(previousValue, Tvalue);
                  
                    previousValue = Tvalue;
                }
            }
            else
            if (!Tvalue.Equals(previousValue))
            {
                onChangeValue?.Invoke(previousValue, Tvalue);
              
                previousValue = Tvalue;
            }

        }
    }

    private T previousValue;


    private bool disposed;
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
}
