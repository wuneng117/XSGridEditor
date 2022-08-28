/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-01 20:40:16
/// @Description: xxsobjectdata对象管理
/// </summary>

namespace XSSLG
{
    /// <summary>  </summary>
    public class XSUnitMgr : XSNodeMgr<XSIUnitNode>
    {
        public XSUnitMgr(XSGridHelper helper)
        {
            if (helper)
            {
                this.CreateDict(helper.GetUnitDataList());
            }
        }

        /// <summary>
        /// 添加XSIUnitNode到字典中
        /// </summary>
        /// <param name="unitNode"></param>
        /// <returns></returns>
        public override bool Add(XSIUnitNode node)
        {
            var ret = base.Add(node);
            if (ret && !XSUnityUtils.IsEditor())
            {
                // 根据子节点的collider获取总的collider，用于射线检测
                node.AddBoxCollider();
            }
            return ret;
        }

        public virtual void UpdateUnitPos()
        {
            if (!XSUnityUtils.IsEditor())
            {
                return;
            }

            var gridMgr = XSInstance.Instance.GridMgr;
            foreach (var pair in this.Dict)
            {
                var newWorldPos = gridMgr.TileToTileCenterWorld(pair.Key);
                pair.Value.WorldPos = newWorldPos;
            }
        }
    }
}