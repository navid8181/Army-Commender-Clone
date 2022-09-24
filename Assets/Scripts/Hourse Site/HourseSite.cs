using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIDistribution))]
public class HourseSite : MonoBehaviour
{


    public float timeToCreateHourse = 3;

    public AIDistribution aIDistribution { get; private set; }


    public StateChangere stateChanger;


    private Timer timer;

    

    private void Awake()
    {
        aIDistribution = GetComponent<AIDistribution>();

        timer = new Timer(timeToCreateHourse);
        stateChanger.OnAIbaseComing.AddListener((AiBase aibse) =>
        {
            if (aibse is Hourse) return;


            IDistributable distributable = aIDistribution.GetLastDistribuble();

            if (distributable == null) return;

            Hourse hourse = (Hourse) distributable;

            hourse.aibse = aibse;
        });
    }

    private void Update()
    {
        if (aIDistribution.CurrentDistribuionSize <= aIDistribution.MaxDistrubutionSize)
        {


            timer.Init(() =>
            {



                string Hoursename = PoolManager.Hourse;

                Vector3 poolPositon = aIDistribution.taregt.position + Vector3.back;

                GameObject hourseObject = MasterManager.Instance.PoolManager.requestPool(Hoursename);

                hourseObject.transform.position = poolPositon;

                AiBase aibase = hourseObject.GetComponent<AiBase>();

                aIDistribution.SetDistribut(aibase);

            });

        }
        else
        {
            timer.ResetValue();
        }
    }
}
