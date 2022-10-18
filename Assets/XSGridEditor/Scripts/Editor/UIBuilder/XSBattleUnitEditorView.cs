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

        [MenuItem("Window/UI Toolkit/XSBattleUnitEditorView")]
        public static void ShowExample()
        {
            XSBattleUnitEditorView wnd = GetWindow<XSBattleUnitEditorView>();
            wnd.titleContent = new GUIContent("XSBattleUnitEditorView");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            this.root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/XSGridEditor/Scripts/Editor/UIBuilder/XSBattleUnitEditorView.uxml");
            visualTree.CloneTree(this.root);

            this.listview = this.root.Q<ListView>("unitlist");

            // 设置页签
            var toolbar_toggle = this.root.Q("toolbar_toggle");
            XSUE.XSInitToggle(toolbar_toggle?.Children().Select(child => child as ToolbarToggle).ToList(), this.RefreshView);
        }

        protected virtual void RefreshView(int index)
        {
            if (XSUE.UnitMgrEditMode == null)
            {
                return;
            }

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
    }
}