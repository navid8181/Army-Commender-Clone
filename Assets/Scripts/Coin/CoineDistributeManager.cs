using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoineDistributeManager : DistributionBase
{

    public CoinType coinType = CoinType.ironCoin;






    private void Start()
    {
        MaxDistrubutionSize = int.MaxValue;
    }

    public int with = 2, height = 2;
    public Vector3 offset = Vector3.one;


    public override bool Validition(IDistributable distributable)
    {
        return ((Coin)distributable).coinType == coinType;
    }

    public override void ExeCuteDistribute(int i)
    {
        Utility.CubicDistribute(GetDistributables(), transform, with, height, offset, i);
    }
}
