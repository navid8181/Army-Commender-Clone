using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Timer :IDisposable
{
    public delegate void OnExecute();
    public float counter { get; set; }

    private float time = 0;


    public float getCounter() => counter;
    public void SetCounter(float value) => counter = value;

    public Timer(float counter)
    {
        this.counter= counter;

        time = counter;
    }

    public void Init(OnExecute onExecute)
    {
        if (time > 0) time -= Time.deltaTime;

        else
        {

            onExecute();

            time = counter;
        }


    }


    public void ResetValue() => time = counter;

    public float getNormalTime =>  1.0f- (time/counter);

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
