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
    public class XSU : UnityUtils
    {
        //-------------------------------继承自UnityUtils begin---------------------------------
        /// <summary>
        /// 世界坐标转为屏幕坐标
        /// </summary>
        /// <param name="worldPos">世界坐标</param>
        /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
        /// <returns></returns>       
        public static Vector3 WorldPosToScreenPos(Vector3 worldPos) => UnityUtils.WorldPosToScreenPos(worldPos);

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

        /// <summary> 
        /// 获取鼠标所在的世界坐标 
        /// </summary>
        /// <param name="screenPos">屏幕坐标</param>
        /// <param name="camera">主视角相机，如果不传入这个参数，则会设置为场景中第一个找到的Camera组件</param>
        /// <returns></returns>
        public static Vector3 ScreenPosToWorldPos(Vector2 screenPos, Camera camera = null) 
        {
            var hit = UnityUtils.GetMouseHit(screenPos, camera, "Ground");
            return hit.point;
        }
    }
}