using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDistrubtionManager : DistributionBase
{
    public int with = 4, height = 4;
    public Vector3 offset = Vector3.one;

    public Item[] items;

    public bool isremove = false;
    private void Start()
    {
        for (int i = 0; i < items.Length; i++)
        {
            SetDistribut(items[i]);
        }
    }

    private void Update()
    {
        if (isremove)
        {
            isremove = false;

            for (int i = GetDistributables().Length -1; i > 0; i--)
            {
                RemoveDistribut(GetDistributables()[i]);
            }
        }
    }
    public override void ExeCuteDistribute(int i)
    {
        if (IsUpdatingIndex) return;
        Utility.CubicDistribute(GetDistributables(), transform, with, height, offset, i);
    }
}
