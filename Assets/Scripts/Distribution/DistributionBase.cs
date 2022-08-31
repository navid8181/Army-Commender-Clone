using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DistributionBase : MonoBehaviour
{

    private List<IDistributable> distributables = new List<IDistributable>();


    public IDistributable[] GetDistributables() => distributables.ToArray();
    public int CurrentDistribuionSize { get { return distributables.Count; } }

    public int MaxDistrubutionSize { get; set; }

    protected bool IsUpdatingIndex;
    private void Awake()
    {
       
    }

    protected void UpdateIndex(int index)
    {
        for (int i = index; i < distributables.Count; i++)
        {
            IsUpdatingIndex = true;
            distributables[i].DistributIndex = i;
        }

        IsUpdatingIndex = false;
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
        bool isUpdate = true;

        if (distributable.DistributIndex == GetDistributables().Length - 1)
            isUpdate = false;

        distributables.RemoveAt(distributable.DistributIndex);



        if (isUpdate)
        UpdateIndex(distributable.DistributIndex);

        distributable.DistributIndex = -1;
    }

    public void ChangeDistribution(DistributionBase distributionBase)
    {
        for (int i = CurrentDistribuionSize - 1; i >=0; i--)
        {

            if (distributionBase.MaxDistrubutionSize <= distributionBase.CurrentDistribuionSize)
            {
                break;
            }
            IDistributable distributable = distributables[i];

            RemoveDistribut(distributable);

            distributionBase.SetDistribut(distributable);

           

        }
    }
}
