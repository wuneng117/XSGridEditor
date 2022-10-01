/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态机管理
/// </summary>
using UnityEngine;

namespace XSSLG
{
    /// <summary>
    /// 状态机，管理游戏中各个阶段的转换，从而分解每个阶段的操作
    /// 目前基础的流程
    /// </summary>
    public abstract class BattleFSMBase
    {
        /// <summary> 当前的阶段 </summary>
        public IPhaseBase Phase { get; protected set; }

        /// <summary> 鼠标事件处理 </summary>
        private PhaseMouseEvent MouseEvent { get; } = new PhaseMouseEvent();
        /// <summary>
        /// 切换阶段
        /// </summary>
        /// <param name="nextPhase"> 下个阶段 </param>
        /// <param name="logic"> 游戏管理，作为参数传入，让阶段可以做一些操作 </param>
        public void Change<T>(IPhaseBase nextPhase, T logic) where T : BattleLogic
        {
            Debug.Log("BattleFSM Change: " + this.Phase + "===>" + nextPhase);
            this.Phase?.OnExit(logic);
            this.Phase = nextPhase;
            this.Phase.OnEnter(logic);
        }

        /// <summary> 预留接口，每帧更新 </summary>
        public void Update<T>(T logic) where T : BattleLogic
        {
            this.Phase.Update(logic);
            this.MouseEvent.Update(logic, this.Phase);
        }
    }
}