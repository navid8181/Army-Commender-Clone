using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent (typeof(EnemyStateManager))]
public class EnemyBase : MonoBehaviour, IDamageable
{

    public List<AiBase> targets = new List<AiBase>();

    public float distanceStopToAttack = 1;

   
    private PlayerController playerController;

    public float speed = 3,rotationSpeed = 2;

    public float maxDistanceToStop = 2;

    public float brackDistance = 1;

    float distance = 0;

    float velocity = 0;

     private EnemyStateManager enemyStateManager;

    public float Health { get; set; }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        enemyStateManager = GetComponent<EnemyStateManager>();

        Health = 100;
    }

    public EnemyStateManager GetEnemyStateManager() => enemyStateManager;

    public virtual void Attack() {


        playerController.SetBoolAnimiton("Attack", true);
    
    }

    public void setVelocity(int value) { velocity = Mathf.Lerp(velocity, value, Time.deltaTime); }
    public float getVelocity() =>  velocity;
    
    public void SetMoveAnim(bool value)
    {
        playerController.SetBoolAnimiton("Moving",value);
    }



    public void Move()
    {
        if (targets.Count <= 0) return;

        Vector3 targetPos = averageOfTargets();

        velocity = distance / maxDistanceToStop;

        velocity = Mathf.Clamp01(velocity); 

        Vector3 dire =    targetPos - transform.position;

        dire.y = 0;

        dire.Normalize();

        playerController.SetBoolAnimiton("Moving", distance > brackDistance * 2);
        playerController.SetFloatAnimiton("Velocity", velocity);

     

        playerController.Move(dire, speed * velocity, rotationSpeed);
    }

    public Vector3 averageOfTargets()
    {
        Vector3 sum = Vector3.zero;
        distance = 0;
        if (targets.Count == 0) return sum;

        //for (int i = 0; i < targets.Count; i++)
        //{
        //    sum += targets[i].transform.position;
        //}

        Vector3 avverage = targets[0].transform.position;

        avverage.y = 0;

        Vector3 enemyPos = transform.position;
        enemyPos.y = 0;


        distance = Vector3.Distance(avverage, enemyPos);

        return avverage;
    }


    public void AddTarget(AiBase aiBase) => targets.Add(aiBase);
    public void RemoveTarget(AiBase aiBase) => targets.Remove(aiBase);


    public float disTotarget() => distance;

    public void ApplyDamage(float damage)
    {
       Health -= damage;
    }
}
