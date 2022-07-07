using System.IO;
using UnityEngine;

namespace XSSLG
{
    /// <summary> 加载资源 </summary>
    public class ResLoadMgr
    {
        /************************* 变量 begin ***********************/
        // private static ResLoadMgr msInstance;
        // public static ResLoadMgr Instance { get => msInstance = msInstance ?? new ResLoadMgr(); }

        /************************* 变量  end  ***********************/

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
            // obj.transform.parent = parentNode.transform;
        }


        /************************* 编辑器用 根目录是项目文件夹 begin ***********************/
        // /// <summary>
        // /// 通过资源路径动态加载文件并创建proto对象
        // /// </summary>
        // /// <param name="path">资源路径</param>
        // public static T EditorLoadProto<T>(string path)
        // {
        //     if (!File.Exists(path))
        //         return default(T);

        //     var bytes = File.ReadAllBytes(path);
        //     if (bytes == null)
        //         return default(T);

        //     System.IO.Stream steam = new System.IO.MemoryStream(bytes);
        //     var ret = ProtoBuf.Serializer.Deserialize<T>(steam);
        //     return ret;
        // }

        // /// <summary>
        // /// 通过资源路径保存proto对象
        // /// </summary>
        // /// <param name="protoFile">保存的proto</param>
        // /// <param name="folderPath">资源路径的文件夹</param>
        // /// <param name="savePath">资源路径</param>
        // public static void EditorSaveProto<T>(T protoFile, string savePath)
        // {
        //     var dir = Path.GetDirectoryName(savePath);
        //     if (dir != "")
        //         Directory.CreateDirectory(dir);
        //     FileStream fs = File.Create(savePath); //创建的数据流 生成 user.bin 文件
        //     ProtoBuf.Serializer.Serialize<T>(fs, protoFile);
        //     fs.Close();
        // }
    }
    /************************* 编辑器用 根目录是项目文件夹  end  ***********************/
}