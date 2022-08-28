using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HelthBarController))]
public class ChangeDistribution : MonoBehaviour
{

    private Rigidbody _rigidbody;

    private HelthBarController helthBarController;

    public DistributionBase from;

    private DistributionBase to;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;

        helthBarController = GetComponent<HelthBarController>();
    }



    private void OnTriggerStay(Collider other)
    {
        if (to == null)
         to = other.GetComponent<AIDistribution>();

        if (to == null) return; 

        helthBarController.SetFill(1.0f);

        if (helthBarController.fill  >= 1)
        {
            Debug.Log(to.CurrentDistribuionSize + " player max Size");
            from.ChangeDistribution(to);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        to = null;
        helthBarController.SetFill(0.0f);
    }


    void Update()
    {
        
    }
}
