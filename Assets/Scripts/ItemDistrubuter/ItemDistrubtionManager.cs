using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDistrubtionManager : DistributionBase
{
    public int with = 4, height = 4;
    public Vector3 offset = Vector3.one;



    public bool isremove = false;
    private void Start()
    {
     
    }


    public override void ExeCuteDistribute(int i)
    {

        Utility.CubicDistribute(GetDistributables(), transform, with, height, offset, i);
    }
}
