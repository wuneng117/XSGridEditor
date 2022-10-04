
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-08-01 20:40:16
/// @Description: XSunitNode`s manager
/// </summary>
namespace XSSLG
{
    public class XSUnitMgrEditMode : XSNodeMgr<XSIUnitNode>
    {
        public XSUnitMgrEditMode(XSGridHelper helper)
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
                if (XSU.IsEditor())
                {
                    node.UpdatePos();
                }
            }
            return ret;
        }

        public virtual void UpdateUnitPos()
        {
            if (!XSU.IsEditor())
            {
                return;
            }

            var gridMgr = XSU.GridMgr;
            foreach (var pair in this.Dict)
            {
                var newWorldPos = gridMgr.TileToTileCenterWorld(pair.Key);
                pair.Value.WorldPos = newWorldPos;
                pair.Value.UpdatePos();
            }
        }
    }
}