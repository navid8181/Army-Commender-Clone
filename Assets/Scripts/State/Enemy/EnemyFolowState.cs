using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFolowState : State
{

    private EnemyBase enemyBase;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
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

        if (enemyBase.Health <= 0) { enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Die; return; }

        if (enemyBase.target == null)
        {
            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.IdleState;
            return;

        }
        else
        {
            enemyBase.averageOfTargets();

            Debug.Log(enemyBase.disTotarget());

            if (enemyBase.disTotarget() <= enemyBase.distanceStopToAttack)
            {
                enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Attack;
                return;
            }
              
            else
                enemyBase.Move(enemyBase.averageOfTargets());
        }



    }
}
