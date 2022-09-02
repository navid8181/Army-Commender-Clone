using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinType { goldCoin,ironCoin}
public class Coin : MonoBehaviour, IDistributable
{

    public CoinType coinType;

    private Collider _collider;

    public Vector3 NoneDistributeTarget { get; set; }

    public AnimationCurve moveNoneDistribute;

    public float speed = 1,upY = 3;
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
        else
        {


            Vector3 coinPos = transform.position;

            Vector3 copyCoinPos = coinPos;
            copyCoinPos.y = 0;

            Vector3 copynone = NoneDistributeTarget;
            copynone.y = 0;

            float dis = Vector3.Distance(copyCoinPos, copynone);

            coinPos.x = Mathf.Lerp(coinPos.x, NoneDistributeTarget.x, Time.deltaTime * speed);

            coinPos.y = (NoneDistributeTarget.y + moveNoneDistribute.Evaluate(Mathf.Clamp01((dis / 10))) * upY);

            coinPos.z = Mathf.Lerp(coinPos.z, NoneDistributeTarget.z, Time.deltaTime * speed);

            transform.position = coinPos;

        }
    }
}
