/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-01 20:40:16
/// @Description: xxsobjectdata对象管理
/// </summary>
using System.Collections.Generic;
// using UnityEngine;
using Vector3Int = UnityEngine.Vector3Int;
using Vector3 = UnityEngine.Vector3;
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
        /// 添加XSIUnitNode到字典中
        /// </summary>
        /// <param name="unitNode"></param>
        /// <returns></returns>
        public virtual bool AddXSUnit(XSIUnitNode unitNode)
        {
            if (unitNode == null)
                return false;

            var gridMgr = XSInstance.Instance.GridMgr;
            var unitTilePos = gridMgr.WorldToTile(unitNode.WorldPos);
            if (this.UnitDict.ContainsKey(unitTilePos))
            {
                Debug.LogError("XSUnitMgr.AddXSUnit: 同一tilePos上已经存在unitData：" + unitTilePos);
                return false;
            }
            else
            {
                // 根据子节点的collider获取总的collider，用于射线检测
                if (!XSUnityUtils.IsEditor())
                    unitNode.AddBoxCollider();

                this.UnitDict.Add(unitTilePos, unitNode);
                return true;
            }
        }

        public virtual bool RemoveXSUnit(Vector3 worldPos)
        {
            var gridMgr = XSInstance.Instance.GridMgr;
            var unitTilePos = gridMgr.WorldToTile(worldPos);
            if (this.UnitDict.ContainsKey(unitTilePos))
            {
                var unitNode = this.UnitDict[unitTilePos];
                unitNode.RemoveNode();
                this.UnitDict.Remove(unitTilePos);
                return true;
            }
            else
            {
                Debug.LogError("XSUnitMgr.RemoveXSUnit: 这个位置上不存在unit，tilePos：" + unitTilePos);
                return false;
            }
        }

        public virtual XSIUnitNode GetXSUnit(Vector3 worldPos)
        {
            var gridMgr = XSInstance.Instance.GridMgr;
            var unitTilePos = gridMgr.WorldToTile(worldPos);
            if (this.UnitDict.ContainsKey(unitTilePos))
                return this.UnitDict[unitTilePos];
            else
                return null;
        }

        public virtual void UpdateUnitPos()
        {
            if (!XSUnityUtils.IsEditor())
                return;

            var gridMgr = XSEditorInstance.Instance.GridMgr;
            foreach (var pair in this.UnitDict)
            {
                var newWorldPos = gridMgr.TileToTileCenterWorld(pair.Key);
                pair.Value.WorldPos = newWorldPos;
            }
        }
    }
}