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

   //     Debug.Log("Attack State ");


        if (enemyBase.Health <= 0) { enemyBase.GetEnemyStateManager().currentStateType = currentStateType.Die; return; }
        if (enemyBase.target != null)
        {
            IDamageable aiBase = enemyBase.target.GetComponent<IDamageable>();
            if (aiBase != null)
                if (aiBase.Health <= 0) enemyBase.target = null;
            
        }
        if ( enemyBase.target == null)
        {

        

            enemyBase.GetEnemyStateManager().currentStateType = currentStateType.IdleState;
            return;
        }


        Vector3 targetPos = enemyBase.averageOfTargets();
        targetPos.y = 0;

        Vector3 enemyPos = transform.position;
        enemyPos.y = 0;

        float dis = Vector3.Distance(targetPos, enemyPos);

        //if (dis > enemyBase.distanceStopToAttack+0.1f )
        //{
        //    if (enemyBase.target != null)
        //    enemyBase.Move(enemyBase.target.position);
        //    else
        //    {
        //        enemyBase.GetEnemyStateManager().currentStateType = currentStateType.IdleState;
        //        return;
        //    }

        //    Debug.Log("dis > enemyBase.distanceStopToAttack+0.1f ");
        //}
        //else
        {
            enemyBase.SetMoveAnim(false);
            enemyBase.setVelocity(0);
            enemyBase.FootStepparticleController.SetStartLifeTime(0);

            Vector3 dire = targetPos - enemyPos;
            dire.Normalize();

            transform.rotation = Quaternion.LookRotation(dire, Vector3.up);

            timer.Init(() =>
            {
                enemyBase.Attack();

            });
        }
      
    }
}
