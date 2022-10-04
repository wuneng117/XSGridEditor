using UnityEngine;
/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态回合开始
/// </summary>
namespace XSSLG
{
    /// <summary>
    /// 玩家的回合开始，通知当前玩家回合开始，当前玩家做一些回合开始的操作
    /// </summary>
    public class PhaseTurnBegin : PhaseBase
    {
        private CustomScheduler Scheduler { get; set; } = new CustomScheduler();

        /// <summary> 状态进入 </summary>
        public override void OnEnter<T>(T logic)
        {
            // 通知自己单位onTurnStart
            var unitList = logic.UnitMgr.GetSelfUnitList();
            unitList.ForEach(unit => unit.OnTurnStart());
            if (unitList.Count > 0)
                XSUG.CameraGoto(unitList[0].WorldPos);

            XSUG.CameraCanFreeMove(false);
            // 可以行动的单位高亮 PHASETODO

            var battleNode = XSUG.GetBattleNode();
            battleNode.OpenTurnChange(GroupType.Self);

            this.Scheduler.ScheduleOnce(() =>
            {
                battleNode.CloseTurnChange(GroupType.Self);
                // 切换到选择单位行动
                logic.Change(new PhaseChooseUnit(), logic);
            }, 2);
        }

        public override void OnExit<T>(T logic)
        {
            base.OnExit(logic);
            XSUG.CameraCanFreeMove(true);
        }

        public override void Update<T>(T logic)
        {
            base.Update(logic);
            this.Scheduler.Update(Time.deltaTime);
        }
    }
}
