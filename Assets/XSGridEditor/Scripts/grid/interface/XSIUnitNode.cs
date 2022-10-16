/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:34:19
/// @Description: interface to XSUnitNode
/// </summary>
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace XSSLG
{
    public interface XSIUnitNode : XSINode
    {

        bool IsMoving { get; }

        /// <summary> 是否正在播放攻击动画 </summary>
        bool IsAttacking { get; }

        /// <summary> 是否需要受击后播放死亡动画 </summary>
        bool IsNeedDeadAnimation { get; set; }

        Unit GetUnit();

        void RotateTo(XSTile tile);

        void WalkTo(List<Vector3> path);

        void AddBoxCollider();

        void UpdatePos();

        void AttackAnimation();

        void ApplyDamageAnimation(int damage);

        void DieAnimation();
    }
}