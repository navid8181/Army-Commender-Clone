using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(IdleState))]
[RequireComponent(typeof(FollowTargetState))]
public class StateManager : MonoBehaviour
{
    public State currentState;


    private State previousState;

    private void Start()
    {
        previousState = currentState;

        currentState.OnEnter();
        
    }


    private void Update()
    {
        if (currentState != previousState)
        {
            previousState.OnExit();
            currentState.OnEnter();

            previousState = currentState;
        }
        else
        {
            currentState.OnStay();
        }
    }


    public void ChangeState(State stare)
    {
        currentState = stare; 
    }
}
