using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIDistribution))]
public class HourseSite : MonoBehaviour
{


    public float timeToCreateHourse = 3;

    private AIDistribution aIDistribution;

    private Timer timer;

    private void Awake()
    {
        aIDistribution = GetComponent<AIDistribution>();

        timer = new Timer(timeToCreateHourse);
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
