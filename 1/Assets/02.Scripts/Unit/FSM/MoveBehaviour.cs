using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : BehaviourBase
{

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (manager.distanceTarget == 0)
        {
            manager.ChangeState(StateType.Idle);
        }
    }

}
