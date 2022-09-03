using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DieState : State
{

    private AiBase aiPlayer;

    public float timeBackPool = 2;

    private Timer timer;
    private void Awake()
    {
        aiPlayer = GetComponent<AiBase>();
        timer = new Timer(timeBackPool);
    }
    public override void OnEnter()
    {

      
      

        timer.ResetValue();
        if (aiPlayer.targetToAttack != null)
        {
          if (aiPlayer.targetToAttack.target != null)
            {
                aiPlayer.targetToAttack.target = null;
            }
        }


        aiPlayer.targetToAttack = null;
        aiPlayer.setDieAnimiton(true);
     aiPlayer.DisableAvatar();  
     
    }

    public override void OnExit()
    {

    }

    public override void OnStay()
    {
        if (aiPlayer .Health > 0)
        {
            aiPlayer.EnableAvatar();
            aiPlayer.GetStateManager().currentStateType = currentStateType.FollowTargetState;
            return;
        }
        else
        {
            timer.Init(() =>
            {
                AiBase aiBases = GetComponent<AiBase> ();    
               
                    aiPlayer.currentDistribution.RemoveDistribut(aiBases);
                    aiBases.currentDistribution = null;
                   
                
                MasterManager.Instance.PoolManager.BackToPool(aiPlayer.gameObject);
            });
        }
    }
}
