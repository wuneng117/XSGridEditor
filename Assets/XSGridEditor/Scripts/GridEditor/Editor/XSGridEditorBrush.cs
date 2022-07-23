/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/1/15
/// @Description: 复制官方的类 GameObjectBrush ，做了以下一些修改
/// 1.画完之后，把每个 tile 的 y 设置到障碍物的顶端
/// 2.画完时调整了 y ，所以要假设 y=0 才能正确获得 child
/// </summary>
/// 
using System;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using XSSLG;
using Object = UnityEngine.Object;

namespace UnityEditor.Tilemaps
{
    /// <summary>
    /// This Brush instances, places and manipulates GameObjects onto the scene.
    /// Use this as an example to create brushes which targets objects other than tiles for manipulation.
    /// </summary>
    [CustomGridBrush(true, false, false, "XSGridEditor Brush")]
    public class XSGridEditorBrush : GridBrushBase
    {
        [SerializeField]
        private BrushCell m_Cell;

        private Vector3 m_Anchor = new Vector3(0.5f, 0.5f, 0.5f);

        // /// <summary>
        // /// This Brush instances, places and manipulates GameObjects onto the scene.
        // /// </summary>
        // public GameObjectBrushEx()
        // {
        // }


        /// <summary>
        /// Paints GameObjects into a given position within the selected layers.
        /// The GameObjectBrushEx overrides this to provide GameObject painting functionality.
        /// </summary>
        /// <param name="gridLayout">Grid used for layout.</param>
        /// <param name="brushTarget">Target of the paint operation. By default the currently selected GameObject.</param>
        /// <param name="position">The coordinates of the cell to paint data to.</param>
        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            GetGrid(ref gridLayout, ref brushTarget);
            PaintCell(gridLayout, position, brushTarget != null ? brushTarget.transform : null, m_Cell);
        }

        private void PaintCell(GridLayout grid, Vector3Int position, Transform parent, BrushCell cell)
        {
            if (cell.gameObject == null)
                return;

            var existingGO = GetObjectInCell(grid, parent, position);
            if (existingGO == null)
            {
                SetSceneCell(grid, parent, position, cell.gameObject, m_Anchor);
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
            GetGrid(ref gridLayout, ref brushTarget);
            EraseCell(gridLayout, position, brushTarget != null ? brushTarget.transform : null);
        }

        private void EraseCell(GridLayout grid, Vector3Int position, Transform parent)
        {
            ClearSceneCell(grid, parent, position);
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
            GetGrid(ref gridLayout, ref brushTarget);
            
            foreach (Vector3Int location in position.allPositionsWithin)
            {
                Vector3Int local = location - position.min;
                BrushCell cell = m_Cell;
                PaintCell(gridLayout, location, brushTarget != null ? brushTarget.transform : null, cell);
            }
        }

        /// <summary>
        /// This is not supported but it should floodfill GameObjects starting from a given position within the selected layers.
        /// </summary>
        /// <param name="gridLayout">Grid used for layout.</param>
        /// <param name="brushTarget">Target of the flood fill operation. By default the currently selected GameObject.</param>
        /// <param name="position">Starting position of the flood fill.</param>
        public override void FloodFill(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            Debug.LogWarning("FloodFill not supported");
        }

        /// <summary>
        /// Rotates the brush by 90 degrees in the given direction.
        /// </summary>
        /// <param name="direction">Direction to rotate by.</param>
        /// <param name="layout">Cell Layout for rotating.</param>
        public override void Rotate(RotationDirection direction, GridLayout.CellLayout layout)
        {
            Debug.LogWarning("Rotate not supported");
        }

        /// <summary>Flips the brush in the given axis.</summary>
        /// <param name="flip">Axis to flip by.</param>
        /// <param name="layout">Cell Layout for flipping.</param>
        public override void Flip(FlipAxis flip, GridLayout.CellLayout layout)
        {
            Debug.LogWarning("Flip not supported");
        }

        /// <summary>
        /// Picks child GameObjects given the coordinates of the cells.
        /// The GameObjectBrushEx overrides this to provide GameObject picking functionality.
        /// </summary>
        /// <param name="gridLayout">Grid to pick data from.</param>
        /// <param name="brushTarget">Target of the picking operation. By default the currently selected GameObject.</param>
        /// <param name="position">The coordinates of the cells to paint data from.</param>
        /// <param name="pivot">Pivot of the picking brush.</param>
        public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt position, Vector3Int pivot)
        {
            Debug.LogWarning("Pick not supported");
        }

