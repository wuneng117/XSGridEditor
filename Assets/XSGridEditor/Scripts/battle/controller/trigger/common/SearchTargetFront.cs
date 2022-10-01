/// <summary>
/// @Author: zhoutao
/// @Date: 2021/6/8
/// @Description: 搜索前方敌人
/// </summary>
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 搜索前方敌人 </summary>
    public class SearchTargetFront : SearchTargetBase
    {
        /************************* 变量 begin ***********************/
        /// <summary> 前方多少长度 </summary>
        private int Length { get; }
        /************************* 变量  end  ***********************/

        public SearchTargetFront(TriggerDataSearchStruct searchStruct, int length) : base(searchStruct)
        {
            this.Length = length;
        }

        /// <summary>
        /// 获取攻击范围
        /// </summary>
        /// <param name="srcTile">技能释放者所在位置</param>
        /// <param name="gridMgr">参数传递下就不用重新获取了</param>
        /// <returns></returns>
        public override List<XSTile> GetAttackRegion(XSIGridMgr gridMgr, XSTile srcTile) => srcTile.NearTileList;

        /// <summary>
        /// 获取攻击效果范围的格子，方向性搜索
        /// </summary>
        /// <param name="cellPos">技能释放的原点，也可以说是鼠标点击的地点</param>
        /// <param name="srcPos">技能释放者所在位置</param>
        /// <param name="cellPosList">以网格坐标存储所有tile</param>
        /// <returns></returns>
        public override List<XSTile> GetAttackEffectRegion(Vector3Int cellPos, Vector3Int srcPos)
        {
            var ret = new List<XSTile>();
            for (var i = 0; i < this.Length; i++)
            {
                // 确实有这个格子再加
                var pos = cellPos + i * (cellPos - srcPos);
                if (XSInstance.GridMgr.TryGetXSTile(pos, out var tile))
                    ret.Add(tile);
            }
            return ret;
        }
    }
}