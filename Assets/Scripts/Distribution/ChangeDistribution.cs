using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(StatusBar))]
public class ChangeDistribution : MonoBehaviour
{

    private Rigidbody _rigidbody;

    private StatusBar helthBarController;

    public DistributionBase from;

    private DistributionBase to;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;

        helthBarController = GetComponent<StatusBar>();
    }



    private void OnTriggerStay(Collider other)
    {
        if (to == null)
         to = other.GetComponent<AIDistribution>();

        if (to == null ) return; 

        Player player = other.GetComponent<Player>();

        if (player == null) return;

        if (player.isMoveing()) return;

        helthBarController.SetFill(1.0f);

        helthBarController.OnStatusBarCompleate.AddListener(() =>
        {
            //Debug.Log(to.CurrentDistribuionSize + " player max Size");
            from.ChangeDistribution(to);
        });
         
        
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
