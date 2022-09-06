using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : EnemyBase,IArrow
{

    [SerializeField] private GameObject Arrow;
    public override void Attack()
    {
        base.Attack();
    }

    public void DisableArrow()
    {
      Arrow.SetActive(false);
    }

    public void EnableArrow()
    {
     Arrow.SetActive(true);
    }
}
