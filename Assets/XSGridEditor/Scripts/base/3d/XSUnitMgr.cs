using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-01 20:40:16
/// @Description: xxsobjectdata对象管理
/// </summary>
namespace XSSLG
{
    using UnitDict = Dictionary<Vector3Int, XSUnitData>;

    /// <summary>  </summary>
    public class XSUnitMgr
    {
        /************************* 变量 begin ***********************/
        // private Transform UnitRoot { get; }
        public UnitDict UnitDict { get; private set; } = new UnitDict();

        /************************* 变量  end  ***********************/
        public XSUnitMgr(XSGridHelper helper)
        {
            this.CreateXSUnitDict(helper);
        }

        protected void CreateXSUnitDict(XSGridHelper helper)
        {
            if (helper == null)
                return;

            var unitDataList = helper.GetUnitDataList();
            if (unitDataList == null || unitDataList.Count == 0)
                return;

            // 遍历Tile
            unitDataList.ForEach(unitData => this.AddXSUnit(unitData));
        }

        /// <summary>
        /// 添加XSTile到字典中
        /// </summary>
        /// <param name="unitData"></param>
        /// <returns></returns>
        public bool AddXSUnit(XSUnitData unitData)
        {
            var gridMgr = XSInstance.Instance.GridMgr;
            var tilePos = gridMgr.WorldToTile(unitData.transform.position);
            if (this.UnitDict.ContainsKey(tilePos))
            {
                Debug.LogError("XSUnitMgr.AddXSUnit: 同一tilePos上已经存在unitData：" + tilePos);
                return false;
            }
            else
            {
                // 根据子节点的collider获取总的collider，用于射线检测
                if (!UnityUtils.IsEditor())
                {
                    var collider = unitData.gameObject.AddComponent<BoxCollider>();
                    var bounds = unitData.GetMaxBounds();
                    collider.bounds.SetMinMax (bounds.min, bounds.max);
                    collider.center = collider.transform.InverseTransformPoint (bounds.center);
                    collider.size = bounds.size;
                }

                this.UnitDict.Add(tilePos, unitData);
                return true;
            }
        }
    }
}