using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AIDistribution))]
public class Camp : Updater
{

    private AIDistribution aIDistribution;

    public float timeToCreateAiBace = 4;

    public int maxAi = 5;

    private Timer timer;

    private void Awake()
    {
        aIDistribution = GetComponent<AIDistribution>();
        timer = new Timer(timeToCreateAiBace);

        aIDistribution.maxSoldir = maxAi;

    }



    public override void Update()
    {
        base.Update();
        if (timer.getCounter() != timeToCreateAiBace)
        {
            timer.SetCounter(timeToCreateAiBace);
        }

        if (maxAi != aIDistribution.maxSoldir)
            aIDistribution.maxSoldir = maxAi;

        if (aIDistribution.CurrentDistribuionSize >= aIDistribution.MaxDistrubutionSize)
        {
            timer.ResetValue();
        }
        timer.Init(() =>
        {
            if (aIDistribution.CurrentDistribuionSize <= aIDistribution.MaxDistrubutionSize)
            {
                CreatePlayer();
            }
       
        });


        }

    private void CreatePlayer()
    {
        GameObject pol = MasterManager.Instance.PoolManager.requestPool(PoolManager.sowrdManAI);
        if (pol == null) return;
        AiBase aiBase =pol.GetComponent<AiBase>();
      
        aiBase.InitilizeOnStatup();
        aiBase.transform.position = transform.position;

        aiBase.currentDistribution = aIDistribution;
        aiBase.currentDistribution.SetDistribut(aiBase);
    }

    public override void ExecuteUpdater()
    {
        base.ExecuteUpdater();
        maxAi++;
    }
}
