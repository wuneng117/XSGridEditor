/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/15
/// @Description: 简单的画prefab的画笔，实现以下功能
/// 1.画完之后，把每个 tile 的 y 设置到障碍物的顶端
/// </summary>
/// 
#if ENABLE_TILEMAP
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace XSSLG
{
    public class XSBrushBase : GridBrushBase
    {
        [SerializeField]
        public GameObject brushObj;

        protected virtual GameObject AddGameObject(Transform parent, GridLayout gridLayout, Vector3Int position, string layerName)
        {
            var obj = this.brushObj?.gameObject;
            if (obj == null)
                return null;


            GameObject tileObj;
            if (PrefabUtility.IsPartOfPrefabAsset(obj))
                tileObj = (GameObject)PrefabUtility.InstantiatePrefab(obj, parent) as GameObject;
            else
            {
                tileObj = Instantiate(obj, parent);
                tileObj.name = obj.name;
            }

            var mgr = XSEditorInstance.Instance.GridMgr;
            var worldPos = gridLayout.CellToWorld(position);
            tileObj.transform.position = mgr.WorldToTileCenterWorld(worldPos);
            tileObj.layer = LayerMask.NameToLayer(layerName);
            return tileObj;
        }

        protected virtual bool IsExistTile(GridLayout gridLayout, Vector3Int position)
        {
            var mgr = XSEditorInstance.Instance.GridMgr;
            var worldPos = gridLayout.CellToWorld(position);
            var existTile = mgr.GetXSTile(worldPos);
            return existTile != null;
        }

        /// <param name="position">The coordinates of the cell to erase data from.</param>
        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var worldPos = gridLayout.CellToWorld(position);
            XSEditorInstance.Instance.GridHelperEditMode.RemoveXSTile(worldPos);
        }

        public override void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position) => Debug.LogWarning("FloodFill failed");

        public override void Rotate(RotationDirection direction, GridLayout.CellLayout layout) => Debug.LogWarning("Rotate failed");

        public override void Flip(FlipAxis flip, GridLayout.CellLayout layout) => Debug.LogWarning("Flip failed");

        public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pivot) => Debug.LogWarning("Pick failed");

        public override void MoveStart(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) => Debug.LogWarning("MoveStart failed");

        public override void MoveEnd(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) => Debug.LogWarning("MoveEnd failed");
    }
}

#endif