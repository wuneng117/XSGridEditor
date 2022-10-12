/// <summary>
/// @Author: zhoutao
/// @Date: 2021/6/2
/// @Description: 通过条件搜索对象
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 索敌 </summary>
    public abstract class SearchTargetBase
    {
        /************************* 变量 begin ***********************/
        protected SearchStruct SearchStruct { get; }
        /************************* 变量  end  ***********************/

        public SearchTargetBase(SearchStruct searchStruct)
        {
            this.SearchStruct = searchStruct;
        }

        /// <summary>
        /// 搜索返回可以攻击的对象
        /// </summary>
        /// <param name="data">释放数据</param>
        /// <returns></returns>
        public virtual List<UnitBase> Search(OnTriggerDataBase data)
        {
            var logic = XSUG.GetBattleLogic();
            var gridMgr = XSU.GridMgr;
            var srcTile = gridMgr.GetXSTileByWorldPos(((Unit)data.Src).Node.WorldPos);
            if (srcTile == null)
            {
                return new List<UnitBase>();
            }

            // 有技能释放的原点，直接搜索即可
            if (data.GetType() == typeof(OnTriggerDataCommon))
            {
                var dataCommon = data as OnTriggerDataCommon;
                return this.SearchByPos(dataCommon.Src, dataCommon.Tile, logic, gridMgr, srcTile);
            }
            // 没有技能释放的原点，遍历所有可能的技能释放原点
            else
                return this.SearchByAll(data.Src, logic, gridMgr, srcTile);
        }

        /// <summary>
        /// 通过技能释放的原点，获取所有攻击对象
        /// </summary>
        /// <param name="src">技能释放者</param>
        /// <param name="tile">技能释放的原点，也可以说是鼠标点击的地点</param>
        /// <param name="logic">参数传递下就不用重新获取了</param>
        /// <param name="gridMgr">参数传递下就不用重新获取了</param>
        /// <param name="srcCellPos">技能释放者所在位置</param>
        /// <returns></returns>
        protected List<UnitBase> SearchByPos(UnitBase src, XSTile tile, BattleLogic logic, XSIGridMgr gridMgr, XSTile srcTile)
        {
            var ret = new List<UnitBase>();

            // 获取所有符合条件的网格
            var tileList = this.GetAttackEffectRegion(tile.TilePos, srcTile.TilePos);

            // 判断cellpos的合法性
            var tileUnitDict = logic.UnitMgr.GetTileUnitDict();
            tileList.ForEach(destTile =>
            {
                // 上面没有unit
                if (!tileUnitDict.ContainsKey(destTile))
                    return;

                var target = tileUnitDict[tile];

                // unit是目标
                if (this.CheckIsTarget(src, target))
                    ret.Add(target);
            });

            return ret;
        }

        /// <summary>
        /// 遍历所有可能的技能释放原点，获取所有攻击对象，只要有攻击对象就直接返回，不全部搜索
        /// </summary>
        /// <param name="src">技能释放者</param>
        /// <param name="logic">参数传递下就不用重新获取了</param>
        /// <param name="gridMgr">参数传递下就不用重新获取了</param>
        /// <param name="srcCellPos">技能释放者所在位置</param>
        /// <returns></returns>
        protected virtual List<UnitBase> SearchByAll(UnitBase src, BattleLogic logic, XSIGridMgr gridMgr, XSTile srcTile)
        {
            var attackRegion = this.GetAttackRegion(gridMgr, srcTile);
            foreach (var near in attackRegion)
            {
                var ret = this.SearchByPos(src, near, logic, gridMgr, srcTile);
                if (ret.Count > 0)
                    return ret;
            }

            return new List<UnitBase>();
        }

        /// <summary>
        /// 获取攻击范围
        /// </summary>
        /// <param name="gridMgr">参数传递下就不用重新获取了</param>
        /// <param name="srcCellPos">技能释放者所在位置</param>
        /// <returns></returns>
        public abstract List<XSTile> GetAttackRegion(XSIGridMgr gridMgr, XSTile srcTile);

        /// <summary>
        /// 获取攻击效果范围的格子
        /// </summary>
        /// <param name="cellPos">技能释放的原点，也可以说是鼠标点击的地点</param>
        /// <param name="srcPos">技能释放者所在位置</param>
        /// <returns></returns>
        public abstract List<XSTile> GetAttackEffectRegion(Vector3Int cellPos, Vector3Int srcPos);

        /// <summary> 可否选为目标 </summary>
        private bool CheckIsTarget(UnitBase src, UnitBase target)
        {
            // 没有条件，那就都可以
            if (this.SearchStruct.Target.Count == 0)
                return true;

            // 只要有一个type条件成立就可以将target选为目标
            return SearchStruct.Target.Any(type => SearchTargetBase.CheckSearchTagretType(src, target, type));
        }

        /// <summary>
        /// 根据type判断src和target的Group是否符合条件
        /// </summary>
        /// <param name="src">技能释放者</param>
        /// <param name="target">技能目标</param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool CheckSearchTagretType(UnitBase src, UnitBase target, SearchTargetType type)
        {
            switch (type)
            {
                //说明没填正确，不可选为目标
                default: return false;
                case SearchTargetType.None: return false;
                // 是不是自己
                case SearchTargetType.Self: return src == target;
                //友方不包括自己，只要我不能攻击它,那就是友方
                case SearchTargetType.Friend: return src != target && (!Config.UNIT_GROUP_MARTEX[(int)src.Group][(int)target.Group]);
                //只要我可以攻击它,那就是敌方
                case SearchTargetType.Enemy: return Config.UNIT_GROUP_MARTEX[(int)src.Group][(int)target.Group];
            }
        }
    }

    /// <summary> 空的，不进行搜索，返回空的对象列表 </summary>
    public class SearchTargetNull : SearchTargetBase
    {
        public SearchTargetNull() : base(null) { }
        public override List<UnitBase> Search(OnTriggerDataBase data) => new List<UnitBase>();
        protected override List<UnitBase> SearchByAll(UnitBase src, BattleLogic logic, XSIGridMgr gridMgr, XSTile srcTile) => new List<UnitBase>();
        public override List<XSTile> GetAttackRegion(XSIGridMgr gridMgr, XSTile srcTile) => new List<XSTile>();
        public override List<XSTile> GetAttackEffectRegion(Vector3Int tile, Vector3Int srcTile) => new List<XSTile>();
    }
}