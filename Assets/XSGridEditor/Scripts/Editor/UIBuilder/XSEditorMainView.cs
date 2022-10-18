using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class XSEditorMainView : EditorWindow
{
    protected VisualElement root;
    [MenuItem("Window/UI Toolkit/XSEditorMainView")]
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
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/XSGridEditor/Scripts/Editor/UIBuilder/XSEditorMainView.uxml");
        visualTree.CloneTree(this.root);

        // var menu1 = this.root.Q<ToolbarMenu>("menu1");
        // menu1.menu.AppendAction("menu1", (a) => 
        // { 
        //     Debug.Log("menu1"); 
        // });
    }
}