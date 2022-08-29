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
        enemyBase.setDieAnimiton(true);

        MeshCreator meshCreator = MasterManager.Instance.enemyManager.GetComponent<MeshCreator>();

        for (int i = 0; i < meshCreator.listGroup.groupes.Length; i++)
        {
            for (int j = 0; j < meshCreator.listGroup.groupes[i].objects.Length; j++)
            {
                if (meshCreator.listGroup.groupes[i].objects[j] == enemyBase.transform)
                {
                    meshCreator.listGroup.groupes[i].objects[j] = null;
                }
            }
        }
    }

    public override void OnExit()
    {
        
    }

    public override void OnStay()
    {
        
    }
}
