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
    [HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/GameObjectBrush.html")]
    [CustomGridBrush(true, false, false, "GameObject BrushEx")]
    public class GameObjectBrushEx : GridBrushBase
    {
        [SerializeField]
        private BrushCell[] m_Cells;

        [SerializeField]
        private Vector3Int m_Size;

        [SerializeField]
        [HideInInspector]
        private bool m_CanChangeZPosition;

        /// <summary>Size of the brush in cells. </summary>
        public Vector3Int size { get { return m_Size; } set { m_Size = value; SizeUpdated(); } }
        /// <summary>All the brush cells the brush holds. </summary>
        public BrushCell[] cells { get { return m_Cells; } }
        /// <summary>Number of brush cells in the brush.</summary>
        public int cellCount { get { return m_Cells != null ? m_Cells.Length : 0; } }
        /// <summary>Number of brush cells based on size.</summary>
        public int sizeCount
        {
            get { return m_Size.x * m_Size.y * m_Size.z; }
        }
        /// <summary>Whether the brush can change Z Position</summary>
        public bool canChangeZPosition
        {
            get { return m_CanChangeZPosition; }
            set { m_CanChangeZPosition = value; }
        }

        private static XSGridHelperEditMode gridHelperEditMode { get; set; }
        
        /// <summary>
        /// This Brush instances, places and manipulates GameObjects onto the scene.
        /// </summary>
        public GameObjectBrushEx()
        {
            Init(Vector3Int.one, Vector3Int.zero);
            SizeUpdated();
        }

        private void OnEnable()
        {
            gridHelperEditMode = Component.FindObjectOfType<XSGridHelperEditMode>();
        }

        // private void OnDisable()
        // {
        // }

        /// <summary>
        /// Initializes the content of the GameObjectBrushEx.
        /// </summary>
        /// <param name="size">Size of the GameObjectBrushEx.</param>
        public void Init(Vector3Int size)
        {
            Init(size, Vector3Int.zero);
            SizeUpdated();
        }

        /// <summary>Initializes the content of the GameObjectBrushEx.</summary>
        /// <param name="size">Size of the GameObjectBrushEx.</param>
        /// <param name="pivot">Pivot point of the GameObjectBrushEx.</param>
        public void Init(Vector3Int size, Vector3Int pivot)
        {
            m_Size = size;
            SizeUpdated();
        }

        /// <summary>
        /// Paints GameObjects into a given position within the selected layers.
        /// The GameObjectBrushEx overrides this to provide GameObject painting functionality.
        /// </summary>
        /// <param name="gridLayout">Grid used for layout.</param>
        /// <param name="brushTarget">Target of the paint operation. By default the currently selected GameObject.</param>
        /// <param name="position">The coordinates of the cell to paint data to.</param>
        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            Vector3Int min = position;
            BoundsInt bounds = new BoundsInt(min, m_Size);

            GetGrid(ref gridLayout, ref brushTarget);
            BoxFill(gridLayout, brushTarget, bounds);
        }

        private void PaintCell(GridLayout grid, Vector3Int position, Transform parent, BrushCell cell)
        {
            if (cell.gameObject == null)
                return;

            var existingGO = GetObjectInCell(grid, parent, position);
            if (existingGO == null)
            {
                SetSceneCell(grid, parent, position, cell.gameObject);
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
            Vector3Int min = position;
            BoundsInt bounds = new BoundsInt(min, m_Size);

            GetGrid(ref gridLayout, ref brushTarget);
            BoxErase(gridLayout, brushTarget, bounds);
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
                BrushCell cell = m_Cells[GetCellIndexWrapAround(local.x, local.y, local.z)];
                PaintCell(gridLayout, location, brushTarget != null ? brushTarget.transform : null, cell);
            }
        }

        /// <summary>
        /// Erases GameObjects from given bounds within the selected layers.
        /// The GameObjectBrushEx overrides this to provide GameObject box-erasing functionality.
        /// </summary>
        /// <param name="gridLayout">Grid to erase data from.</param>
        /// <param name="brushTarget">Target of the erase operation. By default the currently selected GameObject.</param>
        /// <param name="position">The bounds to erase data from.</param>
        public override void BoxErase(GridLayout gridLayout, GameObject brushTarget, BoundsInt position)
        {
            GetGrid(ref gridLayout, ref brushTarget);
            
            foreach (Vector3Int location in position.allPositionsWithin)
            {
                EraseCell(gridLayout, location, brushTarget != null ? brushTarget.transform : null);
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

        /// <summary>Gets the index to the GameObjectBrushEx::ref::BrushCell based on the position of the BrushCell.</summary>
        /// <param name="brushPosition">Position of the BrushCell.</param>
        /// <returns>The cell index for the position of the BrushCell.</returns>
        public int GetCellIndex(Vector3Int brushPosition)
        {
            return GetCellIndex(brushPosition.x, brushPosition.y, brushPosition.z);
        }

        /// <summary>Gets the index to the GameObjectBrushEx::ref::BrushCell based on the position of the BrushCell.</summary>
        /// <param name="x">X Position of the BrushCell.</param>
        /// <param name="y">Y Position of the BrushCell.</param>
        /// <param name="z">Z Position of the BrushCell.</param>
        /// <returns>The cell index for the position of the BrushCell.</returns>
        public int GetCellIndex(int x, int y, int z)
        {
            return x + m_Size.x * y + m_Size.x * m_Size.y * z;
        }

        /// <summary>Gets the index to the GameObjectBrushEx::ref::BrushCell based on the position of the BrushCell. Wraps each coordinate if it is larger than the size of the GameObjectBrushEx.</summary>
        /// <param name="x">X Position of the BrushCell.</param>
        /// <param name="y">Y Position of the BrushCell.</param>
        /// <param name="z">Z Position of the BrushCell.</param>
        /// <returns>The cell index for the position of the BrushCell.</returns>
        public int GetCellIndexWrapAround(int x, int y, int z)
        {
            return (x % m_Size.x) + m_Size.x * (y % m_Size.y) + m_Size.x * m_Size.y * (z % m_Size.z);
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

        internal void SizeUpdated(bool keepContents = false)
        {
            Array.Resize(ref m_Cells, sizeCount);
            BoundsInt bounds = new BoundsInt(Vector3Int.zero, m_Size);
            foreach (Vector3Int pos in bounds.allPositionsWithin)
            {
                if (keepContents || m_Cells[GetCellIndex(pos)] == null)
                    m_Cells[GetCellIndex(pos)] = new BrushCell();
            }
        }

        private static void SetSceneCell(GridLayout grid, Transform parent, Vector3Int position, GameObject go)
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
            instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3Int(position.x, position.y, position.z)));

            //1.画完之后，把每个tile的高度设置到障碍物的顶端
            if (gridHelperEditMode != null)
            {

                var ret = gridHelperEditMode.AddXSTile(instance.GetComponent<XSTileData>());
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
                gridHelperEditMode.RemoveXSTile(erased.GetComponent<XSTileData>());

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
                foreach (var cell in cells)
                {
                    hash = hash * 33 + cell.GetHashCode();
                }
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
    [CustomEditor(typeof(GameObjectBrushEx))]
    public class GameObjectBrushExEditor : GridBrushEditorBase
    {
        /// <summary>
        /// The GameObjectBrushEx for this Editor
        /// </summary>
        public GameObjectBrushEx brush { get { return target as GameObjectBrushEx; } }

        /// <summary> Whether the GridBrush can change Z Position. </summary>
        public override bool canChangeZPosition
        {
            get { return brush.canChangeZPosition; }
            set { brush.canChangeZPosition = value; }
        }

        /// <summary>
        /// Callback for painting the inspector GUI for the GameObjectBrushEx in the tilemap palette.
        /// The GameObjectBrushEx Editor overrides this to show the usage of this Brush.
        /// </summary>
        public override void OnPaintInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            if (EditorGUI.EndChangeCheck() && brush.cellCount != brush.sizeCount)
            {
                brush.SizeUpdated(true);
            }
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