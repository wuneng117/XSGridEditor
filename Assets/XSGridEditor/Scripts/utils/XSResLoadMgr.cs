using System.IO;
using System.Text;
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

        /************************* 编辑器用 根目录是项目文件夹 begin ***********************/
        public static void SaveFileToPath(string str, string savePath)
        {
            var dir = Path.GetDirectoryName(savePath);
            if (dir != "")
            {
                Directory.CreateDirectory(dir);
            }
            FileStream fs = File.Create(savePath);
            byte[] info = new UTF8Encoding(true).GetBytes(str);
            fs.Write(info, 0, info.Length);
            fs.Close();
        }
    /************************* 编辑器用 根目录是项目文件夹  end  ***********************/
    }
}