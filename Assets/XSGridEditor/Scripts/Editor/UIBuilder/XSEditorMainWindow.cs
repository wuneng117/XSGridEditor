using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace XSSLG
{

    public class XSEditorMainWindow : EditorWindow
    {
        protected VisualElement root;
        [MenuItem("XSSRPGEngine/XSEditorMainWindow")]
        public static void ShowExample()
        {
            XSEditorMainWindow wnd = GetWindowWithRect<XSEditorMainWindow>(new Rect(0, 0, 300, 200), false, "XSEditor");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            this.root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/XSEditorMainWindow.uxml");
            visualTree.CloneTree(this.root);

            this.CreateTestMenu();

            var battleUnitBtn = this.root.Q<Button>("battleUnitBtn");
            if (battleUnitBtn != null)
            {
                battleUnitBtn.clickable.clicked += () => XSBattleUnitEditorView.ShowExample();
            }
        }

        protected virtual void CreateTestMenu()
        {
            var test = this.root.Q<ToolbarMenu>("test");
            if (test == null)
            {
                return;
            }

            test.menu.AppendAction("XSBattleEventEditorView", (a) => XSBattleEventEditorView.ShowExample());
        }

    }
}