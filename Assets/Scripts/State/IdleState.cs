using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    private AIPlayer aiPlayer;

    private void Awake()
    {
        aiPlayer = GetComponent<AIPlayer>();
    }

    public override void OnEnter()
    {
        aiPlayer.StopPlayer();
    }

    public override void OnExit()
    {
      
    }

    public override void OnStay()
    {
      
    }
}
