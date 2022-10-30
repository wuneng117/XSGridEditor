
using UnityEngine;

namespace XSSLG
{
    public class XSStatEditorWindow : XSBaseWindow
    {
        protected override string UXMLPath { get; } = "Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/common/XSStatEditorWindow.uxml";

        public static void ShowExample()
        {
            var wnd = GetWindowWithRect<XSStatEditorWindow>(new Rect(0, 0, 503, 600), false, "XSStatEditor");
            // wnd.Init();
            wnd.ShowModal();
        }
    }
}
