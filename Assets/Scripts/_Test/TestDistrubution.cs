using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDistrubution : DistributionBase
{

    public AiBase aiBase;


    private void Start()
    {
        SetDistribut(aiBase);
    }

    public override void ExeCuteDistribute(int i)
    {
        Utility.RectangleDistribute(GetDistributables(), transform, 2, Vector3.zero, i);
    }
}
