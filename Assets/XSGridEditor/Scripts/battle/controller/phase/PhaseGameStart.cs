using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态游戏开始
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 开始一局新的游戏，通知玩家游戏开始，玩家做一些初始化操作
    /// </summary>
    public class PhaseGameStart : PhaseBase
    {
        /// <summary> 状态进入 </summary>
        public override void OnEnter<T>(T logic)
        {
            // 通知所有单位onTurnStart
            logic.UnitMgr.ForEach(unit => unit.OnGameStart());
            // 等操作完成后切换到turnbegin
            logic.Change(new PhaseTurnBegin());
        }
    }
}