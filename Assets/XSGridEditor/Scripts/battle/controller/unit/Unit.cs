using UnityEngine;

namespace XSSLG
{
    /// <summary> 战斗单位 </summary>
    public class Unit : UnitBase
    {
        /// <summary> 实际坐标 </summary>
        public override Vector3 GetPosition() => this.Node.WorldPos;

        /// <summary> unity的unit脚本 </summary>
        public XSIUnitNode Node { get; }
    
        public Unit(Role role, GroupType group, XSIUnitNode node) : base(role, group)
        {
            this.Node = node ?? throw new System.ArgumentNullException(nameof(node));
        }

        internal void Die()
        {
            this.SetDead();
            this.Node.IsNeedDeadAnimation = true;
        }

        /// <summary>
        /// 移动到指定位置
        /// </summary>
        /// <param name="path">移动路径</param>
        public bool WalkTo(XSTile tile)
        {
            var path = this.FindPath(tile);
            if (path.Count == 0)
                return false;

            // 如果是原地就直接返回true
            if (path.Count == 1 && path[0] == XSInstance.GridMgr.WorldToTile(this.GetPosition()))
                return true;

            this.Node.WalkTo(path);
            return true;
        }
    }
}