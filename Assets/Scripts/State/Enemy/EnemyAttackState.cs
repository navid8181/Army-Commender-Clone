using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{


    private EnemyBase enemyBase;

    public float timeToAttack = 2;


    private Timer timer;

    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();

        timer = new Timer(timeToAttack);
    }

    public override void OnEnter()
    {
      
    }

    public override void OnExit()
    {
        timer.ResetValue();
    }

    public override void OnStay()
    {
        enemyBase.averageOfTargets();

        timer.Init(() =>
        {
            enemyBase.Attack();
        });


        if (enemyBase.disTotarget() > enemyBase.distanceStopToAttack)
        {
            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.FollowTargetState;
        }
    }
}
