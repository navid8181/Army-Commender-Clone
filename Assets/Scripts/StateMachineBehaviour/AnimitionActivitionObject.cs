using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimitionActivitionObject : StateMachineBehaviour

{

    IArrow arrow;
    public float timeDisable = 0.8f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        arrow = animator.transform.root.GetComponent<IArrow>();

    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        if (arrow != null)
        {

            if (stateInfo.normalizedTime >= 0.3f)
                arrow.EnableArrow();


            if (stateInfo.normalizedTime >= timeDisable)
                arrow.DisableArrow();
        }
    }






}
