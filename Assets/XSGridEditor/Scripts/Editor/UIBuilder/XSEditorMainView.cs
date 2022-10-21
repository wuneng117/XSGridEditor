using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace XSSLG
{

    public class XSEditorMainView : EditorWindow
    {
        protected VisualElement root;
        [MenuItem("XSSRPGEngine/XSEditorMainView")]
        public static void ShowExample()
        {
            XSEditorMainView wnd = GetWindow<XSEditorMainView>();
            wnd.titleContent = new GUIContent("XSEditorMainView");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            this.root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/XSEditorMainView.uxml");
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