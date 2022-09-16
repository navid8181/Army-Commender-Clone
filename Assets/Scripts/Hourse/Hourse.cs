using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourse : AiBase
{
    private void Awake()
    {
        Health = 100;
    }

    private void OnEnable()
    {
        Health = 100;
    }

    public override void Attack()
    {
     
    }


    public override void StopAttack()
    {
       
    }


}
