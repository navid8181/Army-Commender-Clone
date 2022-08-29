using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetState : State
{

    private AiBase aiPlayer;


    private void Awake()
    {
        aiPlayer = GetComponent<AiBase>();
       
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }

    public override void OnStay()
    {
        // print(aiPlayer.getCurrentIndex());
        if (aiPlayer.targetToAttack == null)
        {
            aiPlayer.currentDistribution.ExeCuteDistribute(aiPlayer.DistributIndex);
        }
        else
        {
            aiPlayer.SetTraget(aiPlayer.targetToAttack.transform.position);

            Vector3 aiPos = aiPlayer.transform.position;

            aiPos.y = 0;

            Vector3 targetAttack = aiPlayer.targetToAttack.transform.position;

            targetAttack.y = 0;

            float dis = Vector3.Distance(aiPos, targetAttack);

            if (dis <= aiPlayer.maxDistanceToAttack)
            {
                aiPlayer.GetStateManager().currentStateType = currentStateType.Attack;
            }

        }

        aiPlayer.FallowTarget();


    }
}
