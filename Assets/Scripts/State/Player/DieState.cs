using System.Collections;
using System.Collections.Generic;
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
        aiPlayer.targetToAttack.targets.Clear();
        aiPlayer.targetToAttack = null;
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
        }
        else
        {
            timer.Init(() =>
            {
                aiPlayer.currentDistribution.RemoveDistribut(aiPlayer);
                aiPlayer.currentDistribution = null;
                MasterManager.Instance.PoolManager.BackToPool(aiPlayer.gameObject);
            });
        }
    }
}
