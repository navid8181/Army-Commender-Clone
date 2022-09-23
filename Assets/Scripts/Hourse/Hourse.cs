using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Hourse : AiBase
{

    public AiBase aibse;

    public Changeable<AiBase> changeableAibace;

    public Transform playerPos;


    

    public override void Awake()
    {
        base.Awake();
        changeableAibace = new Changeable<AiBase>(null);
        changeableAibace.onChangeValue += riderChangeValue;
        Health = 100;
    }

    private void riderChangeValue(AiBase lastValue, AiBase CurrentValue)
    {
        if (CurrentValue != null)
        {
            currentDistribution.RemoveDistribut(this);

            currentDistribution = CurrentValue.currentDistribution;

            CurrentValue.currentDistribution.RemoveDistribut(CurrentValue);

            currentDistribution.SetDistribut(this);
           // currentDistribution = CurrentValue.currentDistribution;

           



            CurrentValue.CanMove = false;

            // CurrentValue.transform.position = playerPos.position;




            //CurrentValue.currentDistribution = new DistributionBase();
        }
        else
        {
            if (lastValue != null)
            {
                lastValue.CanMove = true;
                lastValue.currentDistribution = currentDistribution;
                lastValue.currentDistribution.SetDistribut(lastValue);
              


            }
        }
    }

    public void SetRider(AiBase rider) =>changeableAibace.Value = rider;


    public override void Update()
    {
        base.Update();

        if (aibse != null )
        {

            if (Health > 0)
            {
                aibse.transform.position = playerPos.position;
                aibse.transform.rotation = playerPos.rotation;
            }
            else
            {
                //aibse.currentDistribution = currentDistribution;
                //  aibse.currentDistribution.EditDistribution(DistributIndex, aibse);
                aibse = null;
                //CanMove = true;
            }

         
        }
        else
        {
       
     
        }




        changeableAibace.Value = aibse;
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
     if (aibse != null)
        {
            aibse.Attack();

            float damage = weapones[indexOfWeapone].damge;

           targetToAttack.ApplyDamage(damage);
        }
    }


    public override void StopAttack()
    {
       
    }


}
