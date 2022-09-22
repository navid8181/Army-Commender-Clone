using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(PlayerController))]

[RequireComponent(typeof(TriggerDetection))]
public class Player : MonoBehaviour,IDamageable, ICollisonable
{
    [SerializeField] private JoyStick joyStick;
    [SerializeField] private float moveSpeed = 2.5f;

    [SerializeField] private float rotationSpeed = 2.5f;

    [SerializeField] private new Camera camera;

    [SerializeField] private CoinChest coinChest;

    [SerializeField] private OptimizeCoinChest optimizeCoinChest;

    [SerializeField] private ParticleSystemController AttackparticleSystemController;
    [SerializeField] private ParticleSystemController FootStepparticleSystemController;

    private PlayerController PlayerController;

    private TriggerDetection triggerDetection;


    private AIDistribution aIDistribution;

    public float Health { get; set; }

    public StatusBar healthbar;

    public bool isMoveing() => joyStick.getRawInput().sqrMagnitude >= 0.01f * 0.01f;


    private EnemyBase targetToAttack;
    public LayerMask enemyLayermask;
    public float radiusEnemyFinder = 5;

    public float timeToAttack = 2;
    public float collisionRadius = 2;
    private Timer timer;

    public bool CanMove { get; set; } = true;


    public Camera getCamera() => camera;

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();

        triggerDetection = GetComponent<TriggerDetection>();

        triggerDetection.OnTriggerEnterDetection.AddListener(OntriggerEnter);
        aIDistribution = GetComponent<AIDistribution>();

       

        Health = 100;
        healthbar.SetFill(Health / 100);
        timer = new Timer(timeToAttack);
    }

    private void OntriggerEnter(Collider col)
    {

        Coin coin = col.GetComponent<Coin>();

        if (coin != null)
        {

            

            optimizeCoinChest.SetDistribut(coin);


            coin.SetAvtiveCollider(false);

          


        }
    }

    private void Update()
    {




        FindEnemy();

        if (targetToAttack != null)
        {
            timer.Init(() =>
            {
                targetToAttack.ApplyDamage(55);
                PlayerController.SetBoolAnimiton("Attack", true);
                AttackparticleSystemController.Play();
            });
  

        }
        else{
            timer.ResetValue();
        }
    }

    private void FixedUpdate()
    {


        if (!CanMove)
        {
            PlayerController.SetFloatAnimiton("Velocity", 0);
            FootStepparticleSystemController.SetStartLifeTime(0);
            FootStepparticleSystemController.SetAvtive(false);
            return;
        }
        Vector2 joysStickPos = joyStick.getInput();

        Vector3 direMove = new Vector3(joysStickPos.x, 0, joysStickPos.y);


        Vector3 rightCam = camera.transform.right;
        rightCam.y = 0;

        Vector3 forwardCam = camera.transform.forward;
        forwardCam.y = 0;


        Vector3 dire = rightCam * direMove.x + forwardCam * direMove.z;


        dire.Normalize();




        PlayerController.SetBoolAnimiton("Moving", joysStickPos.magnitude != 0);



        float velocity = (joysStickPos.magnitude > 1) ? joysStickPos.normalized.magnitude : joysStickPos.magnitude;


        FootStepparticleSystemController.SetStartLifeTime(velocity * 0.5f);
        FootStepparticleSystemController.SetAvtive(velocity >= 0.2f);

        PlayerController.SetFloatAnimiton("Velocity", velocity);


        PlayerController.Move(dire, moveSpeed * (velocity), rotationSpeed);
    }
    public void FindEnemy()
    {
        if (Health <= 0)
        {
            targetToAttack = null;
            return;
        }
        //if (targetToAttack != null) return;

        Collider[] col = Physics.OverlapSphere(transform.position, radiusEnemyFinder, enemyLayermask);

        if (col.Length == 0 )
        {
            targetToAttack = null;

        }

        for (int i = 0; i < col.Length; i++)
        {
            if (Health <= 0) return;
            EnemyBase enemyBase = col[i].GetComponent<EnemyBase>();
            if (enemyBase != null)
            {
                targetToAttack = enemyBase;

              //  enemyBase.AddTarget(transform);
            }
            else
            {
                break;
            }
       
                
            
           
            //if (enemyBase.targets.Count > 0 && i +1 >=col.Length)
            //{
            //    SetAttackTarget(enemyBase);
            //}
        }
        if (col.Length > 0)
        {


        }
    }
    public void ApplyDamage(float damage)
    {
        Health -= damage/4;

        healthbar.SetFill(Health / 100);
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position, radiusEnemyFinder);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }

    public float getCollisionRadius()
    {
        return collisionRadius;
    }
}
