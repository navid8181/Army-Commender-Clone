using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{

    private AiBase aiPlayer;


    public float timeToAttack = 2;


    private Timer timer;
    private void Start()
    {
        aiPlayer = GetComponent<AiBase>();

        timer = new Timer(timeToAttack);
        
    }

    public override void OnEnter()
    {
        aiPlayer.SetBoolAnim(false);
    }

    public override void OnExit()
    {
        timer.ResetValue();
    }

    public override void OnStay()
    {
        Vector3 aiPos = aiPlayer.transform.position;

        aiPos.y = 0;

        Vector3 targetAttack = aiPlayer.targetToAttack.transform.position;

        targetAttack.y = 0;

        float dis = Vector3.Distance(aiPos, targetAttack);

        timer.Init(() =>
        {
            aiPlayer.Attack();
        });
        

        if (dis > aiPlayer.maxDistanceToAttack)
        {
            aiPlayer.GetStateManager().currentStateType = currentStateType.FollowTargetState;
        }
    }
}
