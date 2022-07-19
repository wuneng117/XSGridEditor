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
    public class PopUpView : EditorWindow
    {
        private string Desc { get; set; } = "";

        public void Init(Size size, string desc)
        {
            this.position = new Rect(Screen.width / 2, Screen.height / 2, size.Width, size.Height);
            this.Desc = desc;
        }
        void OnGUI()
        {
            EditorGUILayout.LabelField(this.Desc, EditorStyles.wordWrappedLabel);
            GUILayout.Space(this.height / 3);
            // 按钮大概大小就行了
            if (GUILayout.Button(new Rect(this.width / 2, this.height / 2, this.width / 3.5, this.height / 6), "Agree"))
                this.Close();
        }
    }
}