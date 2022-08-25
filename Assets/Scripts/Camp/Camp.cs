using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AIDistribution))]
public class Camp : MonoBehaviour
{

    private AIDistribution aIDistribution;

    public float timeToCreateAiBace = 4;

    public float maxAi = 5;

    private Timer timer;

    private void Awake()
    {
        aIDistribution = GetComponent<AIDistribution>();
        timer = new Timer(timeToCreateAiBace);
       

    }



    private void Update()
    {

        timer.Init(() =>
        {
            if (aIDistribution.getLenght <= maxAi)
            {
                CreatePlayer();
            }
       
        });
      
           

        
    }

    private void CreatePlayer()
    {
        AiBase aiBase = MasterManager.Instance.PoolManager.requestPool(PoolManager.sowrdManAI).GetComponent<AiBase>();

        aiBase.transform.position = transform.position;

        aIDistribution.SetAi(aiBase);
    }

}
