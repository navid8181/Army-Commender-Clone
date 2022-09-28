using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Updater : MonoBehaviour
{


    public int maxUpdateIndex = 3;

    public AnimationCurve graphMoneyPyment;

    public StatusBar updateStatusBar;
    public StatusBar nameStatusBar;

    public Behaviour[] BehavioursScript;

    public MeshRenderer[] Behavioursmodels;
    public GameObject[] BehavioursmodelsObject;

    private int currentMoney = 0;



    private int currentUpdateIndex = -1;

    private Changeable<bool> IsAvtive;

    protected bool isAvtice = true;


    private void Start()
    {
        IsAvtive = new Changeable<bool>(true);
        IsAvtive.onChangeValue += (lastValue, CurrentValue) =>
        {
            if (CurrentValue)
            {
                foreach (var item in BehavioursScript)
                {
                    item.enabled = true;
                }
                foreach (var item in Behavioursmodels)
                {
                    item.enabled = true;
                }

                foreach (var item in BehavioursmodelsObject)
                {
                    item.SetActive(true);
                }
            }
            else
            {
                foreach (var item in BehavioursScript)
                {
                    item.enabled = false;
                }
                foreach (var item in Behavioursmodels)
                {
                    item.enabled = false;
                }

                foreach (var item in BehavioursmodelsObject)
                {
                    item.SetActive(false);
                }
            }
        };

        currentUpdateIndex = -1;
        UpgradeStatusBar();
    }
    public virtual void ExecuteUpdater()
    {
        UpgradeStatusBar();

        
    }

    private void UpgradeStatusBar() => updateStatusBar.SetFill(currentUpdateIndex / (float)maxUpdateIndex);

    public virtual void Update()
    {
        CheckForUpdate();

        if (nameStatusBar != null)
        {
            nameStatusBar.SetFill(currentMoney / (float)graphMoneyPyment.Evaluate(currentUpdateIndex + 1));
        }


       isAvtice = currentUpdateIndex < 0 ? false : true;

        IsAvtive.Value = isAvtice;

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
