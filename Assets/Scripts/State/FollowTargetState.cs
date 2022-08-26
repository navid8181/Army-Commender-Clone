using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetState : State
{

    private AIPlayer aiPlayer;

    private void Awake()
    {
        aiPlayer = GetComponent<AIPlayer>();
    }

    public override void OnEnter()
    {
  
    }

    public override void OnExit()
    {

    }

    public override void OnStay()
    {
        print(aiPlayer.getCurrentIndex());
        aiPlayer.GetAIDistribution().RectangleDistribute(aiPlayer.getCurrentIndex());
        aiPlayer.Fallow();
    }
}
