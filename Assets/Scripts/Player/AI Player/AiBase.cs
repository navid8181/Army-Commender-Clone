using System.Collections;
using System.Collections.Generic;
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
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        navMeshPath = new NavMeshPath();

      aIRadius =  brackDistance = GetComponent<CapsuleCollider>().radius;

    }

    private void Update()
    {
       
    }
    protected void FindTarget()
    {

        Vector3 startPos = transform.position;

        if (target == null ) return;

   


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


            //if (dire.magnitude <= 0.1f)
            //{
            //    if (corners.Length >= 3)
            //    {
            //        pos = corners[2];


            //        dire = pos - transform.position;
            //        dire.y = 0;
            //    }

            //}

            dire.Normalize();

            pos.y = 0;
            Vector3 playerPos = transform.position;
            playerPos.y = 0;

            float dis = distanceToTarget(corners);


            float t = dis / (distanceToStop + aIRadius);

            t = Mathf.Clamp01(t);


            velocity = Mathf.Lerp(0, 1, t);


            isMove = dis >= brackDistance;


            playerController.SetFloatAnimiton("Velocity", velocity);

            playerController.SetBoolAnimiton("Moving", dis >= brackDistance);
            playerController.Move(dire, moveSpeed * t, rotationSpeed );
        }

  

        if (corners.Length == 0 || stopFast)
        {
            playerController.SetBoolAnimiton("Moving", false);
        }
    }


    private float distanceToTarget(Vector3[] corners)
    {
        float dis = 0;


        for (int i = 1; i < corners.Length; i++)
        {
            Vector3 pos = corners[i];

            pos.y = 0;

            Vector3 playerPos = transform.position;

            playerPos.y = 0;

            dis += Vector3.Distance(playerPos, pos);
        }

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


        if (ai != null)
        {


            if (!ai.isMove)
            {
                if (Vector3.Distance(ai.getTargetPos().Value, getTargetPos().Value) <= aIRadius)
                {
                    stopFast = true;
                    currentCollisionAiBase = ai;
                }else
                {
                    stopFast = false;
                    currentCollisionAiBase = null;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        AiBase ai = collision.collider.GetComponent<AiBase>();

        if (ai != currentCollisionAiBase) return;

        stopFast = false;

        currentCollisionAiBase = null;

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(target.GetValueOrDefault(), 0.1f);
    }
}

