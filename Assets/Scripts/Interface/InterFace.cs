
using UnityEngine;
public interface I_Ai_Base
{
    public void FindTarget();

    public void SetTraget(Vector3? target);

}



public interface IDistributable
{
    public int DistributIndex { get; set; }
    public DistributionBase currentDistribution { get; set; }
    public void SetTraget(Vector3? target);


}


