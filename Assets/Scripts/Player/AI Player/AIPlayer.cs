using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class AIPlayer : AiBase, IArrow
{



    [SerializeField]
    private GameObject arrow;


    public override void Update()
    {
        base.Update();
        if (targetToAttack == null && CanMove)
        {
            weaponeParticleSystemControllers[indexOfWeapone].Stop();
        }
    }

    public void EnableArrow()
    {
        if (indexOfWeapone == 1)
        {
            weaponeParticleSystemControllers[indexOfWeapone].Stop();
        }
        arrow.SetActive(true);
    }

    public void DisableArrow()
    {


        if (indexOfWeapone == 1)
        {
            if (targetToAttack != null)
                weaponeParticleSystemControllers[indexOfWeapone].Play();
        }
        arrow.SetActive(false);
    }
    private void FixedUpdate()
    {



    }


    public override void Attack()
    {
        base.Attack();


    }


}
