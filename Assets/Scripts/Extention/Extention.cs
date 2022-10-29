using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Extention 
{
    public static Coroutine wait(this MonoBehaviour monoBehaviour, float duration, Action execution)
    {

        return monoBehaviour.StartCoroutine(ExeCutionFunction(duration, execution));

         
    }


    static IEnumerator ExeCutionFunction(float time, Action execution)
    {
        yield return new WaitForSecondsRealtime(time);
        execution();    
    }
}
