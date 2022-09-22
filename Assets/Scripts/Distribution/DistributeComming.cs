using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.Events;



[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class DistributeComming : MonoBehaviour
{

    public UnityEvent<IDistributable[]> idistributComming;


    private Rigidbody Rigidbody;
    private BoxCollider BoxCollider;


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.isKinematic = true;

        BoxCollider = GetComponent<BoxCollider>();
        BoxCollider.isTrigger = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            AIDistribution aIDistribution  = player.GetComponent<AIDistribution>();

            idistributComming?.Invoke(aIDistribution.GetDistributables());
        }

    }

}
