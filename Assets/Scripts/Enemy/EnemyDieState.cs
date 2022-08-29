using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : State
{
    private EnemyBase enemyBase;

    private void Awake()
    {
        enemyBase = GetComponent<EnemyBase>();
    }
    public override void OnEnter()
    {
        for (int i = 0; i < enemyBase.targets.Count; i++)
        {
            enemyBase.RemoveTarget(enemyBase.targets[i]);
        }
      
        enemyBase.DisableAvatar();
        enemyBase.gameObject.SetActive(false);
    }

    public override void OnExit()
    {
        
    }

    public override void OnStay()
    {
        
    }
}
