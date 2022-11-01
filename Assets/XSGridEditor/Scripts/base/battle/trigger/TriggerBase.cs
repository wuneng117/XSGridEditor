using System.Diagnostics;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    /// <summary>
    /// 主动技能触发器，触发技能释放
    /// 先通过CanRelease判断技能能否施放，然后调用Release施放技能
    /// 和被动技的区别就是是否注册BattleEmitter，但是还有其他要注意的：
    /// 1.拥有这个trigger的技能必须是主动技能
    /// 2.triggerType必须是ClickCombat或者ClickMagic
    /// 3.因为第2条，所以condition没有用
    /// 3.因为第1，2条，参数OnTriggerDataBase只能是OnTriggerDataBase或者OntriggerDataCommon，其他类原则上不能传递，
    /// 因为这个trigger和主动技能都不会处理其他OnTriggerDataBase类额外的参数，做好约束才能方便设计
    /// </summary>
    public class TriggerBase : WorkItem<TriggerData, SkillUpdateData>
    {
        /************************* 变量 begin ***********************/
        public TriggerType Type {get => this.Data.Type;}

        /// <summary> 对SkillBase的引用 </summary>
        protected IReleaseEntity ReleaseEntity { get; set; }

        /// <summary> 倒计时 </summary>
        protected CountDown Cd { get; set; }

        /// <summary> 索敌 </summary>
        protected SearchTargetBase SearchTarget { get; }

        /// <summary> 触发器触发条件 </summary>
        protected List<TriggerConditionBase> ConditionList { get; }
        /************************* 变量  end  ***********************/
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="data">触发器data</param>
        /// <param name="releaseEntity">触发器触发的对象</param>
        public TriggerBase(TriggerData data, IReleaseEntity releaseEntity) : base(data)
        {
            System.Diagnostics.Debug.Assert(releaseEntity != null);
            this.ReleaseEntity = releaseEntity;
            this.Cd = new CountDown();
            this.SearchTarget = SearchTargetFactory.CreateSearchTarget(data.SearchTarget);
            this.ConditionList = data.ConditionList.Select(condition => TriggerConditionFactory.Create(data.Type, condition)).ToList();
        }

        /// <summary> 获取攻击范围的格子 </summary>
        public List<Vector3> GetAttackRegion(XSIGridMgr gridMgr, XSTile srcTile) => this.SearchTarget.GetAttackRegion(gridMgr, srcTile).Select(tile => tile.WorldPos).ToList();
        /// <summary> 获取攻击效果范围的格子 </summary>
        public List<Vector3> GetAttackEffectRegion(XSTile tile, XSTile srcTile) => this.SearchTarget.GetAttackEffectRegion(tile.TilePos, srcTile.TilePos).Select(tile => tile.WorldPos).ToList();
        /// <summary>
        /// 触发器开始工作
        /// </summary>
        public override void StartWork()
        {
            this.StartTick();
        }

        /// <summary>
        ///结束触发器
        /// </summary>
        public override void StopWork()
        {
        }

        /// <summary> 开始倒计时 </summary>
        public void StartTick()
        {
            this.Cd.Start(this.Data.CD);
        }

        /// <summary> 停止倒计时 </summary>
        public void StopTick()
        {
            this.Cd.Stop();
        }

        /// <summary>
        /// 是否能释放
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual bool CanRelease(OnTriggerDataBase data)
        {
            var target = this.GetTarget(data);
            var triggerReleaseData = this.CreateReleaseData(data, target);
            return this.CanRelease(triggerReleaseData);
        }

        /// <summary>
        /// 是否能释放
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual bool CanRelease(ReleaseData data)
        {
            var ret = this.Cd.Finish;
            if (!ret)
                return false;

            if (!this.ConditionList.All(condition => condition.CanRelease(data.OnTriggerData)))
                return false;

            ret = this.ReleaseEntity.CanRelease(data);
            return ret;
        }

        /// <summary>
        /// 释放技能
        /// </summary>
        /// <param name="data">触发数据</param>
        public virtual bool Release(OnTriggerDataBase data)
        {
            if (!this.CanRelease(data))
                return false;

            var target = this.GetTarget(data);
            var triggerReleaseData = this.CreateReleaseData(data, target);
            this.ReleaseEntity.Release(triggerReleaseData);
            return true;
        }

        /// <summary>
        /// 获取处理对象
        /// </summary>
        /// <param name="data">触发数据</param>
        protected virtual List<UnitBase> GetTarget(OnTriggerDataBase data)
        {
            var ret = new List<UnitBase>();
            ret.AddRange(this.SearchTarget.Search(data));
            return ret;
        }

        private ReleaseData CreateReleaseData(OnTriggerDataBase data, List<UnitBase> target)
        {
            var triggerReleaseData = new ReleaseData(data, target);
            return triggerReleaseData;
        }

        /// <summary>
        /// 回合开始响应
        /// </summary>
        public override void OnTurnStart(SkillUpdateData data) => this.CdUpdate(data);

        /// <summary>
        /// cd更新
        /// </summary>
        protected void CdUpdate(SkillUpdateData data)
        {
            int turnCount = 1;
            this.Cd.Update(turnCount);
        }


    }
}