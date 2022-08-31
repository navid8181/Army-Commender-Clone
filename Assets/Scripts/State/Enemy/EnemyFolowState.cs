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
            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Attack;
    }

    public override void OnExit()
    {

    }

    public override void OnStay()
    {

        if (enemyBase.Health <= 0) enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Die;

        if (enemyBase.target == null)
        {
            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.IdleState;

        }
        else
        {
            enemyBase.averageOfTargets();
            if (enemyBase.disTotarget() <= enemyBase.distanceStopToAttack)
                enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Attack;
            else
                enemyBase.Move(enemyBase.averageOfTargets());
        }



    }
}
