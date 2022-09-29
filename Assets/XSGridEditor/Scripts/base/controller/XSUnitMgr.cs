/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-01 20:40:16
/// @Description: XSunitNode`s manager
/// </summary>

namespace XSSLG
{
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
        /// add XSIUnitNode to dict
        /// </summary>
        /// <param name="unitNode"></param>
        /// <returns></returns>
        public override bool Add(XSIUnitNode node)
        {
            var ret = base.Add(node);
            if (ret)
            {
                if (XSUnityUtils.IsEditor())
                {
                    node.UpdatePos();
                }
                else
                {
                    // add the total collider where js caculated by all collider，used to raycast
                    node.AddBoxCollider();
                }
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
                pair.Value.UpdatePos();
            }
        }
    }
}