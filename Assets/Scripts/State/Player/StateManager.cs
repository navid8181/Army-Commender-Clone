using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum currentStateType { IdleState,FollowTargetState,Attack}

[RequireComponent(typeof(IdleState))]
[RequireComponent(typeof(FollowTargetState))]
[RequireComponent(typeof(AttackState))]
public class StateManager : BaseStateManager
{

    public currentStateType currentStateType;

    private currentStateType previousStateType;

 

    private void Start()
    {
       
        currentState = GetComponent<FollowTargetState>();
       
        
    }

    private void Update()
    {
        if (currentStateType != previousStateType)
        {
            switch (currentStateType)
            {
                case currentStateType.IdleState:
                    ChangeState(GetComponent<IdleState>());
                    break;
                case currentStateType.FollowTargetState:
                    ChangeState(GetComponent<FollowTargetState>());
                    break;
                case currentStateType.Attack:
                    ChangeState(GetComponent<AttackState>());
                    break;
                default:
                    break;
            }

            previousStateType = currentStateType;
        }
    
    }


}
