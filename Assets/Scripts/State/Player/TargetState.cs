using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetState : State
{
    private AiBase aiPlayer;

    public Transform target;

   // private WeaponeStore weaponStore;
    private void Awake()
    {
        aiPlayer = GetComponent<AiBase>();
    }
    public override void OnEnter()
    {
       // if (weaponStore == null)
           // weaponStore = FindObjectOfType<WeaponeStore>();
      //  if (target == null && weaponStore != null)
          //  target = weaponStore.GetComponentInChildren<StateChangere>().transform;
        if (target != null)
            aiPlayer.SetTraget(target.position);
        else
        {
            aiPlayer.GetStateManager().currentStateType = currentStateType.FollowTargetState;
            return;
        }
            

      //  if (weaponStore.itemDistrubtionManager.GetDistributables().Length <=0 && weaponStore.coineDistributeManager.GetDistributables().Length<=0)
      //  {
           // aiPlayer.GetStateManager().currentStateType = currentStateType.FollowTargetState;
          //  return;
       // }


    }

    public override void OnExit()
    {
        
    }

    public override void OnStay()
    {
        aiPlayer.FallowTarget();
    }
}
