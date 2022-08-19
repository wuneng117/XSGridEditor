/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-01 20:40:16
/// @Description: xxsobjectdata对象管理
/// </summary>
using System.Collections.Generic;
// using UnityEngine;
using Vector3Int = UnityEngine.Vector3Int;
using Debug = UnityEngine.Debug;


namespace XSSLG
{
    using UnitDict = Dictionary<Vector3Int, XSIUnitNode>;

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

        protected virtual void CreateXSUnitDict(XSGridHelper helper)
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
        /// <param name="unitNode"></param>
        /// <returns></returns>
        public virtual bool AddXSUnit(XSIUnitNode unitNode)
        {
            var gridMgr = XSInstance.Instance.GridMgr;
            var tilePos = gridMgr.WorldToTile(unitNode.WorldPos);
            if (this.UnitDict.ContainsKey(tilePos))
            {
                Debug.LogError("XSUnitMgr.AddXSUnit: 同一tilePos上已经存在unitData：" + tilePos);
                return false;
            }
            else
            {
                // 根据子节点的collider获取总的collider，用于射线检测
                if (!XSUnityUtils.IsEditor())
                    unitNode.AddBoxCollider();

                this.UnitDict.Add(tilePos, unitNode);
                return true;
            }
        }
    }
}