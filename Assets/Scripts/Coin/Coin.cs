using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IDistributable
{

    private Collider _collider;


    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }


    public void SetAvtiveCollider (bool value) => _collider.enabled = value;

    public int DistributIndex { get; set; }
    public DistributionBase currentDistribution { get; set; }

    public void SetTraget(Vector3? target)
    {
        transform.position = target.GetValueOrDefault();
    }


    private void Update()
    {
        if (currentDistribution != null)
        {
            currentDistribution.ExeCuteDistribute(DistributIndex);
        }
    }
}
