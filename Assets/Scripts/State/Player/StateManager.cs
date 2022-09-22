using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum currentStateType { IdleState,FollowTargetState,Attack,Die,SetTarget}

[RequireComponent(typeof(IdleState))]
[RequireComponent(typeof(FollowTargetState))]
[RequireComponent(typeof(AttackState))]
[RequireComponent(typeof(DieState))]
[RequireComponent(typeof(TargetState))]
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
                case currentStateType.Die:
                    ChangeState(GetComponent<DieState>());
                    break;
                case currentStateType.SetTarget:
                    ChangeState(GetComponent<TargetState>());
                    break;
           
            }

            previousStateType = currentStateType;
        }
    
    }


}
