using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistributeComming))]
public class SetTargetDistribution : MonoBehaviour
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

                TargetState targetState = aiBase.GetComponent<TargetState>();

                targetState.target = target;

                StateManager stateManager = aiBase.GetComponent<StateManager>();
                stateManager.currentStateType = currentStateType.SetTarget;
            }
        });
    }

}
