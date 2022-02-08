/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: 封装 UnityUtils 为这个项目特定使用 
/// </summary>
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XSSLG
{
    /// <summary> 封装 UnityUtils 为这个项目特定使用 </summary>
    public class UnityGameUtils
    {
        //-------------------------------继承自UnityUtils begin---------------------------------
        /// <summary>
        /// 世界坐标转为屏幕坐标
        /// </summary>
        /// <param name="worldPos">世界坐标</param>
        /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
        /// <returns></returns>       
        public static Vector3 WorldPosToScreenPos(Vector3 worldPos) => UnityUtils.WorldPosToScreenPos(worldPos);

        /// <summary>
        /// 删除所有子节点
        /// </summary>
        /// <param name="obj"></param>
        public static void RemoveChildren(GameObject obj) => UnityUtils.RemoveChildren(obj);

        /// <summary>
        /// 从 src 到 dest 坐标的旋转
        /// </summary>
        /// <param name="src">要旋转物体的坐标</param>
        /// <param name="dest">要朝向目标的坐标</param>
        /// <returns>src 的 locationRotation 值</returns>
        public static Quaternion RotationTo(Vector3 src, Vector3 dest) => UnityUtils.RotationTo(src, dest);

        //-------------------------------继承自UnityUtils  end ---------------------------------
        
        //-------------------------------下面都是项目特有的 ---------------------------------

        /// <summary>
        /// 生成一个适配size大小的textmeshpro字体节点，暂时用的默认字体
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static TextMeshPro CreateTextMesh(Vector2 size, Transform parent)
        {
            var textNode = new GameObject();
            textNode.name = "TilePosNode";
            var trans = textNode.AddComponent<RectTransform>();
            trans.SetParent(parent.transform);
            trans.sizeDelta = size;

            var text = textNode.AddComponent<TextMeshPro>();
            text.enableAutoSizing = true;
            text.fontSizeMin = 1;
            text.fontSizeMax = 100;
            text.alignment = TextAlignmentOptions.Center;
            return text;
        }

        /// <summary>
        /// 获取鼠标所指向的 tile
        /// </summary>
        /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
        /// <returns></returns>
        public static PathFinderTile GetMouseTargetTile(Camera camera = null) 
        {
            var screenPos = Pointer.current.position.ReadValue();
            var hit = UnityUtils.GetMouseHit(screenPos, camera, "Tile");
            var tile = hit.collider?.gameObject.GetComponent<XSTileData>()?.Tile;
            if (tile == null)
                return PathFinderTile.Default();
            return tile;
        }
    }
}