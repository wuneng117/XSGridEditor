/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态回合结束
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 当前玩家的回合结束，通知当前玩家回合结束，当前玩家做一些回合结束的操作
    /// 然后切换到AI，并切换到AI回合开始
    /// </summary>
    public class PhaseTurnEnd : PhaseBase
    {
        /// <summary> 状态进入 </summary>
        public override void OnEnter<T>(T logic)
        {
            base.OnEnter(logic);
            // 通知自己单位OnTurnEnd
            logic.GetSelfUnitList().ForEach(unit => unit.OnTurnEnd());

            logic.Change(new AITurnBegin());
        }
    }
}
