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
        if (currentDistribution != null)
        currentDistribution.ExeCuteDistribute(DistributIndex);

       
    }

    

}
