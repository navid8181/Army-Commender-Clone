using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRectangleVisualzer : MonoBehaviour, IDistributable
{
    public Vector3 offsset;

    public int DistributIndex { set; get; }
    public DistributionBase currentDistribution { set; get; }

    public void SetTraget(Vector3? target)
    {
        transform.position = target.GetValueOrDefault() + offsset;
    }

    private void Start()
    {
        currentDistribution.ExeCuteDistribute(DistributIndex);
    }
}
