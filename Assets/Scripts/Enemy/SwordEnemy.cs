using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : EnemyBase
{

    public ParticleSystemController AttackparticleController;
    public override void Attack()
    {
        base.Attack();

        AttackparticleController.Play();
    }
}
