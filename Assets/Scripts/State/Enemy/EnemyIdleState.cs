using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
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

        if (enemyBase.Health<=0) enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Die;

        if (enemyBase.targets.Count > 0)
        {
            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.FollowTargetState;
        }

        enemyBase.setVelocity(0);

        if (enemyBase.getVelocity() <= 0.1f) enemyBase.SetMoveAnim(false);
       
    }
}
