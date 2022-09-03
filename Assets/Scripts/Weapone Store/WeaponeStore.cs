using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponeStore : Updater
{


    public CoineDistributeManager coineDistributeManager;

    public ItemDistrubtionManager itemDistrubtionManager;
    public float timeToCreateWeapone = 4;

    public StateChangere stateChnger;
    private Timer timer;

    private void Awake()
    {
        timer = new Timer(timeToCreateWeapone);

        stateChnger.OnAIbaseComing.AddListener((AiBase aiBase) =>
        {
            if (itemDistrubtionManager.CurrentDistribuionSize > 0)
            {
                if (aiBase.indexOfWeapone + 1 >= aiBase.weapones.Length)
                    return;

                    IDistributable distributable = itemDistrubtionManager.RemoveDistributAtLast();

                Item item = (Item)distributable;

                MasterManager.Instance.PoolManager.BackToPool(item.gameObject);

      
                aiBase.indexOfWeapone++;
            }
        });
    }

    public override void Update()
    {
        base.Update();

        if (timer.getCounter() != timeToCreateWeapone)
            timer.SetCounter(timeToCreateWeapone);

        if(coineDistributeManager.GetDistributables().Length > 0)
        {
            timer.Init(() =>
            {

                Item item = MasterManager.Instance.PoolManager.requestPool(PoolManager.weaponAmmo).GetComponent<Item>() ;

                itemDistrubtionManager.SetDistribut(item);

              IDistributable distributable =  coineDistributeManager.RemoveDistributAtLast();

                MasterManager.Instance.PoolManager.BackToPool(((Coin)distributable).gameObject);


            });
        }

    }


    public override void ExecuteUpdater()
    {
        base.ExecuteUpdater();

        timeToCreateWeapone-=2;
    }
}
