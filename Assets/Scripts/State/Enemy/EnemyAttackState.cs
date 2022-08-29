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
        if (enemyBase.targets.Count >0)
        if (enemyBase.targets[0].GetComponent<IDamageable>().Health <= 0) enemyBase.targets.Clear();

        if (enemyBase.disTotarget() > enemyBase.distanceStopToAttack+0.1f)
        {
            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.FollowTargetState;
        }
        timer.Init(() =>
        {
            enemyBase.Attack();

        });
    }
}
