using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(StateManager))]
public abstract class AiBase : MonoBehaviour, IDistributable, IDamageable
{

    private PlayerController playerController;

    public Vector3? target = null;


    NavMeshPath navMeshPath;

    private Vector3[] corners;

    public bool isMove;

    public NavMeshPathStatus PathStatus;

    [SerializeField] public float brackDistance = 0.1f;
    [SerializeField] protected float moveSpeed = 2.5f;
    [SerializeField] protected float rotationSpeed = 2.5f;

    [SerializeField] protected float distanceToStop = 2;
    float velocity;


    public Weapone[] weapones;
    public Transform[] weaponesModel;
    public int indexOfWeapone = 0;

    private int previousIndexOfWeapone = -1;

    public float maxDistanceToAttack = 1;

    private float aIRadius;

    private bool stopFast = false;


    private AiBase currentCollisionAiBase;

    public float getAiRadius() => aIRadius;


    public EnemyBase targetToAttack;

    private StateManager stateManager;
    public int DistributIndex { get; set; }
    public DistributionBase currentDistribution { set; get; }

    public float radiusEnemyFinder = 5;

    public float Health { get; set; }

    public LayerMask enemyLayermask;

    [HideInInspector]
    public float timeToAttack = 0;
    public float damge = 0;
    public virtual void Attack()
    {

        playerController.SetBoolAnimiton("Attack", true);

        if(targetToAttack != null)

        targetToAttack.ApplyDamage(damge);

    }
    public void SetAttackTarget(EnemyBase target)
    {
        target.AddTarget(this.transform);


        targetToAttack = target;
    }

    private void Awake()
    {


        stateManager = GetComponent<StateManager>();

        playerController = GetComponent<PlayerController>();
        navMeshPath = new NavMeshPath();

        aIRadius = brackDistance = GetComponent<CapsuleCollider>().radius + 0.25f;

        InitializeWapone();


    }

    public void SetBoolAnim(bool value) { playerController.SetBoolAnimiton("Moving", value); }

    public void InitializeWapone()
    {
        if (previousIndexOfWeapone == indexOfWeapone) return;



        Weapone weapone = weapones[indexOfWeapone];

        maxDistanceToAttack = weapone.maxDistanceToAttack;

        timeToAttack = weapone.timeToAttack;

        damge = weapone.damge;

        for (int i = 0; i < weaponesModel.Length; i++)
        {
            weaponesModel[i].gameObject.SetActive(i == indexOfWeapone);
        }

        previousIndexOfWeapone = indexOfWeapone;

    }

    private void Update()
    {
        InitializeWapone();
      //  FindEnemy();



        if (stopFast)
        {
            playerController.SetBoolAnimiton("Moving", false);
            playerController.SetFloatAnimiton("Velocity", 0);

            stopFast = Vector3.Distance(currentCollisionAiBase.getTargetPos().Value, getTargetPos().Value) >= (aIRadius * 2) + 0.1f;



        }


    }
    public void FallowTarget()
    {

        Vector3 startPos = transform.position;

        if (target == null || stopFast) return;




        bool canMove = NavMesh.CalculatePath(startPos + Vector3.up, target.GetValueOrDefault(Vector3.zero), NavMesh.AllAreas, navMeshPath);

        if (!canMove || (PathStatus == NavMeshPathStatus.PathInvalid || PathStatus == NavMeshPathStatus.PathPartial))
        {


            //TDO Fix This Optimizition

            if (NavMesh.SamplePosition(transform.position, out NavMeshHit navMeshHit, 100, -1))
            {
                Vector3 pos = navMeshHit.position;

                startPos = pos;
                // playerController.Move(dire, moveSpeed , rotationSpeed );

                // transform.position = pos;
            }



            canMove = NavMesh.CalculatePath(startPos + Vector3.up, target.GetValueOrDefault(Vector3.zero), NavMesh.AllAreas, navMeshPath);

        }




        PathStatus = navMeshPath.status;


        corners = navMeshPath.corners;


        if (corners.Length >= 2 && !stopFast)
        {

            Vector3 pos = corners[1];


            Vector3 dire = pos - transform.position;
            dire.y = 0;

            if (corners.Length == 2)
            {
                float remeaningDistance = Vector3.Distance(corners[0], corners[1]);
                if (remeaningDistance <= 0.01f)
                {
                    dire = Vector3.zero;
                }
            }


            dire.Normalize();

            Debug.DrawRay(transform.position, dire * 10, Color.red);

            pos.y = 0;
            Vector3 playerPos = transform.position;
            playerPos.y = 0;

            float dis = distanceToTarget();


            float t = dis / (distanceToStop + aIRadius);

            // if (t >= 0.99f) t = 1;
            // if (t <= 0.01f) t = 0;

            t = Mathf.Clamp01(t);


            velocity = Mathf.Lerp(0, 1, t);


            isMove = dis >= brackDistance;


            playerController.SetFloatAnimiton("Velocity", velocity);

            playerController.SetBoolAnimiton("Moving", dis >= brackDistance);
            playerController.Move(dire, moveSpeed * t, rotationSpeed);
        }



        if (corners.Length == 0 || stopFast)
        {
            playerController.SetBoolAnimiton("Moving", false);
            playerController.SetFloatAnimiton("Velocity", 0);
        }
    }


