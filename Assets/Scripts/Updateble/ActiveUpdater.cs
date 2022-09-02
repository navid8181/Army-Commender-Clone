using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(StatusBar))]
public class ActiveUpdater : MonoBehaviour
{
    public Updater updater;


    public CoinType coinType = CoinType.ironCoin;

    private BoxCollider boxCollider;

    private Rigidbody _rigidBody;

    private StatusBar _statusBar;

    private OptimizeCoinChest coinChest;

    private Timer timeGiveMoney,timeBackPool;


    private Queue<GameObject> objectPool;

    private TextMeshPro txt_UpdatePayment;
    private void Awake()
    {

        objectPool = new Queue<GameObject>();

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;

        _rigidBody = GetComponent<Rigidbody>();

        _rigidBody.isKinematic = true;

        _statusBar = GetComponent<StatusBar>();

        coinChest = FindObjectOfType<OptimizeCoinChest>();
        timeGiveMoney = new Timer(0.6f);

        timeBackPool = new Timer(1);

        txt_UpdatePayment = GetComponentInChildren<TextMeshPro>();

        UpdateTextPayment();
    }
    private void Start()
    {
        _statusBar.OnStatusBarCompleate.AddListener(() =>
        {
            Debug.Log("Update stus bal");
            timeGiveMoney.Init(() =>
            {
                IDistributable distributable = coinType == CoinType.ironCoin ? coinChest.RemoveIronCoin() : coinChest.RemoveGoldCoin();

                if (distributable != null)
                {

                    Coin coin = (Coin)distributable;
                    coin.NoneDistributeTarget = updater.transform.position;



                    objectPool.Enqueue(coin.gameObject);

                    updater.AddMoney(1);

                    UpdateTextPayment();

                }
                else
                {
                    UpdateTextPayment();
                }
            });

        });
    }

    private void UpdateTextPayment() => txt_UpdatePayment.text = updater.GetNextMoneyUpdater();
    private void Update()
    {
        if (objectPool.Count > 0)
        {
            timeBackPool.Init(() =>
            {
                MasterManager.Instance.PoolManager.BackToPool(objectPool.Dequeue());
            });
        }
        else
            timeBackPool.ResetValue();


    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _statusBar.SetFill(1);


        }


    }


    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _statusBar.SetFill(0);
        }
    }
}
