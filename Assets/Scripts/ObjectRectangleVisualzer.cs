using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRectangleVisualzer : MonoBehaviour, IDistributable
{
    public Vector3 offsset;
    public void SetTraget(Vector3? target)
    {
        transform.position = target.GetValueOrDefault() + offsset;
    }
}
