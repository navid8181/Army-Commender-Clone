using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistributeComming))]
public abstract class SetTargetDistribution : MonoBehaviour
{


    public Transform target;

    private DistributeComming distributeComming;


    private void Awake()
    {
        distributeComming = GetComponent<DistributeComming>();

        distributeComming.idistributComming.AddListener((IDistributable[] idistributes) =>
        {
            for (int i = 0; i < idistributes.Length; i++)
            {
                AiBase aiBase = (AiBase)idistributes[i];

               

                if (BlockConditon(aiBase)) continue;
                if (aiBase is Hourse) Debug.Log("hhhhhhhhhhhhh");
                TargetState targetState = aiBase.GetComponent<TargetState>();

                targetState.target = target;

                StateManager stateManager = aiBase.GetComponent<StateManager>();
                stateManager.currentStateType = currentStateType.SetTarget;
            }
        });
    }






    public abstract bool BlockConditon(AiBase aiBase);
}
