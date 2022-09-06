using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{

    private AiBase aiPlayer;



    private Timer timer;
    private void Start()
    {
        aiPlayer = GetComponent<AiBase>();

        timer = new Timer(aiPlayer.timeToAttack);

    }


    public override void OnEnter()
    {
        aiPlayer.SetBoolAnim(false);
    }

    public override void OnExit()
    {



        timer.ResetValue();
        if (aiPlayer.targetToAttack == null)
            aiPlayer.GetStateManager().currentStateType = currentStateType.FollowTargetState;
      
    }

    public override void OnStay()
    {


        aiPlayer.particleSystemController.SetStartLifeTime(0);

        timer.SetCounter(aiPlayer.timeToAttack);

        if (aiPlayer.targetToAttack  != null && aiPlayer.targetToAttack.Health <= 0)
            aiPlayer.targetToAttack = null;

        if (aiPlayer.targetToAttack == null)
        {

          //  aiPlayer.FindEnemy();

            if (aiPlayer.targetToAttack == null)
            {
                aiPlayer.GetStateManager().currentStateType = currentStateType.FollowTargetState;
                return;
            }
              
        }
          




         Vector3 aiPos = aiPlayer.transform.position;
        Vector3 targetAttack = Vector3.zero;
        aiPos.y = 0;
        if (aiPlayer.targetToAttack != null)
        {
            targetAttack = aiPlayer.targetToAttack.transform.position;

           Vector3 dire = targetAttack - aiPos;
            dire.y = 0;

            dire.Normalize();

            transform.rotation = Quaternion.LookRotation(dire, Vector3.up);
        }
        
        else
        {
            //aiPlayer.FindEnemy();
            if (aiPlayer.targetToAttack != null)
                targetAttack = aiPlayer.targetToAttack.transform.position;
            else
            {
                aiPlayer.GetStateManager().currentStateType = currentStateType.FollowTargetState;

                return;
            }

        }
        targetAttack.y = 0;

        float dis = Vector3.Distance(aiPos, targetAttack);

        timer.Init(() =>
        {
            aiPlayer.Attack();
        });

        if (aiPlayer.Health <= 0) { aiPlayer.GetStateManager().currentStateType = currentStateType.Die;  return; }



        if (dis > aiPlayer.maxDistanceToAttack)
        {
            aiPlayer.GetStateManager().currentStateType = currentStateType.FollowTargetState;

            return;
        }
    }
}
