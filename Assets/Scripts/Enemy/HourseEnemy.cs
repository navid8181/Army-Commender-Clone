using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourseEnemy : EnemyBase
{


    public EnemyBase enemyBase;

    public Transform playerPos;

    private Changeable<EnemyBase> changeableEnemy;


    private void Awake()
    {
        changeableEnemy = new Changeable<EnemyBase>(null);

        changeableEnemy.onChangeValue += OnEnemyChange;
    }

    private void Update()
    {
        changeableEnemy.Value = enemyBase;

        if (enemyBase != null)
        {
            enemyBase.SetMoveAnim(false);
 

            enemyBase.transform.position = playerPos.position;
            enemyBase.transform.rotation = playerPos.rotation;
        }

    }

    private void OnEnemyChange(EnemyBase lastValue, EnemyBase CurrentValue)
    {
        if (CurrentValue == null)
        {
     
        }
        else
        {
            CurrentValue.CanMove = false;

            distanceStopToAttack = CurrentValue.distanceStopToAttack;

            weaponeMusicController = CurrentValue.weaponeMusicController;

            Damge = enemyBase.Damge * 2;
        }
    }

    public override void Attack()
    {
        if (enemyBase == null) return;

            enemyBase.playerController.SetBoolAnimiton("Attack", true);
        enemyBase.weaponeMusicController.Play();
        IDamageable idamges = target.GetComponent<IDamageable>();

        if (idamges != null)
            idamges.ApplyDamage(Damge);
    }
}
