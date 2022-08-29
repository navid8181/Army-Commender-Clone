using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(PlayerController))]

[RequireComponent(typeof(TriggerDetection))]
public class Player : MonoBehaviour
{
    [SerializeField] private JoyStick joyStick;
    [SerializeField] private float moveSpeed = 2.5f;

    [SerializeField] private float rotationSpeed = 2.5f;

    [SerializeField] private new Camera camera;

    [SerializeField] private CoinChest coinChest;

    [SerializeField] private OptimizeCoinChest optimizeCoinChest;

    private PlayerController PlayerController;

    private TriggerDetection triggerDetection;

    public bool isMoveing() => joyStick.getRawInput().sqrMagnitude >= 0.01f * 0.01f;
    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();

        triggerDetection = GetComponent<TriggerDetection>();

        triggerDetection.OnTriggerEnterDetection.AddListener(OntriggerEnter);
    }

    private void OntriggerEnter(Collider col)
    {

        bool isGoldCoin = col.name.Equals(PoolManager.goldCoin);
        bool isIronCoin = col.name.Equals(PoolManager.ironCoin);

        if (isGoldCoin || isIronCoin)
        {

            Coin coin = col.GetComponent<Coin>();

            optimizeCoinChest.SetDistribut(coin);

            coin.SetAvtiveCollider(false);

          

           // coinChest.AddCoin(col.gameObject);

          //  col.enabled = false;


        }
    }

    private void FixedUpdate()
    {
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


        PlayerController.SetFloatAnimiton("Velocity", velocity);


        PlayerController.Move(dire, moveSpeed * (velocity), rotationSpeed);
    }


}
