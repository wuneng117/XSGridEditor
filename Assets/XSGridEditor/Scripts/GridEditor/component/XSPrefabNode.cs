/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/8/23
/// @Description: tile 节点上的组件
/// </summary>

using UnityEngine;

namespace XSSLG
{
    /// <summary> tile 额外数据的数据结构 </summary>
    public class XSPrefabNode : MonoBehaviour, XSIBrushItem
    {
        public Vector3 WorldPos { get => this.transform.position; set => this.transform.position = value; }
        
        public virtual void RemoveNode() => XSUnityUtils.RemoveObj(this.gameObject);

        public virtual bool IsNull() => this == null;
    }

}
