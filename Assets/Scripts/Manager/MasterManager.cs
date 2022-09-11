using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{



    public static MasterManager Instance { get; private set; }




    public PoolManager PoolManager { get; private set; }
    public EnemyManager enemyManager { get; private set; }

    public UIManager uiManager { get; private set; }


    private void Awake()
    {


        InitializedData();

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    private void InitializedData()
    {


        PoolManager = FindObjectOfType<PoolManager>();
        enemyManager = FindObjectOfType <EnemyManager>();
        uiManager = FindObjectOfType<UIManager>();
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
