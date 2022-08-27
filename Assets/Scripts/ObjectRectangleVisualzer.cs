using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRectangleVisualzer : MonoBehaviour, IDistributable
{
    public Vector3 offsset;

    public int DistributIndex { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void SetTraget(Vector3? target)
    {
        transform.position = target.GetValueOrDefault() + offsset;
    }
}
