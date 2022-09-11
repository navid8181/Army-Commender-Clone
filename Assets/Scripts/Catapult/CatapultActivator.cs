using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class CatapultActivator : MonoBehaviour
{


    public Camera cameraCatapult;


    private Rigidbody rb;
    private BoxCollider BoxCollider;

    private Catapult catapult;

    private StatusBar statusBar;

    private Player Player;

    private Vector3 firstPos;
    private Quaternion firstRot;


    public bool IsActivate { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        BoxCollider = GetComponent<BoxCollider>();  

        BoxCollider.isTrigger = true;

        catapult = transform.root.GetComponent<Catapult>();
        statusBar = GetComponent<StatusBar>();

       firstPos = transform.position;
        firstRot = transform.rotation;
     



      

    }

    private void Start()
    {
        statusBar.OnStatusBarCompleate.AddListener(() =>
        {

          
            if (!catapult.CanUse || IsActivate) return;

            ChangeCamera.Change(Player.getCamera(), cameraCatapult);

            Player.CanMove = false;

            catapult.CanMove = true;
            catapult.curveHandler.GetComponent<LineRenderer>().enabled = true;

            MasterManager.Instance.uiManager.EnableCataputAttackButton();

            IsActivate = true;

        });
    }
    private void Update()
    {
        transform.position = firstPos;
        transform.rotation = firstRot;
    }
    private void OnTriggerStay(Collider other)
    {


        if (!other.CompareTag("Player")) return;

        Player = other.GetComponent<Player>();

        if (Player.isMoveing()) return;

        if (!catapult.CanUse) return;

        statusBar.SetFill(1);





    }

}
