using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

namespace XSSLG
{
    public class XSListView<T> : XSBaseView where T : class, XSIListViewData
    {
        protected override string UXMLPath { get; } = "Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/common/XSListView.uxml";

        protected ListView Listview { get; set; }

        public XSListView(List<T> itemList, string title, Action addFunc, Action<T> removeFunc) : base()
        {
            this.Listview = this.Q<ListView>("listview");
            this.Listview.XSInitEX(itemList);

            this.Q<Label>("title").text = title;
            this.Q<Button>("add_btn")?.RegisterCallback<ClickEvent>(evt => addFunc?.Invoke());
            this.Q<Button>("remove_btn")?.RegisterCallback<ClickEvent>(evt => removeFunc?.Invoke(this.Listview.selectedItem as T));
        }

        public void RefreshView(List<T> itemList)
        {
            this.Listview.itemsSource = itemList;
            if (itemList.Count > 0)
            {
                this.Listview.selectedIndex = 0;
            }
        }
    }
}