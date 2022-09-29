/// <summary>
/// @Author: xiaoshi
/// @Date: 2022/2/2
/// @Description: XSGridHelperEditMode editor
/// </summary>
using System;
using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    /// <summary> XSGridHelperEditMode editor </summary>
    [CustomEditor(typeof(XSGridHelperEditMode))]
    public class XSGridHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            this.DrawDefaultInspector();

            var helper = (XSGridHelperEditMode)target;

            this.DrawButton("Update Tile", helper.SetTileToNearTerrain);

            this.DrawButton(!helper.IsShowTilePos ? "Show Tile Position" : "Hide Tile Position", 
                            () => helper.SetTilePosShow(!helper.IsShowTilePos)
            );

            this.DrawButton(!helper.IsShowTileCost ? "Show Tile Cost" : "Hide Tile Cost", 
                            () => helper.SetTileCostShow(!helper.IsShowTileCost)
            );

            this.DrawButton("Remove All Tiles", helper.ClearTiles);
        }

        /// <summary>
        /// First empty 2 lines, then draw a button
        /// </summary>
        /// <param name="name">button display text</param>
        /// <param name="callback">button execution callback</param>
        protected virtual void DrawButton(string name, Action callback)
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