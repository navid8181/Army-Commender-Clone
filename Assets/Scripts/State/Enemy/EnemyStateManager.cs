using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyIdleState))]
[RequireComponent(typeof(EnemyFolowState))]
[RequireComponent(typeof(EnemyAttackState))]


public class EnemyStateManager : BaseStateManager
{

    public currentStateType currentStateType;

    private currentStateType previousStateType;


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
                default:
                    break;
            }

            previousStateType = currentStateType;
        }

    }

}
