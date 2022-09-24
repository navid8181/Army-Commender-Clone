using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHourseTargetDistribution : SetTargetDistribution
{

    public HourseSite HourseSite;
    public override bool BlockConditon(AiBase aiBase)
    {

        if ((aiBase is Hourse)){
            Debug.Log("ai base is hourse");
            return true;
        }
     


        bool getEnophHourse = HourseSite.aIDistribution.GetDistributables().Length <= 0;

        bool canMove  =  !aiBase.CanMove;

        return  canMove || getEnophHourse;
    }
}
