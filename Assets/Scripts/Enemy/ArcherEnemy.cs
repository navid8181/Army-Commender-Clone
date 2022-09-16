using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : EnemyBase,IArrow
{

    [SerializeField] private GameObject Arrow;

    public ParticleSystemController AttackparticleController;
    public override void Attack()
    {
        base.Attack();
    }

    private void Update()
    {
       if (target == null)
            AttackparticleController.Stop();
    }

    public void DisableArrow()
    {
      Arrow.SetActive(false);
        if (target != null)
            AttackparticleController.Play();
    }

    public void EnableArrow()
    {
        AttackparticleController.Stop();
        Arrow.SetActive(true);
    }
}
