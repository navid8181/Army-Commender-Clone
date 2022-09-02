using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Updater : MonoBehaviour
{


    public float maxUpdateIndex = 3;

    public AnimationCurve graphMoneyPyment;

    private int currentMoney = 0;



    private int currentUpdateIndex = 0;


    public virtual void ExecuteUpdater()
    {

    }

    public virtual void Update()
    {
        CheckForUpdate();
    }

    private void CheckForUpdate()
    {
        if (currentUpdateIndex > maxUpdateIndex) return;

        if (currentMoney >= graphMoneyPyment.Evaluate(currentUpdateIndex + 1))
        {
            currentUpdateIndex++;
            currentMoney = 0;

            ExecuteUpdater();
        }
    }

    public void AddMoney(int value) => currentMoney += value;

    public bool isMax() => currentUpdateIndex >= maxUpdateIndex;

    public string GetNextMoneyUpdater()
    {
        if (currentUpdateIndex >= maxUpdateIndex) return "Max";

        else
            return currentMoney.ToString() + " / " + graphMoneyPyment.Evaluate(currentUpdateIndex + 1).ToString();
    }
}
