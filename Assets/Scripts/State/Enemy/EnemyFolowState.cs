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

    }

    public override void OnExit()
    {

    }

    public override void OnStay()
    {
        if (enemyBase.targets.Count <= 0)
        {
            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.IdleState;
        
        }
        enemyBase.averageOfTargets();
        if (enemyBase.disTotarget() <= enemyBase.maxDistanceToStop )
            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Attack;
        else
        enemyBase.Move();


    }
}
