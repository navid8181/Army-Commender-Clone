using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AIDistribution))]
public class Camp : MonoBehaviour
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



    private void Update()
    {

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
        AiBase aiBase = MasterManager.Instance.PoolManager.requestPool(PoolManager.sowrdManAI).GetComponent<AiBase>();

        aiBase.transform.position = transform.position;

        aiBase.currentDistribution = aIDistribution;
        aiBase.currentDistribution.SetDistribut(aiBase);
    }

}
