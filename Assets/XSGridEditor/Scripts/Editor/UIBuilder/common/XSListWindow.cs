using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

namespace XSSLG
{

    public class XSListWindow : XSBaseWindow
    {
        protected override string UXMLPath { get; } = XSEditorDefine.UI_BUILDER_UXML_PATH + "common/XSListWindow.uxml";
        protected ListView Listview { get; set; }

        public static void ShowExample<T>(List<T> itemList, string title, Action<T> okFunc) where T : class, XSIListViewData
        {
            var wnd = CreateWindow<XSListWindow>(title);
            wnd.Init(itemList, okFunc);
            wnd.ShowModal();
        }

        protected virtual void Init<T>(List<T> itemList, Action<T> okFunc) where T : class, XSIListViewData
        {
            this.Listview = this.Root.Q<ListView>("listview");
            this.Listview.XSInitEX(itemList);

            this.Root.Q<Button>("ok_btn")?.RegisterCallback<ClickEvent>(evt => 
            {
                okFunc?.Invoke(this.Listview.selectedItem as T);
                this.Close();
            });
            this.Root.Q<Button>("cancel_btn")?.RegisterCallback<ClickEvent>(evt => this.Close());
        }
    }
}