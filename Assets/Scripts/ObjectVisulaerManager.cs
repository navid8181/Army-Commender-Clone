using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectVisulaerManager : DistributionBase
{


    private AIDistribution aiDistribution;

    public Camp camp;


    private void Awake()
    {
        aiDistribution = camp.GetComponent<AIDistribution>();
        ApplyVisualer();
    }


    public void ApplyVisualer()
    {

        for (int i = 0; i <= camp.maxAi; i++)
        {
            GameObject visular = MasterManager.Instance.PoolManager.requestPool(PoolManager.objectVisualer);

            ObjectRectangleVisualzer orv = visular.GetComponent<ObjectRectangleVisualzer>();

            SetDistribut(orv);

           // id.Add(obV);
        }

       
    }

    private void Update()
    {
   
    }

    public override void ExeCuteDistribute(int i)
    {
        Utility.RectangleDistribute(GetDistributables(), transform, aiDistribution.getWith, aiDistribution.getOfffset,i);
    }
}
