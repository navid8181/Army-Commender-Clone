using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{


    public abstract void OnEnter();

    public abstract void OnStay();

    public abstract void OnExit();
}
