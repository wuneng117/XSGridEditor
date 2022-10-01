/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: ai战斗状态回合结束
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// AI的回合结束，通知AI回合结束，AI做一些回合结束的操作
    /// 然后切换到玩家，并切换到回合开始
    /// </summary>
    public class AITurnEnd : PhaseBase
    {
        /// <summary> 状态进入 </summary>
        public override void OnEnter<T>(T logic)
        {
            base.OnEnter(logic);
            // 通知自己单位OnTurnEnd
            logic.GetEnemyUnitList().ForEach(unit => unit.OnTurnEnd());
            logic.Change(new PhaseTurnBegin());
        }
        
        public override void OnExit<T>(T logic)
        {
            base.OnExit(logic);
            XSUG.CameraCanFreeMove(true);
        }
    }
}
