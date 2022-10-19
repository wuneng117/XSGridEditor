using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;

namespace XSSLG
{

    public class XSBattleUnitEditorView : EditorWindow
    {
        protected VisualElement root;
        private ListView listview;

        protected XSUnitEditorView UnitEditorView { get; set; }

        public XSUnitNode SelectUnit { get; protected set; }

        public static void ShowExample()
        {
            XSBattleUnitEditorView wnd = GetWindow<XSBattleUnitEditorView>();
            wnd.titleContent = new GUIContent("XSBattleUnitEditorView");
        }

        public void CreateGUI()
        {
            UnityEditor.SceneManagement.EditorSceneManager.sceneOpened += this.SceneOpened;

            // Each editor window contains a root VisualElement object
            this.root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/XSBattleUnitEditorView.uxml");
            visualTree.CloneTree(this.root);

            this.listview = this.root.Q<ListView>("unitlist");

            // 设置页签
            var toolbar_toggle = this.root.Q("toolbar_toggle");
            XSUE.XSInitToggle(toolbar_toggle?.Children().Select(child => child as ToolbarToggle).ToList(), this.RefreshUnitEditerView);
        }

        protected virtual void RefreshUnitEditerView(int index)
        {
            if (this.UnitEditorView == null)
            {
                this.UnitEditorView = new XSUnitEditorView(index);
                this.root.Q("content").Add(this.UnitEditorView);
            }
            else
            {
                this.UnitEditorView.RefreshView(index);
            }
        }

        protected void SceneOpened(UnityEngine.SceneManagement.Scene scene, UnityEditor.SceneManagement.OpenSceneMode mode)
        {
            this.RefreshUnitEditerView(1);
        }
    }
}