using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateManager : MonoBehaviour
{

    protected State currentState;


    protected State previousState;

    private void FixedUpdate()
    {
        if (currentState != previousState)
        {
            if (previousState != null)
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
