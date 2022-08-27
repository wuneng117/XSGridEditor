/// <summary>
/// @Author: xiaoshi
/// @Date: 2022-07-19 16:33:13
/// @Description: 弹窗警告框
/// </summary>
using System;
using UnityEditor;
using UnityEngine;

namespace XSSLG
{
    public class XSPopUpView : EditorWindow
    {
        private string Desc { get; set; } = "";

        public void Init(float width, float height, string desc)
        {
            var instance = XSInstance.Instance;
            this.position = new Rect(Screen.width / 2, Screen.height / 2, width, height);
            this.Desc = desc;
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField(this.Desc, EditorStyles.wordWrappedLabel);
            GUILayout.Space(this.position.height / 2);
            if (GUILayout.Button("OK"))
            {
                this.Close();
            }
        }
    }
}
