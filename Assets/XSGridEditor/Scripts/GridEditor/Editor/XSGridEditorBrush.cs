/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/15
/// @Description: 简单的画prefab的画笔，实现以下功能
/// 1.画完之后，把每个 tile 的 y 设置到障碍物的顶端
/// </summary>
/// 
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace XSSLG
{
    [CustomGridBrush(true, false, true, "XSGridEditor Brush")]
    public class XSGridEditorBrush : GridBrushBase
    {
        [SerializeField]
        private BrushTile tile;

        /// <summary>
        /// Paints GameObjects into a given position within the selected layers.
        /// The GameObjectBrushEx overrides this to provide GameObject painting functionality.
        /// </summary>
        /// <param name="gridLayout">Grid used for layout.</param>
        /// <param name="brushTarget">Target of the paint operation. By default the currently selected GameObject.</param>
        /// <param name="position">The coordinates of the cell to paint data to.</param>
        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            this.PaintTile(gridLayout, position, brushTarget != null ? brushTarget.transform : null, tile);
        }

        private void PaintTile(GridLayout grid, Vector3Int position, Transform parent, BrushTile tile)
        {
            //TODO 要确认下，如果position是tile坐标，需要转成世界坐标
            var worldPos = Vector3.zero;
            if (tile.gameObject == null)
                return;

            var existTile = XSUE.GetGridMgr().GetTile(position);
            if (existTile != null)
                return;
                
            GameObject instance;
            if (PrefabUtility.IsPartOfPrefabAsset(tile.gameObject))
                instance = (GameObject)PrefabUtility.InstantiatePrefab(tile.gameObject, parent) as GameObject;
            else
            {
                instance = Instantiate(tile.gameObject, parent);
                instance.name = tile.gameObject.name;
                instance.SetActive(true);
                foreach (var renderer in instance.GetComponentsInChildren<Renderer>())
                    renderer.enabled = true;
            }

            instance.transform.position = XSUE.GetGridMgr().WorldToTileCenterWorld(worldPos);
            //添加到TileDict
            var ret = XSGridHelperEditMode.Instance?.AddXSTile(instance.GetComponent<XSTileData>());

            if (ret)
                Undo.RegisterCreatedObjectUndo(instance, "Paint GameObject");
            else
            {
                Debug.LogError("AddXSTileData failed");
                GameObject.DestroyImmediate(instance);
            }
        }

        /// <summary>
        /// Erases GameObjects in a given position within the selected layers.
        /// The GameObjectBrushEx overrides this to provide GameObject erasing functionality.
        /// </summary>
        /// <param name="gridLayout">Grid used for layout.</param>
        /// <param name="brushTarget">Target of the erase operation. By default the currently selected GameObject.</param>
        /// <param name="position">The coordinates of the cell to erase data from.</param>
        public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            var existTile = XSUE.GetGridMgr().GetTile(position);
            if (existTile == null || existTile.Node == null)
                return;

            XSGridHelperEditMode.Instance.RemoveXSTile(existTile.Node);
        }

        /// <summary>
        /// Box fills GameObjects into given bounds within the selected layers.
        /// The GameObjectBrushEx overrides this to provide GameObject box-filling functionality.
        /// </summary>
        /// <param name="gridLayout">Grid to box fill data to.</param>
        /// <param name="brushTarget">Target of the box fill operation. By default the currently selected GameObject.</param>
        /// <param name="position">The bounds to box fill data into.</param>
        public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            foreach (Vector3Int location in position.allPositionsWithin)
                this.PaintTile(gridLayout, location, brushTarget, tile);
        }

        public override void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position) => Debug.LogWarning("FloodFill not supported");

        public override void Rotate(RotationDirection direction, GridLayout.CellLayout layout) => Debug.LogWarning("Rotate not supported");

        public override void Flip(FlipAxis flip, GridLayout.CellLayout layout) => Debug.LogWarning("Flip not supported");

        public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pivot) => Debug.LogWarning("Pick not supported");

        public override void MoveStart(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) => Debug.LogWarning("MoveStart not supported");

        public override void MoveEnd(GridLayout gridLayout, GameObject brushTarget, BoundsInt position) => Debug.LogWarning("MoveEnd not supported");

        /// <summary>
        /// Hashes the contents of the brush.
        /// </summary>
        /// <returns>A hash code of the brush</returns>
        public override int GetHashCode()
        {
            int hash = 0;
            unchecked
            {
                hash = hash * 33 + tile.GetHashCode();
            }
            return hash;
        }

        /// <summary>
        ///Brush Cell stores the data to be painted in a grid cell.
        /// </summary>
        [Serializable]
        public class BrushTile
        {
            /// <summary>
            /// GameObject to be placed when painting.
            /// </summary>
            public GameObject gameObject { get => gameObject; set => gameObject = value; }

            [SerializeField]
            private GameObject gameObject;

            /// <summary>
            /// Hashes the contents of the brush cell.
            /// </summary>
            /// <returns>A hash code of the brush cell.</returns>
            public override int GetHashCode()
            {
                int hash;
                unchecked
                {
                    hash = gameObject != null ? gameObject.GetInstanceID() : 0;
                }
                return hash;
            }
        }
    }

    /// <summary>
    /// The Brush Editor for a GameObject Brush.
    /// </summary>
    [CustomEditor(typeof(XSGridEditorBrush))]
    public class XSGridEditorBrushEditor : GridBrushEditorBase
    {
        /// <summary>
        /// The GameObjectBrushEx for this Editor
        /// </summary>
        public XSGridEditorBrush brush { get { return target as XSGridEditorBrush; } }

        /// <summary>
        /// Callback for painting the inspector GUI for the GameObjectBrushEx in the tilemap palette.
        /// The GameObjectBrushEx Editor overrides this to show the usage of this Brush.
        /// </summary>
        public override void OnPaintInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            EditorGUI.EndChangeCheck();
        }

        /// <summary>
        /// The targets that the GameObjectBrushEx can paint on
        /// </summary>
        public override GameObject[] validTargets
        {
            get
            {
                StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
                return currentStageHandle.FindComponentsOfType<GridLayout>().Where(x => x.gameObject.scene.isLoaded && x.gameObject.activeInHierarchy).Select(x => x.gameObject).ToArray();
            }
        }

    }
}