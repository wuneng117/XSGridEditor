/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 战斗状态抽象类
/// </summary>
using System;
using UnityEngine;

namespace XSSLG
{
    public interface IPhaseBase
    {
        void OnEnter<T>(T logic) where T : BattleLogic;
        void OnExit<T>(T logic) where T : BattleLogic;
        void Update<T>(T logic) where T : BattleLogic;
        void OnMouseUpLeft<T>(T logic, XSTile mouseTile) where T : BattleLogic;
        void OnMouseUpRight<T>(T logic, XSTile mouseTile) where T : BattleLogic;
        void OnMouseMove<T>(T logic, XSTile mouseTile) where T : BattleLogic;
    }

    /// <summary>
    /// 每个阶段是一个类专门处理，通过调用状态机BattleFSM的Change函数来做阶段切换
    /// 目前有OnEnter和OnExit2个接口，分2个是因为卡牌游戏经常有“在XX回合开始时”，“在xx回合结束时”这一类效果的触发
    /// </summary>
    public abstract class PhaseBase : IPhaseBase
    {
        /// <summary> 状态进入 </summary>
        public virtual void OnEnter<T>(T logic) where T : BattleLogic { }
        /// <summary> 状态退出 </summary>
        public virtual void OnExit<T>(T logic) where T : BattleLogic { }
        /// <summary> 预留接口，每帧更新 </summary>
        public virtual void Update<T>(T logic) where T : BattleLogic { }

        /************************* 鼠标事件 begin ***********************/
        /// <summary> 鼠标点击事件 </summary>
        public virtual void OnMouseUpLeft<T>(T logic , XSTile mouseTile) where T : BattleLogic {}
        public virtual void OnMouseUpRight<T>(T logic, XSTile mouseTile) where T : BattleLogic {}
        
        /// <summary> 鼠标移动事件 </summary>
        public virtual void OnMouseMove<T>(T logic, XSTile mouseTile) where T : BattleLogic {}
        
        /************************* 鼠标事件  end  ***********************/
    }
}