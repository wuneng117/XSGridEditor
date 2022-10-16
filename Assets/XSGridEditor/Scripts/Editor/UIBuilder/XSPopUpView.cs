using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace XSSLG
{

    public class XSPopUpView : EditorWindow
    {
        private string Desc { get; set; }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/XSGridEditor/Scripts/XSEditor/Editor/UIBuilder/XSPopUpView.uxml");
            visualTree.CloneTree(root);

            var btn = root.Q<Button>("btn");
            btn.clicked += ClickEvent;

            var descLabel = root.Q<Label>("desc");
            descLabel.text = Desc;
        }

        public void Init(string desc)
        {
            this.Desc = desc;
        }

        private void ClickEvent()
        {
            this.Close();
        }
    }
}