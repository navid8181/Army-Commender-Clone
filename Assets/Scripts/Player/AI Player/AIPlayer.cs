using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class AIPlayer : AiBase
{




    public GameObject arrow;



    public void EnableArrow()
    {
        arrow.SetActive(true);
    }

    public void DisableArrow() { arrow.SetActive(false); }
    private void FixedUpdate()
    {
                                                             
        

    }


    public override void Attack()
    {
        base.Attack();
 

    }
}
