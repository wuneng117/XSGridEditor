using UnityEditor;
using UnityEngine.UIElements;

namespace XSSLG
{

    public abstract class XSBaseView : VisualElement
    {
        protected abstract string UXMLPath { get; }
        public XSBaseView() : base()
        {
            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(this.UXMLPath);
            visualTree.CloneTree(this);
        }
    }
}