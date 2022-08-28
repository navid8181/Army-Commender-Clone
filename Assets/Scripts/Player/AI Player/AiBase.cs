using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerController))]
public class AiBase : MonoBehaviour,IDistributable
{



    private PlayerController playerController;


    //public Transform t3;


    public Vector3? target = null;


    NavMeshPath navMeshPath;

    private Vector3[] corners;

    public bool isMove;

    public NavMeshPathStatus PathStatus;

    [SerializeField] protected float brackDistance = 0.1f;
    [SerializeField] protected float moveSpeed = 2.5f;
    [SerializeField] protected float rotationSpeed = 2.5f;

    [SerializeField] protected float distanceToStop = 2;
    float velocity;

    private float aIRadius;

    private bool stopFast = false;


    private AiBase currentCollisionAiBase;







    public int DistributIndex { get; set; }
    public DistributionBase currentDistribution { set;  get; }

    // public void SetCurrentIndex(int i) => currentAiIndex = i;
    //public int getCurrentIndex() => currentAiIndex;

  


    private void Awake()
    {

        

        playerController = GetComponent<PlayerController>();
        navMeshPath = new NavMeshPath();

      aIRadius =  brackDistance = GetComponent<CapsuleCollider>().radius + 0.25f;

    }

    private void Update()
    {


        //if (currentDistribution != previousAIDistribution)
        //{
        //    if (previousAIDistribution != null)
        //    previousAIDistribution.RemoveDistribut(this);

        //    currentDistribution.SetDistribut(this);


        //    previousAIDistribution = (AIDistribution)currentDistribution;
        //}


       if (stopFast)
        {
            playerController.SetBoolAnimiton("Moving", false);
            playerController.SetFloatAnimiton("Velocity", 0);

          stopFast =  Vector3.Distance(currentCollisionAiBase.getTargetPos().Value, getTargetPos().Value) >= (aIRadius * 2) + 0.1f;



        }
    }
    protected void FallowTarget()
    {

        Vector3 startPos = transform.position;

        if (target == null ||stopFast ) return;

   


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

            Debug.DrawRay(transform.position, dire * 10,Color.red);

            pos.y = 0;
            Vector3 playerPos = transform.position;
            playerPos.y = 0;

            float dis = distanceToTarget(corners);


            float t = dis / (distanceToStop + aIRadius);

           // if (t >= 0.99f) t = 1;
           // if (t <= 0.01f) t = 0;

            t = Mathf.Clamp01(t);


            velocity = Mathf.Lerp(0, 1, t);


            isMove = dis >= brackDistance;


            playerController.SetFloatAnimiton("Velocity", velocity);

            playerController.SetBoolAnimiton("Moving", dis >= brackDistance);
            playerController.Move(dire, moveSpeed * t , rotationSpeed );
        }

  

        if (corners.Length == 0 || stopFast)
        {
            playerController.SetBoolAnimiton("Moving", false);
            playerController.SetFloatAnimiton("Velocity", 0);
        }
    }


    protected void Stop()
    {
        playerController.SetFloatAnimiton("Velocity", 0);
        playerController.SetBoolAnimiton("Moving", false);

        target = null;
    }

    private float distanceToTarget(Vector3[] corners)
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


    private void OnTriggerStay(Collider other)
    {

    }


    private void OnCollisionStay(Collision collision)
    {
        AiBase ai = collision.collider.GetComponent<AiBase>();


        if (ai != null && ! stopFast)
        {


            if (!ai.isMove &&  ai.currentCollisionAiBase == null)
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

 


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(target.GetValueOrDefault(), 0.1f);
    }

   
}

