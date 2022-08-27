using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DistributionBase : MonoBehaviour
{

    private List<IDistributable> distributables = new List<IDistributable>();


    public IDistributable[] GetDistributables() => distributables.ToArray();
    public int getLenght { get { return distributables.Count; } }

    private void Awake()
    {
       
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
        distributable.currentDistribution = this;
        distributables.Add(distributable);


    }


    public void RemoveDistribut(IDistributable distributable)
    {
        distributables.Remove(distributable);
        UpdateIndex();
    }
}
