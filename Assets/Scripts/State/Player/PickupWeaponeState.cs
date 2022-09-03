using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeaponeState : State
{
    private AiBase aiPlayer;

    public Transform target;
    private void Awake()
    {
        aiPlayer = GetComponent<AiBase>();
    }
    public override void OnEnter()
    {
        aiPlayer.SetTraget(target.position);
    }

    public override void OnExit()
    {
        
    }

    public override void OnStay()
    {
        aiPlayer.FallowTarget();
    }
}
