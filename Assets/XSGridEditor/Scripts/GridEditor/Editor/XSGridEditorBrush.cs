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
    [CustomGridBrush(true, false, true, "XSGridEditor Brush")]
    public class XSGridEditorBrush : XSBrushBase
    {
        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
            var curHe = currentStageHandle.FindComponentsOfType<XSGridHelperEditMode>();
            if (this.brushObj?.gameObject == null)
                return;

            if (this.IsExistTile(gridLayout, position))
                return;


            var tileObj = this.AddGameObject(brushTarget.transform, gridLayout, position, XSGridDefine.LAYER_TILE);
            if (tileObj == null)
                return;

            //添加到TileDict
            var tile = XSInstance.Instance.GridHelperEditMode?.AddXSTile(tileObj.GetComponent<XSITileNode>());
            if (tile == null)
            {
                Debug.LogError("AddXSTileNode failed");
                GameObject.DestroyImmediate(tileObj);
            }
        }
    }

    [CustomEditor(typeof(XSGridEditorBrush))]
    public class XSGridEditorBrushEditor : GridBrushEditorBase
    {
        public Vector2 scrollPosition = Vector2.zero;
        public override bool canChangeZPosition { get => false; }

        /// <summary>
        /// Callback for painting the inspector GUI for the GameObjectBrushEx in the tilemap palette.
        /// The GameObjectBrushEx Editor overrides this to show the usage of this Brush.
        /// </summary>
        public override void OnPaintInspectorGUI()
        {
            var brush = (XSGridEditorBrush)target;

            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();


            EditorGUI.EndChangeCheck();

            var startPosY = 100;
            scrollPosition = GUI.BeginScrollView(new Rect(10, startPosY, 600, 200), scrollPosition, new Rect(0, 0, 300, 1000));
            for (int i = 1; i < 50; i++)
            {
                var texture = AssetPreview.GetAssetPreview(brush.brushObj) as Texture;
                var pos = new Rect(0, 30 * i, texture.width / 3, texture.height / 3);
                GUI.DrawTextureWithTexCoords(pos, texture, new Rect(0, 0, 1, 1)); // new Rect(0, 0, 1, 1)意思是从0， 0开始画，横竖只绘制1次
                GUI.color = new Color(0, 1f, 1f, 0.5f);
            }

            // if (GUI.Button(pos, ""))
            // GUI.color = new Color(0, 1f, 1f, 1f);

            GUI.EndScrollView();
        }

        public override GameObject[] validTargets
        {
            get
            {
                StageHandle currentStageHandle = StageUtility.GetCurrentStageHandle();
                return currentStageHandle.FindComponentsOfType<GridLayout>().Select(grid => grid.gameObject)
                                                                            .Where(gameObject => gameObject.scene.isLoaded && gameObject.activeInHierarchy && gameObject.name == "TileRoot").ToArray();
            }
        }

    }
}

#endif