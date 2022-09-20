using UnityEngine;

namespace XSSLG
{
    /// <summary> load recource </summary>
    public class XSResLoadMgr
    {
        protected XSResLoadMgr() {}
        
        /// <summary>
        /// Dynamically load prefab from resource path and create GameObject
        /// </summary>
        /// <param name="path">resource path</param>
        /// <returns> the GameObject created</returns>
        public static GameObject LoadGameObject(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            if (prefab == null)
            {
                return null;
            }

            var obj = Object.Instantiate(prefab);
            return obj;
        }
    }
}