/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-19 13:38:22
/// @Description: show tile manager
/// </summary>
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using Vector3Int = UnityEngine.Vector3Int;

namespace XSSLG
{
    public class XSGridShowMgr
    {
        /************************* variable begin ***********************/
        protected XSIGridShowRegion MoveShowRegion { get; }

        /// <summary> show all the tile which in attack range </summary>
        private XSIGridShowRegion AttackShowRegion { get; set; }

        private XSIGridShowRegion AttackEffectShowRegion { get; set; }


        /************************* variable  end  ***********************/

        public XSGridShowMgr(XSIGridShowRegion moveShowRegion, XSIGridShowRegion attackShowRegion, XSIGridShowRegion attackEffectShowRegion)
        {
            this.MoveShowRegion = moveShowRegion;
            this.AttackShowRegion = attackShowRegion;
            this.AttackEffectShowRegion = attackEffectShowRegion;
        }

        /// <summary>
        /// show unit move range
        /// </summary>
        /// <param name="unit"> which unit </param>
        public virtual List<Vector3> ShowMoveRegion(Unit unit)
        {
            if (this.MoveShowRegion == null || this.MoveShowRegion.IsNull() || unit == null)
            {
                return new List<Vector3>();
            }

            var moveRegion = unit.GetMoveRegion();
            this.MoveShowRegion.ShowRegion(moveRegion);
            return moveRegion;
        }

        /// <summary> clear unit move range show </summary>
        public virtual void ClearMoveRegion() => this.MoveShowRegion?.ClearRegion();

        /// <summary>
        /// 显示单位攻击范围
        /// </summary>
        /// <param name="unit">显示范围的单位</param>
        /// <param name="skill">显示范围的技能</param>
        public List<Vector3Int> ShowAttackRegion(Unit unit, SkillBase skill)
        {
            if (this.AttackShowRegion == null || this.AttackShowRegion.IsNull() || unit == null)
            {
                return new List<Vector3Int>();
            }

            var gridMgr = XSU.GridMgr;
            var srcTile = gridMgr.GetXSTileByWorldPos(unit.Node.WorldPos);
            if (srcTile == null)
            {
                return new List<Vector3Int>();
            }

            var attackRegion = skill.Trigger.GetAttackRegion(gridMgr, srcTile);
            this.AttackShowRegion.ShowRegion(attackRegion);
            return attackRegion.ConvertAll(worldPos => gridMgr.WorldToTile(worldPos));
        }

        /// <summary> 清除单位攻击范围 </summary>
        public void ClearAttackRegion() => this.AttackShowRegion.ClearRegion();

        /// <summary>
        /// 显示单位攻击效果范围
        /// </summary>
        /// <param name="unit">显示范围的单位</param>
        /// <param name="skill">显示范围的技能</param>
        /// <param name="tile">当前鼠标所在tile</param>
        public void ShowAttackEffectRegion(Unit unit, SkillBase skill, XSTile tile)
        {
            if (unit == null || tile == null)
                return;

            var gridMgr = XSU.GridMgr;
            var srcTile = gridMgr.GetXSTileByWorldPos(unit.Node.WorldPos);
            if (srcTile == null)
            {
                return;
            }

            var moveRegion = skill.Trigger.GetAttackEffectRegion(tile, srcTile);
            this.AttackEffectShowRegion.ShowRegion(moveRegion);
        }

        /// <summary> 清除单位攻击效果范围 </summary>
        public void ClearAttackEffectRegion() => this.AttackEffectShowRegion.ClearRegion();
    }
}