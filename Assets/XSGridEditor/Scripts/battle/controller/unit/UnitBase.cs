using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 战斗单位基类 </summary>
    public abstract class UnitBase
    {
        /// <summary> 各种状态标记 </summary>
        protected int _flag = 0;

        /// <summary> 实际坐标 </summary>
        public abstract Vector3 GetPosition();

        /// <summary> 绑定的角色数据 </summary>
        public Role Role { get; }

        /// <summary> 势力 </summary>
        public GroupType Group { get; protected set; }

        public NormalTable Table { get; private set; } = new NormalTable();

        /// <summary> 基础属性 </summary>
        public Stat BaseStat { get; }

        /// <summary> 计算后的属性 </summary>
        public Stat GetStat()
        {
            var ret = new Stat();
            ret.Add(this.BaseStat);
            var add = this.Table.GetStat();
            ret.Add(add);
            return ret;
        }
        // public Stat FinalStat { get; }

        /// <summary> 缓存的所有路径 </summary>
        private Dictionary<Vector3, List<Vector3>> CachedPaths { get; set; }

        public UnitBase(Role role, GroupType group)
        {
            this.Role = role ?? throw new System.ArgumentNullException(nameof(role));
            this.Group = group;

            var skillList = new List<SkillData>();
            skillList.AddRange(this.Role.CombatArtArray);
            skillList.AddRange(this.Role.AbilityArray);
            skillList.AddRange(this.Role.LearnMagicArray);
            skillList.AddRange(this.Role.CrestArray);
            this.Table.InitSkill(skillList, this);

            this.BaseStat = new Stat(role.Stat);
            // this.FinalStat = new Stat(this.BaseStat);
        }

        /// <summary> 结束生命周期 </summary>
        public void Destroy()
        {
            this.Table.StopWork();
        }

        /************************* 标记判断 begin ***********************/
        virtual public void SetActived() => XSU.SetFlag(ref this._flag, (int)UnitStatusType.Actived);
        public bool IsActived() => XSU.GetFlag(ref this._flag, (int)UnitStatusType.Actived);
        public void SetDead() => XSU.SetFlag(ref this._flag, (int)UnitStatusType.Dead);
        public bool IsDead() => XSU.GetFlag(ref this._flag, (int)UnitStatusType.Dead);
        public void SetAttacked() => XSU.SetFlag(ref this._flag, (int)UnitStatusType.Attacked);
        public bool IsAttacked() => XSU.GetFlag(ref this._flag, (int)UnitStatusType.Attacked);
        public void SetMoved() => XSU.SetFlag(ref this._flag, (int)UnitStatusType.Moved);
        public bool IsMoved() => XSU.GetFlag(ref this._flag, (int)UnitStatusType.Moved);

        /************************* 标记判断  end  ***********************/

        /// <summary>
        /// 能否穿过，敌人算墙，会影响行走路径
        /// </summary>
        /// <param name="unit">单位</param>
        public bool CanWalkThrough(UnitBase unit)
        {
            // 如果自己或者对方有一个是enemy，那必须势力一样
            if ((this.Group == GroupType.Enemy || unit.Group == GroupType.Enemy) && this.Group != unit.Group)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 找到路径去指定位置
        /// </summary>
        /// <param name="path">找到路径</param>
        protected List<Vector3> FindPath(XSTile tile)
        {
            if (tile == null)
            {
                return new List<Vector3>();
            }

            //缓存
            if (this.CachedPaths != null && this.CachedPaths.ContainsKey(tile.TilePos))
                return this.CachedPaths[tile.TilePos];
            else
            {
                var srcWorldPos = this.GetPosition();
                var srcTile = XSU.GridMgr.GetXSTileByWorldPos(srcWorldPos);
                var path = XSU.GridMgr.FindPath(srcTile, tile);
                return path;
            }
        }

        /// <summary>
        /// 获取移动范围
        /// </summary>
        /// <returns></returns>
        public List<Vector3> GetMoveRegion()
        {
            var logic = XSUG.GetBattleLogic();

            var srcWorldPos = this.GetPosition();
            var srcTile = XSU.GridMgr.GetXSTileByWorldPos(srcWorldPos);
            // 缓存起来哈
            this.CachedPaths = XSU.GridMgr.FindAllPath(srcTile, this.GetStat().GetMov().GetFinal());
            // 把this.CachedPaths累加起来
            var ret = this.CachedPaths.Aggregate(new List<Vector3>(), (ret, pair) =>
            {
                // 去重
                ret.AddRange(pair.Value.Distinct());
                return ret;
            }).Distinct().ToList(); // 去重
            return ret;
        }

        /************************* 条件触发 begin ***********************/
        public virtual void OnGameStart()
        {
            this.Table.StartWork();
        }

        internal void OnGameEnd()
        {
            this.Table.StopWork();
        }

        public virtual void OnTurnStart(SkillUpdateData data = null)
        {
            this._flag = 0;
            this.Table.OnTurnStart(data);
        }

        public virtual void OnTurnEnd()
        {

        }

        /************************* 条件触发  end  ***********************/
    }
}