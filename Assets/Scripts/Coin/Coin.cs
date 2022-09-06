using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinType { goldCoin, ironCoin }
public class Coin : MonoBehaviour, IDistributable
{

    public CoinType coinType;

    public float coinDistributeSpeed = 40;
    private Collider _collider;

    public Vector3 NoneDistributeTarget { get; set; }

    public AnimationCurve moveNoneDistribute;

    public float speed = 1, upY = 3;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }


    public void SetAvtiveCollider(bool value) => _collider.enabled = value;

    public int DistributIndex { get; set; }
    public DistributionBase currentDistribution { get; set; }

    public void SetTraget(Vector3? target)
    {

        Vector3 coinPos = transform.position;

        Vector3 targtPos = target.GetValueOrDefault();

        Vector3 dire = coinPos - targtPos;
        dire.y = 0;

        float dis = dire.magnitude;


        coinPos.y = Mathf.Lerp(coinPos.y, targtPos.y, coinDistributeSpeed * Time.deltaTime);

        float normalValue = DistributIndex / (float)currentDistribution.GetDistributables().Length;

        coinPos.x = Mathf.Lerp(coinPos.x, targtPos.x, coinDistributeSpeed * Time.deltaTime * ((1 - normalValue) + 0.1f) * (dis + 1));

        coinPos.z = Mathf.Lerp(coinPos.z, targtPos.z, coinDistributeSpeed * Time.deltaTime * ((1 - normalValue) + 0.1f) * (dis + 1));






        transform.position = coinPos;

    }


    private void FixedUpdate()
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

            coinPos.y = Mathf.Lerp(coinPos.y, (NoneDistributeTarget.y + moveNoneDistribute.Evaluate(Mathf.Clamp01((dis / 10))) * upY), Time.deltaTime * speed);

            coinPos.z = Mathf.Lerp(coinPos.z, NoneDistributeTarget.z, Time.deltaTime * speed);

            transform.position = coinPos;

        }
    }
}
