using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyIdleState))]
[RequireComponent(typeof(EnemyFolowState))]
[RequireComponent(typeof(EnemyAttackState))]
[RequireComponent(typeof(EnemyDieState))]


public class EnemyStateManager : BaseStateManager
{

    public currentStateType currentStateType = currentStateType.IdleState;

    private currentStateType previousStateType = currentStateType.FollowTargetState;


    private void Start()
    {
        currentState = GetComponent<EnemyIdleState>();
   

       
    }

    private void Update()
    {
        if (currentStateType != previousStateType)
        {
            switch (currentStateType)
            {
                case currentStateType.IdleState:
                    ChangeState(GetComponent<EnemyIdleState>());
                    break;
                case currentStateType.FollowTargetState:
                    ChangeState(GetComponent<EnemyFolowState>());
                    break;
                case currentStateType.Attack:
                    ChangeState(GetComponent<EnemyAttackState>());
                    break;

                    case currentStateType.Die:
                    ChangeState(GetComponent<EnemyDieState>());
                    break;
                default:
                    break;
            }

            previousStateType = currentStateType;
        }

    }

}
