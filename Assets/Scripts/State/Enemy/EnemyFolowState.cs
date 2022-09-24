using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFolowState : State
{

    private EnemyBase enemyBase;

    private Changeable<Transform> target;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        target = new Changeable<Transform>(null);

        target.onChangeValue += OnTargetChange;
    }

    private void OnTargetChange(Transform lastValue, Transform CurrentValue)
    {
        
    }

    public override void OnEnter()
    {
        if (enemyBase.disTotarget() <= enemyBase.distanceStopToAttack)
        {
            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Attack;
            return;
        }

    }

    public override void OnExit()
    {

    }

    public override void OnStay()
    {


        if (enemyBase.target != null)
        {
            float distanceStop = enemyBase.distanceStopToAttack;
            float collisonStop = enemyBase.collisionRadius;
            float playerCollison = enemyBase.target.GetComponent<ICollisonable>().getCollisionRadius();

            float weaponeDistanceStop = enemyBase.weapone.maxDistanceToAttack;

            if (weaponeDistanceStop <= collisonStop + playerCollison)
            {
                enemyBase.distanceStopToAttack = collisonStop + playerCollison;
            }
            else
            {
                enemyBase.distanceStopToAttack = weaponeDistanceStop;
            }
        }


        if (enemyBase.Health <= 0) { enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Die; return; }

        target.Value = enemyBase.target;

        if (enemyBase.target == null)
        {
            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.IdleState;
            return;

        }
        else
        {
            enemyBase.averageOfTargets();

            //Debug.Log(enemyBase.disTotarget());

            if (enemyBase.disTotarget() <= enemyBase.distanceStopToAttack)
            {
                enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Attack;
                enemyBase.SetMoveAnim(false);
                enemyBase.setVelocity(0);
                return;
            }
              
            else
                enemyBase.Move(enemyBase.averageOfTargets());
        }



    }
}
