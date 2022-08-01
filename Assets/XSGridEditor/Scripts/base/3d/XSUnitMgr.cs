using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-01 20:40:16
/// @Description: xxsobjectdata对象管理
/// </summary>
namespace XSSLG
{
    using XSUnitDict = Dictionary<Vector3Int, XSUnitData>;

    /// <summary>  </summary>
    public class XSUnitMgr
    {
        /************************* 变量 begin ***********************/
        // private Transform UnitRoot { get; }
        public XSUnitDict UnitDict { get; private set; } = new XSUnitDict();

        /************************* 变量  end  ***********************/
        public XSUnitMgr(XSGridHelper helper)
        {
            // this.UnitRoot = helper?.UnitRoot;
            this.UnitDict = this.CreateXSUnitDict(helper);
        }

        protected XSUnitDict CreateXSUnitDict(XSGridHelper helper)
        {
            var ret = new XSUnitDict();
            if (helper == null)
                return ret;

            var unitDataList = helper.GetUnitDataList();
            if (unitDataList == null || unitDataList.Count == 0)
                return ret;


            // 遍历Tile
            unitDataList.ForEach(tileData => this.AddXSUnit(tileData, ret));

            return ret;
        }

        /// <summary>
        /// 添加XSTile到字典中
        /// </summary>
        /// <param name="unitData"></param>
        /// <param name="unitDict"></param>
        /// <returns></returns>
        public bool AddXSUnit(XSUnitData unitData, XSUnitDict unitDict)
        {
            var gridMgr = XSInstance.Instance.GridMgr;
            var tilePos = gridMgr.WorldToTile(unitData.transform.position);
            if (unitDict.ContainsKey(tilePos))
            {
                Debug.LogError("XSUnitMgr.AddXSUnit: 同一tilePos上已经存在unitData：" + tilePos);
                return false;
            }
            else
            {
                unitDict.Add(tilePos, unitData);
                return true;
            }
        }
    }
}