    public StateManager GetStateManager() => stateManager;

    public void Stop()
    {
        velocity = Mathf.Lerp(velocity, 0, Time.deltaTime * 2);

        playerController.SetFloatAnimiton("Velocity", velocity);

        if (velocity <= 0.01f)
            playerController.SetBoolAnimiton("Moving", false);

      
    }

    public float distanceToTarget()
    {
        float dis = 0;


        dis = Vector3.Distance(transform.position, target.GetValueOrDefault());
        //for (int i = 1; i < corners.Length; i++)
        //{
        //    Vector3 pos = corners[i];

        //    pos.y = 0;

        //    Vector3 playerPos = transform.position;

        //    playerPos.y = 0;

        //    dis += Vector3.Distance(playerPos, pos);
        //}

        return dis;
    }


    public void SetTraget(Vector3? target)
    {

        if (NavMesh.SamplePosition(target.GetValueOrDefault(Vector3.zero), out NavMeshHit navMeshHit1, 100, -1))
        {
            target = navMeshHit1.position;



        }



        this.target = target;

    }
    public Vector3? getTargetPos() => target;


 


    private void OnCollisionStay(Collision collision)
    {
        StopFastChecker(collision);
    }

    private void StopFastChecker(Collision collision)
    {
        AiBase ai = collision.collider.GetComponent<AiBase>();


        if (ai != null && !stopFast)
        {


            if (!ai.isMove && ai.currentCollisionAiBase == null)
            {
                if (Vector3.Distance(ai.getTargetPos().Value, getTargetPos().Value) <= (aIRadius * 2) + 0.1f)
                {
                    stopFast = true;
                    currentCollisionAiBase = ai;



                }
                else
                {
                    stopFast = false;
                    currentCollisionAiBase = null;
                }
            }
        }
    }

    public void DisableAvatar()
    {
        targetToAttack = null;
        playerController.Disable();
        GetComponent<Collider>().enabled = false;

    }

    public void EnableAvatar()
    {

        playerController.Enable();
        GetComponent<Collider>().enabled = true;

    }
    public void ApplyDamage(float damage)
    {
        Health -= damage;
    }

    public void InitilizeOnStatup()
    {
        EnableAvatar();
        Health = 150;
        setDieAnimiton(false);

    }
    public void setDieAnimiton(bool value)
    {
        playerController.SetBoolAnimiton("isdie", value);
    }
    public void FindEnemy()
    {

        if (targetToAttack != null) return;

        Collider[] col = Physics.OverlapSphere(transform.position, radiusEnemyFinder, enemyLayermask);

        for (int i = 0; i < col.Length; i++)
        {
            if (Health <= 0) return;
            EnemyBase enemyBase = col[i].GetComponent<EnemyBase>();

           // if (enemyBase.targets.Count <= 0)
           // {
                SetAttackTarget(enemyBase);
            //}

            //if (enemyBase.targets.Count > 0 && i +1 >=col.Length)
            //{
            //    SetAttackTarget(enemyBase);
            //}
        }
        if (col.Length > 0)
        {


        }
    }
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position, radiusEnemyFinder);

        Gizmos.DrawSphere(target.GetValueOrDefault(), 0.1f);
    }
}

