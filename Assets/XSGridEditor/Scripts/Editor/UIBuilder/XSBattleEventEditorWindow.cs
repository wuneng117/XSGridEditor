using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace XSSLG
{
    public class XSBattleEventEditorWindow : EditorWindow
    {
        protected VisualElement root;

        [MenuItem("Window/UI Toolkit/XSBattleEventEditorWindow")]
        public static void ShowExample()
        {
            XSBattleEventEditorWindow wnd = GetWindow<XSBattleEventEditorWindow>();
            wnd.titleContent = new GUIContent("XSBattleEventEditorWindow");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            this.root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(XSEditorDefine.UI_BUILDER_UXML_PATH + "XSBattleEventEditorWindow.uxml");
            visualTree.CloneTree(this.root);

            var menu1 = this.root.Q<ToolbarMenu>("menu1");
            menu1.menu.AppendAction("menu1", (a) =>
            {
                Debug.Log("menu1");
            });
        }
    }
}
