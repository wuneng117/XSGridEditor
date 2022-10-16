/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/9
/// @Description: 单位动画管理
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 单位动画管理 </summary>
    public class UnitAnimation
    {
        /// <summary> unity的动画状态机 </summary>
        private Animator Animator { get; }
        public UnitAnimation(Animator animator)
        {
            // this.Animator = animator ?? throw new System.ArgumentNullException(nameof(animator));
        }

        /// <summary>
        /// 行动动画
        /// </summary>
        /// <param name="src">原来坐标</param>
        /// <param name="dest">目标坐标</param>
        public void SetForwardSpeed(int speed)
        {
            // this.Animator.SetFloat(AnimatorState.FORWARD_SPEED, speed);
            // Debug.Log(speed);
        }

        /// <summary> 播放攻击动画，通过trigger触发 </summary>
        public void PlayAttack() {}
        // public void PlayAttack() => this.Animator.SetTrigger(AnimatorState.ATTACK);


        /// <summary> 播放受击动画，通过trigger触发 </summary>
        public void PlayApplyDamage() {}
        // public void PlayApplyDamage() => this.Animator.SetTrigger(AnimatorState.APPLY_DAMAGE);

        /// <summary> 播放死亡动画，通过trigger触发 </summary>
        public void PlayDie() {}
        // public void PlayDie() => this.Animator.SetTrigger(AnimatorState.DIE);
    }
}