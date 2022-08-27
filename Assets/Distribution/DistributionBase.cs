using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DistributionBase : MonoBehaviour
{

    protected List<IDistributable> distributables;


    public int getLenght { get { return distributables.Count; } }

    private void Awake()
    {
        distributables = new List<IDistributable>();
    }

    protected void UpdateIndex()
    {
        for (int i = 0; i < distributables.Count; i++)
        {
            distributables[i].DistributIndex = i;
        }
    }

    public abstract void ExeCuteDistribute(int i);

    public void SetDistribut(IDistributable distributable)
    {
        distributable.DistributIndex = distributables.Count;
        distributables.Add(distributable);


    }


    public void RemoveDistribut(IDistributable distributable)
    {
        distributables.Remove(distributable);
        UpdateIndex();
    }
}
