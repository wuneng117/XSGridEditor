/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/9
/// @Description: tile 上放置的 object 数据结构
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG {
    /// <summary> tile 上放置的 object 数据结构 </summary>
    public class XSObjectData : MonoBehaviour
    {
        public int Id = -1;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        public virtual GameObject GetGameObj()
        {
    #if UNITY_EDITOR
    #endif
            return  null;
        }

        virtual protected void AddGameObj()
        { 
    #if UNITY_EDITOR
            var obj = this.GetGameObj();
            if (obj)
            {
                UnityUtils.RemoveChildren(this.gameObject);
                obj.transform.parent = this.transform;
            }
    #endif
        }
    }
}
