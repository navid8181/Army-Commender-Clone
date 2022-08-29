using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent (typeof(EnemyStateManager))]
public class EnemyBase : MonoBehaviour, IDamageable
{

    public List<Transform> targets = new List<Transform>();

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

        targets[0].GetComponent<IDamageable>().ApplyDamage(50);
    
    }

    public void setVelocity(int value) { velocity = Mathf.Lerp(velocity, value, Time.deltaTime); }
    public float getVelocity() =>  velocity;
    
    public void SetMoveAnim(bool value)
    {
        playerController.SetBoolAnimiton("Moving",value);
    }

    public float disTotarget()
    {
        Vector3 targetPos = averageOfTargets();

        targetPos.y = 0;

        Vector3 enemyPos = transform.position;
        enemyPos.y = 0;

        return Vector3.Distance(targetPos, enemyPos);
    }

    public void setDieAnimiton(bool value)
    {
        playerController.SetBoolAnimiton("isdie", value);
    }
    public void Move()
    {
        if (targets.Count <= 0) return;
        Vector3 targetPos = averageOfTargets();


        distance = disTotarget();

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

        if (targets.Count == 0) return sum;

        //for (int i = 0; i < targets.Count; i++)
        //{
        //    sum += targets[i].transform.position;
        //}

        Vector3 avverage = targets[0].transform.position;
        return avverage;
        //avverage.y = 0;

        //Vector3 enemyPos = transform.position;
        //enemyPos.y = 0;


        //distance = Vector3.Distance(avverage, enemyPos);

  
    }


    public void AddTarget(Transform aiBase) {
    
        if (targets.Contains(aiBase)) return;

        targets.Add(aiBase);
    
    }
    public void RemoveTarget(Transform aiBase) => targets.Remove(aiBase);




    public void ApplyDamage(float damage)
    {
       Health -= damage;
    }


    public void DisableAvatar()
    {
        targets.Clear();
        playerController.Disable();
        GetComponent<Collider>().enabled = false;
        
    }
}