        /// <summary>
        /// MoveStart is called when user starts moving the area previously selected with the selection marquee.
        /// The GameObjectBrushEx overrides this to provide GameObject moving functionality.
        /// </summary>
        /// <param name="gridLayout">Grid used for layout.</param>
        /// <param name="brushTarget">Target of the move operation. By default the currently selected GameObject.</param>
        /// <param name="position">Position where the move operation has started.</param>
        public override void MoveStart(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            Debug.LogWarning("MoveStart not supported");
        }

        /// <summary>
        /// MoveEnd is called when user has ended the move of the area previously selected with the selection marquee.
        /// The GameObjectBrushEx overrides this to provide GameObject moving functionality.
        /// </summary>
        /// <param name="gridLayout">Grid used for layout.</param>
        /// <param name="brushTarget">Target of the move operation. By default the currently selected GameObject.</param>
        /// <param name="position">Position where the move operation has ended.</param>
        public override void MoveEnd(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            Debug.LogWarning("MoveEnd not supported");
        }

        private void GetGrid(ref GridLayout gridLayout, ref GameObject brushTarget)
        {
            if (brushTarget != null)
            {
                var targetGridLayout = brushTarget.GetComponent<GridLayout>();
                if (targetGridLayout != null)
                    gridLayout = targetGridLayout;
            }
        }

        private GameObject GetObjectInCell(GridLayout grid, Transform parent, Vector3Int position)
        {
            int childCount;
            GameObject[] sceneChildren = null;
            if (parent == null)
            {
                var scene = SceneManager.GetActiveScene();
                sceneChildren = scene.GetRootGameObjects();
                childCount = scene.rootCount;
            }
            else
            {
                childCount = parent.childCount;
            }

            for (var i = 0; i < childCount; i++)
            {
                var child = sceneChildren == null ? parent.GetChild(i) : sceneChildren[i].transform;
                // 2.画完时调整了y，所以要假设y=0才能正确获得child
                var pos = new Vector3(child.position.x, 0, child.position.z);
                if (position == grid.WorldToCell(pos))
                    return child.gameObject;
            }
            return null;
        }

        private static void SetSceneCell(GridLayout grid, Transform parent, Vector3Int position, GameObject go, Vector3 anchor)
        {
            if (go == null)
                return;

            GameObject instance;
            if (PrefabUtility.IsPartOfPrefabAsset(go))
            {
                instance = (GameObject) PrefabUtility.InstantiatePrefab(go, parent != null ? parent.root.gameObject.scene : SceneManager.GetActiveScene());
                instance.transform.parent = parent;
            }
            else
            {
                instance = Instantiate(go, parent);
                instance.name = go.name;
                instance.SetActive(true);
                foreach (var renderer in instance.GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = true;
                }
            }

            Undo.RegisterCreatedObjectUndo(instance, "Paint GameObject");
            instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3Int(position.x, position.y, position.z) + anchor));

            //1.画完之后，把每个tile的高度设置到障碍物的顶端
            if (XSGridHelperEditMode.Instance != null)
            {

                var ret = XSGridHelperEditMode.Instance.AddXSTile(instance.GetComponent<XSTileData>());
                if (!ret)
                {
                    Debug.LogError("AddXSTileData failed");
                    GameObject.DestroyImmediate(instance);
                }
            }
        }

        private void ClearSceneCell(GridLayout grid, Transform parent, Vector3Int position)
        {
            GameObject erased = GetObjectInCell(grid, parent, new Vector3Int(position.x, position.y, position.z));
            if (erased != null)
                XSGridHelperEditMode.Instance.RemoveXSTile(erased.GetComponent<XSTileData>());

        }

        /// <summary>
        /// Hashes the contents of the brush.
        /// </summary>
        /// <returns>A hash code of the brush</returns>
        public override int GetHashCode()
        {
            int hash = 0;
            unchecked
            {
                hash = hash * 33 + m_Cell.GetHashCode();
            }
            return hash;
        }

        /// <summary>
        ///Brush Cell stores the data to be painted in a grid cell.
        /// </summary>
        [Serializable]
        public class BrushCell
        {
            /// <summary>
            /// GameObject to be placed when painting.
            /// </summary>
            public GameObject gameObject { get { return m_GameObject; } set { m_GameObject = value; } }
            
            [SerializeField]
            private GameObject m_GameObject;


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