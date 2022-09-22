using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]

[RequireComponent(typeof(Rigidbody))]


public class StateChangere : MonoBehaviour
{

    public currentStateType state;

   public UnityEvent<AiBase> OnAIbaseComing;

    private BoxCollider boxCollider;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        AiBase aiBase = other.GetComponent<AiBase>();

        if (aiBase != null)
        {
            aiBase.GetStateManager().currentStateType = state;
            OnAIbaseComing?.Invoke(aiBase); 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        AiBase aiBase = other.GetComponent<AiBase>();

        if (aiBase != null)
        {
            if (aiBase.GetStateManager().currentStateType == currentStateType.SetTarget)
            {
                aiBase.GetStateManager().currentStateType = state;
                OnAIbaseComing?.Invoke(aiBase);
            }
     
        }
    }
}
