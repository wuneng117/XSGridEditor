/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: unity Grid 画格子扩展类 XGGridHelper 的 UI 显示
/// </summary>
using System;
using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    /// <summary> unity Grid 画格子扩展类 XGGridHelper 的 UI 显示 </summary>
    [CustomEditor(typeof(XSGridHelperEditMode))]
    public class XSGridHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            this.DrawDefaultInspector();

            var helper = (XSGridHelperEditMode)target;

            // 将地体块贴到合适的地方
            this.DrawButton("Update Tile", helper.SetTileToNearTerrain);

            // 显示tilepos
            this.DrawButton(!helper.IsShowTilePos ? "Show Tile Position" : "Hide Tile Position", 
                            () => helper.SetTilePosShow(!helper.IsShowTilePos)
            );

            // 显示tilepos
            this.DrawButton(!helper.IsShowTileCost ? "Show Tile Cost" : "Hide Tile Cost", 
                            () => helper.SetTileCostShow(!helper.IsShowTileCost)
            );

            this.DrawButton("Remove All Tiles", helper.ClearTiles);

            this.DrawButton("Create A XSUnit", helper.CreateObject);

            // this.DrawButton("对齐所有XSObject", helper.SetObjectToTileCenter);
        }

        /// <summary>
        /// 先空2行，然后画个按钮
        /// </summary>
        /// <param name="name">按钮显示文字</param>
        /// <param name="callback">按钮执行回调</param>
        private void DrawButton(string name, Action callback)
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button(name))
            {
                callback();
            }
        }
    }
}