using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWeaponeTargetDistribution : SetTargetDistribution
{
    public WeaponeStore weaponeStore;
    public override bool BlockConditon(AiBase aiBase)
    {
        bool hasWeapone = aiBase.indexOfWeapone + 1 >= aiBase.weapones.Length;

        bool hasAmmo = weaponeStore.itemDistrubtionManager.GetDistributables().Length <= 0;

       

        return hasAmmo || hasWeapone;

    }

}
