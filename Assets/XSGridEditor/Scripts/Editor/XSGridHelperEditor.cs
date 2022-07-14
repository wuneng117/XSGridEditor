/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: unity tilemap 画格子扩展类 XGGridHelper 的 UI 显示
/// </summary>
using System;
using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    /// <summary> unity tilemap 画格子扩展类 XGGridHelper 的 UI 显示 </summary>
    [CustomEditor(typeof(XSGridHelperEditMode))]
    public class XSGridHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            this.DrawDefaultInspector();

            var helper = (XSGridHelperEditMode)target;

            // 将地体块贴到合适的地方
            this.DrawButton("tile贴到地面", helper.SetTileToNearTerrain);

            // 显示tilepos
            this.DrawButton(!helper.IsShowTilePos ? "显示tile坐标" : "隐藏tile坐标", 
                            () => helper.SetTilePosShow(!helper.IsShowTilePos)
            );

            // 显示tilepos
            this.DrawButton(!helper.IsShowTileCost ? "显示tile移动消耗" : "隐藏tile移动消耗", 
                            () => helper.SetTileCostShow(!helper.IsShowTileCost)
            );

            this.DrawButton("删除所有tile", helper.ClearTiles);

            this.DrawButton("创建1个XSObject", helper.CreateObject);

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