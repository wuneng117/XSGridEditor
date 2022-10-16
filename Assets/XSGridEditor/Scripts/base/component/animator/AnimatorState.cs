using UnityEngine;

namespace XSSLG
{
    /// <summary> animator参数规范定义 </summary>
    public class AnimatorState
    {
        public static readonly int FORWARD_SPEED    = Animator.StringToHash("ForwardSpeed");    // 移动速度，大于0表示移动
        public static readonly int ATTACK           = Animator.StringToHash("Attack");  // 普通攻击
        public static readonly int APPLY_DAMAGE     = Animator.StringToHash("ApplyDamage"); // 受击
        public static readonly int DIE              = Animator.StringToHash("Die"); // 死亡
        public static readonly int TIMEOUT_TO_IDLE  = Animator.StringToHash("TimeoutToIdle");
        public static readonly int INPUT_DETECTED   = Animator.StringToHash("InputDetected");
        public static readonly int GROUNDED         = Animator.StringToHash("Grounded");
        public static readonly int LOCOMOTION       = Animator.StringToHash("Locomotion");
    }
}