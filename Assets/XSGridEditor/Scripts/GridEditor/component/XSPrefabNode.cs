/// <summary>
/// @Author: xiaoshi
/// @Date: 2021/8/23
/// @Description: script added to prefab gameobject
/// </summary>

using UnityEngine;

namespace XSSLG
{
    public class XSPrefabNode : MonoBehaviour, XSINode
    {
        public Vector3 WorldPos { get => this.transform.position; set => this.transform.position = value; }
        
        public virtual void RemoveNode() => XSUnityUtils.RemoveObj(this.gameObject);

        public virtual bool IsNull() => this == null;
    }

}
