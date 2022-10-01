using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/10/5
/// @Description: 要处理animator事件的就继承下这个类
/// </summary>

/// <summary> 要处理animator事件的就继承下这个类 </summary>
public class AnimatorStateEvent : MonoBehaviour
{
    virtual public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    virtual public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // virtual public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    // }

    // virtual public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    // }

    // virtual public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    // }
}
