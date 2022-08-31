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

    
        if (enemyBase.Health <= 0) enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Die;
        if (enemyBase.target != null)
        {
            IDamageable aiBase = enemyBase.target.GetComponent<IDamageable>();
            if (aiBase != null)
                if (aiBase.Health <= 0) enemyBase.target = null;
            
        }
        if ( enemyBase.target == null)
        {
            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.IdleState;
        }

        if (enemyBase.disTotarget() > enemyBase.distanceStopToAttack+0.1f )
        {
            if (enemyBase.target != null)
            enemyBase.Move(enemyBase.target.position);
            else
            {
                enemyBase.GetEnemyStateManager().currentStateType = currentStateType.IdleState;
            }
        }
        timer.Init(() =>
        {
            enemyBase.Attack();

        });
    }
}
