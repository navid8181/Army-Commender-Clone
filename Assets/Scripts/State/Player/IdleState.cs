using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    private AiBase aiPlayer;

    private StateManager stateManager;

    private void Awake()
    {
        aiPlayer = GetComponent<AiBase>();
        stateManager = GetComponent<StateManager>();
    }

    public override void OnEnter()
    {
        aiPlayer.Stop();
    }

    public override void OnExit()
    {
      
    }

    public override void OnStay()
    {
        aiPlayer.Stop();
        if (aiPlayer.distanceToTarget() > aiPlayer.brackDistance)
        {
            stateManager.currentStateType = currentStateType.FollowTargetState;
        }
    }
}
