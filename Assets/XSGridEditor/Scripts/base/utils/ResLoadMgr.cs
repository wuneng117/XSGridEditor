using UnityEngine;

namespace XSSLG
{
    /// <summary> 加载资源 </summary>
    public class ResLoadMgr
    {
        /// <summary>
        /// 通过资源路径动态加载prefab并创建GameObject
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns>生成的GameObject</returns>
        public static GameObject LoadGameObject(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            if (prefab == null)
                return null;

            var obj = Object.Instantiate(prefab);
            return obj;
        }
    }
}