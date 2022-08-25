/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/15
/// @Description: 简单的画prefab的画笔，实现以下功能
/// 1.画完之后，把每个 tile 的 y 设置到障碍物的顶端
/// </summary>
/// 
#if ENABLE_TILEMAP
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace XSSLG
{
    [CustomGridBrush(true, false, false, "XSPrefab Brush")]
    public class XSPrefabBrush : XSBrushBase
    {
        public virtual void Awake()
        {
            this.UnitPath = "Assets/XSGridEditor/Resources/Prefabs/PrefabBrushs";
        }

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            this.AddGameObject(gridLayout, position, "Default");
        }
        // TODO
        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var worldPos = gridLayout.CellToWorld(position);
            XSInstance.Instance.GridHelperEditMode.RemoveXSTile(worldPos);
        }
    }

    [CustomEditor(typeof(XSPrefabBrush))]
    public class XSPrefabBrushEditor : XSBrushBaseEditor<XSPrefabBrush, Transform>
    {
    }
}

#endif