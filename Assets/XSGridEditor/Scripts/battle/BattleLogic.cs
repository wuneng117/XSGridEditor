using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 
    /// 战斗管理
    /// 继承BattleFSMBase，修改回合状态
    /// </summary>
    public class BattleLogic : BattleFSMBase
    {
        /************************* 变量 begin ***********************/

        /// <summary> grid 助手 </summary>
        public XSGridHelper GridHelper { get; }

        public XSUnitMgr UnitMgr { get; protected set; }


        /************************* 变量  end  ***********************/

        public BattleLogic()
        {
            this.UnitMgr = new XSUnitMgr();
        }

        
        public void TurnBegin() => this.Change(new PhaseTurnBegin());

        /// <summary> 统一处理Change传递的第二个参数就是自己 </summary>
        public void Change(IPhaseBase nextPhase) => this.Change(nextPhase, this);

        /// <summary> 统一处理Update传递的第二个参数就是自己 </summary>
        public void Update() => this.Update(this);
    }
}