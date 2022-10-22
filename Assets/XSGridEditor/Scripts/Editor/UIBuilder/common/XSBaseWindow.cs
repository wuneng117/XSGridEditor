using UnityEditor;
using UnityEngine.UIElements;

namespace XSSLG
{

    public abstract class XSBaseWindow : EditorWindow
    {
        protected abstract string UXMLPath { get; }
        protected VisualElement Root { get; set; }

        public virtual void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            this.Root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(this.UXMLPath);
            visualTree.CloneTree(this.Root);
        }
    }
}