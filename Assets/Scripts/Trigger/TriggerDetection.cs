using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TriggerDetection : MonoBehaviour
{
    public UnityEvent<Collider> OnTriggerEnterDetection;

    private void Awake()
    {
       // OnTriggerEnterDetection = new UnityEvent<Collider>();
    }





    private void OnTriggerEnter(Collider other)
    {

        

        OnTriggerEnterDetection?.Invoke(other);
    }

}
