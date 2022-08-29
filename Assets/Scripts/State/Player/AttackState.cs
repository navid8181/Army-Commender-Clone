using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{

    private AiBase aIPlayer;

    private void Start()
    {
        aIPlayer = GetComponent<AiBase>();
        
    }

    public override void OnEnter()
    {
       
    }

    public override void OnExit()
    {
    
    }

    public override void OnStay()
    {
        
    }
}
