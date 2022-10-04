using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/10/4
/// @Description: 战斗状态选择移动的位置
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 选择移动的位置，基类，ai和玩家通用
    /// </summary>
    public class PhaseChooseMoveBase : PhaseBase
    {
        /// <summary> 技能的攻击范围 </summary>
        protected List<Vector3> MoveRegion { get; set; }

        public override void OnEnter<T>(T logic)
        {
            base.OnEnter(logic);
            // 显示移动范围
            Debug.Assert(logic.UnitMgr.ActionUnit != null);
            this.MoveRegion = XSUG.GetBattleNode().GridShowMgr.ShowMoveRegion(logic.UnitMgr.ActionUnit.Node);
        }

        public override void OnExit<T>(T logic)
        {
            base.OnExit(logic);
            XSUG.GetBattleNode().GridShowMgr.ClearMoveRegion();
        }
    }
}