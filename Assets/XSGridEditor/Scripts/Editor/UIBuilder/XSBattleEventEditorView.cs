using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class XSBattleEventEditorView : EditorWindow
{
    protected VisualElement root;

    [MenuItem("Window/UI Toolkit/XSBattleEventEditorView")]
    public static void ShowExample()
    {
        XSBattleEventEditorView wnd = GetWindow<XSBattleEventEditorView>();
        wnd.titleContent = new GUIContent("XSBattleEventEditorView");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        this.root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/XSBattleEventEditorView.uxml");
        visualTree.CloneTree(this.root);

        var menu1 = this.root.Q<ToolbarMenu>("menu1");
        menu1.menu.AppendAction("menu1", (a) => 
        { 
            Debug.Log("menu1"); 
        });
    }
}