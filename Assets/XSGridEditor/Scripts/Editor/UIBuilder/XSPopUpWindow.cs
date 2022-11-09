using UnityEngine;
using UnityEngine.UIElements;

namespace XSSLG
{
    public class XSPopUpWindow : XSBaseWindow
    {
        protected override string UXMLPath { get; } = XSEditorDefine.UI_BUILDER_UXML_PATH + "XSPopUpWindow.uxml";

        public static void ShowExample(string desc)
        {
            var wnd = GetWindowWithRect<XSPopUpWindow>(new Rect(0, 0, 300, 200), false, "XSPopUpWindow");
            var descLabel = wnd.Root.Q<Label>("desc");
            descLabel.text = desc;
            wnd.ShowModal();
        }

        public override void CreateGUI()
        {
            base.CreateGUI();
            var btn = this.Root.Q<Button>("btn");
            btn.clicked += ClickEvent;
        }

        protected virtual void ClickEvent()
        {
            this.Close();
        }
    }
}