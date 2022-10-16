/// <summary>
/// @Author: zhoutao
/// @Date: 2021/10/5
/// @Description: 挂到animator的layer上，把所有事件抛给AnimatorStateEvent，为了通用化，不在这个脚本处理回调事件
/// </summary>
using UnityEngine;

/// <summary> 挂到animator的layer上，把所有事件抛给AnimatorStateEvent，为了通用化，不在这个脚本处理回调事件 </summary>
public class AnimatorStateBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var stateEvent = animator.GetComponent<AnimatorStateEvent>();
        if (stateEvent != null)
            stateEvent.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var stateEvent = animator.GetComponent<AnimatorStateEvent>();
        if (stateEvent != null)
            stateEvent.OnStateExit(animator, stateInfo, layerIndex);
    }

    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     var stateEvent = animator.GetComponent<AnimatorStateEvent>();
    //     if (stateEvent != null)
    //         stateEvent.OnStateUpdate(animator, stateInfo, layerIndex);
    // }

    // override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     var stateEvent = animator.GetComponent<AnimatorStateEvent>();
    //     if (stateEvent != null)
    //         stateEvent.OnStateMove(animator, stateInfo, layerIndex);
    // }

    // override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     var stateEvent = animator.GetComponent<AnimatorStateEvent>();
    //     if (stateEvent != null)
    //         stateEvent.OnStateIK(animator, stateInfo, layerIndex);
    // }
}