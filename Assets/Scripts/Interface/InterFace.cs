
using UnityEngine;
public interface I_Ai_Base
{
    public void FindTarget();

    public void SetTraget(Vector3? target);

}


public interface IDamageable
{
    public float Health { get; set; }

    public void ApplyDamage(float damage);
}
public interface IDistributable
{
    public int DistributIndex { get; set; }
    public DistributionBase currentDistribution { get; set; }
    public void SetTraget(Vector3? target);


}

public interface IArrow{

    public void EnableArrow();
    public void DisableArrow();
   
}

