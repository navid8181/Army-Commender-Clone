using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetState : State
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
  
    }

    public override void OnExit()
    {

    }

    public override void OnStay()
    {
       // print(aiPlayer.getCurrentIndex());
        aiPlayer.currentDistribution.ExeCuteDistribute(aiPlayer.DistributIndex);
        aiPlayer.FallowTarget();

      
    }
}
