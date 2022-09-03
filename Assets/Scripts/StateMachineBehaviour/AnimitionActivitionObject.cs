using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimitionActivitionObject : StateMachineBehaviour

{

    AiBase aiBase;
    public float timeDisable = 0.8f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         aiBase = animator.transform.root.GetComponent<AiBase>();
        if (aiBase != null)
        {
            ((AIPlayer)aiBase).EnableArrow();
        }
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (aiBase != null)
        {
            if (stateInfo.normalizedTime >= timeDisable)
            ((AIPlayer)aiBase).DisableArrow();
        }
    }


  
       

    
}
