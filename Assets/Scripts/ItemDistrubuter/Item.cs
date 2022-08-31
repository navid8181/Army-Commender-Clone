using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,IDistributable
{
    public int DistributIndex { get; set; }
    public DistributionBase currentDistribution { get; set; }

    public bool isRemove = false;

     

    public void SetTraget(Vector3? target)
    {
        transform.position = target.GetValueOrDefault();
    }

    private void Update()
    {
        if (DistributIndex >=0)
        currentDistribution.ExeCuteDistribute(DistributIndex);

       
    }

    private void LateUpdate()
    {
        if (isRemove)
        {
            isRemove = false;

            currentDistribution.RemoveDistribut(this);
        }
    }

}
