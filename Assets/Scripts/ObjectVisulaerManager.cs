using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectVisulaerManager : DistributionBase
{


    private AIDistribution aiDistribution;

    public Camp camp;

    private int visulaerCount = 0;

    private void Awake()
    {
        aiDistribution = camp.GetComponent<AIDistribution>();
        //CreateVisualer();
    }


    public void CreateVisualer()
    {

        if (camp.maxAi == visulaerCount) return;

        for (int i = 0; i < camp.maxAi - visulaerCount; i++)
        {
            GameObject visular = MasterManager.Instance.PoolManager.requestPool(PoolManager.objectVisualer);

            ObjectRectangleVisualzer orv = visular.GetComponent<ObjectRectangleVisualzer>();

            SetDistribut(orv);

           // id.Add(obV);
        }

        visulaerCount = camp.maxAi;
       
    }

    private void Update()
    {
        CreateVisualer();
    }

    public override void ExeCuteDistribute(int i)
    {
        Utility.RectangleDistribute(GetDistributables(), transform, aiDistribution.getWith, aiDistribution.getOfffset,i);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawCube(transform.position, Vector3.one);

    }
}
