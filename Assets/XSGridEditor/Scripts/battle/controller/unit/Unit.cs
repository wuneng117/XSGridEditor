using UnityEngine;

namespace XSSLG
{
    /// <summary> 战斗单位 </summary>
    public class Unit : UnitBase, XSINode
    {
        /// <summary> unity的unit脚本 </summary>
        public XSIUnitNode Node { get; }

        public override Vector3 WorldPos
        {
            get
            {
                if (this.Node == null)
                {
                    return Vector3.zero;
                }
                else
                {
                    return this.Node.WorldPos;
                }
            }
            set
            {
                if (this.Node != null)
                {
                    this.Node.WorldPos = value;
                }
            }
        }

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
            if (path.Count == 1 && path[0] == XSU.GridMgr.WorldToTile(this.WorldPos))
                return true;

            this.Node.WalkTo(path);
            return true;
        }

        public void RemoveNode() => this.Node?.RemoveNode();

        public bool IsNull()
        {
            if (this.Node == null)
            {
                return true;
            }
            else
            {
                return this.Node.IsNull();
            }
        }
    }
}