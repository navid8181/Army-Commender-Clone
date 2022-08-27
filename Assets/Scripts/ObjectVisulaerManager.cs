using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectVisulaerManager : DistributionBase
{


    public AIDistribution aiDistribution;

    public Camp camp;


    private void Awake()
    {

        ApplyVisualer();
    }

    public void ApplyVisualer()
    {

        for (int i = 0; i < camp.maxAi; i++)
        {
          //  ObjectRectangleVisualzer obV = MasterManager.Instance.PoolManager.requestPool(PoolManager.objectVisualer).GetComponent<>;

           // id.Add(obV);
        }

       
    }

    private void Update()
    {
        //Utility.RectangleDistribute(objectRectangleVisualzers.ToArray(), transform, aiDistribution.getWith, aiDistribution.getOfffset);
    }

    public override void ExeCuteDistribute(int i)
    {
        throw new System.NotImplementedException();
    }
}
