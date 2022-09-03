using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{

    private EnemyBase enemyBase;

    private Vector3 firstPose = Vector3.zero;
    private void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        firstPose =transform.position;
    }

    public override void OnEnter()
    {
 
    }

    public override void OnExit()
    {
      
    }

    public override void OnStay()
    {

        if (enemyBase.Health <= 0) { enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Die; return; }

        if (enemyBase.target != null)
        {
            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.FollowTargetState;
            return;
        }

        enemyBase.Move(firstPose);

        if (enemyBase.getVelocity() <= 0.1f) enemyBase.SetMoveAnim(false);
       
    }
}
