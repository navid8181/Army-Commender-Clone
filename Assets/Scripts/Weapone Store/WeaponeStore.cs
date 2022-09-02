using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponeStore : Updater
{


    public CoineDistributeManager coineDistributeManager;
    private float timeToCreateWeapone = 4;

    private Timer timer;

    private void Awake()
    {
        timer = new Timer(timeToCreateWeapone);
    }

    public override void Update()
    {
        base.Update();

        if (timer.getCounter() != timeToCreateWeapone)
            timer.SetCounter(timeToCreateWeapone);



    }


    public override void ExecuteUpdater()
    {
        base.ExecuteUpdater();
    }
}
