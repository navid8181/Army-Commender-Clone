using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinZone : MonoBehaviour
{

    public float minRadius = 1;
    public float maxRadius = 10;


    [Range(-180f, 180f)] public float minAngle = -180;
    [Range(-180f, 180f)] public float maxAngle = 180;

    public bool isGold;

    public float totalInstance = 10;

    private void Start()
    {
        string name = isGold ? PoolManager.goldCoin : PoolManager.ironCoin;

        CoinInstancer coinInstancer = new CoinInstancer(name, minRadius, maxRadius, minAngle, maxAngle);

        for (int i = 0; i < totalInstance; i++)
        {
            coinInstancer.Clone(transform.position);
        }

        coinInstancer.Dispose();
    }







    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawCube(transform.position, Vector3.one);

        Gizmos.DrawWireSphere(transform.position, minRadius);
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    }

}
