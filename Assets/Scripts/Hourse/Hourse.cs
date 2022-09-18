using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourse : AiBase
{

    public override void Awake()
    {
        base.Awake();
        Health = 100;
    }


    private void Start()
    {
        Health = 100;
    }

    private void OnEnable()
    {

    }

    public override void Attack()
    {
     
    }


    public override void StopAttack()
    {
       
    }


}
