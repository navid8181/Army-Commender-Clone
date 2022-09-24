using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetState : State
{

    private AiBase aiPlayer;

    private Changeable<EnemyBase> enemy;


    private void Awake()
    {
        aiPlayer = GetComponent<AiBase>();
        enemy = new Changeable<EnemyBase>(null);
        enemy.onChangeValue += targetChange;
    }

    private void targetChange(EnemyBase lastValue, EnemyBase CurrentValue)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {
        enemy.Value = aiPlayer.targetToAttack;
    }

    public override void OnStay()
    {

        if (aiPlayer.targetToAttack != null)
        {
            float distanceStop = aiPlayer.distanceStopToAttack;
            float collisonStop = aiPlayer.collisionRadius;
            float playerCollison = aiPlayer.targetToAttack.getCollisionRadius();

            float weaponeAtackStop = aiPlayer.distanceStopToAttack;

            if (weaponeAtackStop <= collisonStop + playerCollison)

                aiPlayer.distanceStopToAttack = collisonStop + playerCollison;
            else
                aiPlayer.distanceStopToAttack = weaponeAtackStop;

        }

        if (aiPlayer.Health <= 0)
        {
            aiPlayer.GetStateManager().currentStateType = currentStateType.Die;
            return;
        }

        enemy.Value = aiPlayer.targetToAttack;

        if (aiPlayer.targetToAttack == null)
        {

            if (aiPlayer.currentDistribution != null)
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

            //Debug.Log(dis + "??" + aiPlayer.distanceStopToAttack);

            if (dis <= aiPlayer.distanceStopToAttack)
            {
                // Debug.Log(dis);
                aiPlayer.GetStateManager().currentStateType = currentStateType.Attack;
                return;
            }

        }

        aiPlayer.FallowTarget();


    }
}
