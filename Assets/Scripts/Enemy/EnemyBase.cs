using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent (typeof(EnemyStateManager))]
public class EnemyBase : MonoBehaviour, IDamageable,ICollisonable
{

    public Transform target;

    public float distanceStopToAttack = 1;

   
    public PlayerController playerController { get; private set; }

    public float speed = 3,rotationSpeed = 2;

    public float maxDistanceToStop = 2;

    public float brackDistance = 1;

    public float collisionRadius = 0.7f;

    public Weapone weapone;

    float distance = 0;

    float velocity = 0;

     private EnemyStateManager enemyStateManager;

    public float Damge = 10;


    private Coroutine Damgecoroutine;


    public ParticleSystemController FootStepparticleController;

    public MusicController weaponeMusicController;

    public float Health { get; set; }
    public bool CanMove { get; set; } = true;

    

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        enemyStateManager = GetComponent<EnemyStateManager>();

        Health = 150;
    }

    public EnemyStateManager GetEnemyStateManager() => enemyStateManager;

   
    public virtual void Attack() {

        if (target == null)
        {
            if(Damgecoroutine != null)
            {
                StopCoroutine(Damgecoroutine);
            }
            return;
        }

        playerController.SetBoolAnimiton("Attack", true);

        IDamageable idamges = target.GetComponent<IDamageable>();



        if(idamges != null)
        {
          Damgecoroutine =   this.wait(0.5f, () =>
            {
                idamges?.ApplyDamage(Damge);
            });
      
        }
        

        Damge = weapone.damge;
    }

    public void setVelocity(int value) { velocity = Mathf.Lerp(velocity, value, Time.deltaTime); }
    public float getVelocity() =>  velocity;
    
    public void SetMoveAnim(bool value)
    {
        playerController.SetBoolAnimiton("Moving",value);
    }
    public void SetVelocityAnim(float value)
    {
        playerController.SetFloatAnimiton("Velocity", value);

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
    public void Move(Vector3 targetPos)
    {

        if (!CanMove)
        {
            velocity = 0;
            FootStepparticleController.SetStartLifeTime(0);
            FootStepparticleController.SetAvtive(false);
            return;

        }
      

        //if (target == null) return;

        Vector3 tempTargetPos = targetPos;
        tempTargetPos.y = 0;
        Vector3 playerPos = transform.position;
        playerPos.y = 0;

        distance = Vector3.Distance(playerPos, tempTargetPos);

        float t = Mathf.InverseLerp(0, maxDistanceToStop, distance);

        velocity = Mathf.Lerp(0, 1, t);

       // velocity = Mathf.Clamp01(velocity); 

        Vector3 dire =    targetPos - transform.position;

        dire.y = 0;

        dire.Normalize();


        FootStepparticleController.SetStartLifeTime(velocity * 0.5f);
        FootStepparticleController.SetAvtive(velocity >= 0.15f);

        playerController.SetBoolAnimiton("Moving", distance >= brackDistance );
        playerController.SetFloatAnimiton("Velocity", velocity);

     

        playerController.Move(dire, speed * velocity, rotationSpeed);
    }

    public Vector3 averageOfTargets()
    {
        Vector3 sum = Vector3.zero;

        if (target == null) return sum;

        //for (int i = 0; i < targets.Count; i++)
        //{
        //    sum += targets[i].transform.position;
        //}

        Vector3 avverage = target.transform.position;
        return avverage;
        //avverage.y = 0;

        //Vector3 enemyPos = transform.position;
        //enemyPos.y = 0;


        //distance = Vector3.Distance(avverage, enemyPos);

  
    }


    public void AddTarget(Transform aiBase) {

        target = aiBase;
    }





    public void ApplyDamage(float damage)
    {
       Health -= damage;
    }


    public void DisableAvatar()
    {
        target = null;
        playerController.Disable();
        GetComponent<Collider>().enabled = false;
        FootStepparticleController.Stop();
        
    }
    private void OnDrawGizmos()
    {



        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }
    public float getCollisionRadius()
    {
        return collisionRadius;
    }
}
