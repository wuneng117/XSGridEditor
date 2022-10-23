using UnityEditor;
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
            this.Listview.XSInit(new List<T>(),
                "Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/common/XSListViewItem.uxml",
                this.BindListItem
            );
            // this.listview.onItemsChosen += this.OnChosenItem;

            this.RefreshView(itemList);

            this.Q<Label>("title").text = title;
            this.Q<Button>("add_btn")?.RegisterCallback<ClickEvent>(evt => addFunc?.Invoke());
            this.Q<Button>("remove_btn")?.RegisterCallback<ClickEvent>(evt => removeFunc?.Invoke(this.Listview.selectedItem as T));
        }

        public void RefreshView(List<T> itemList)
        {
            Listview.itemsSource = itemList;
            if (itemList.Count > 0)
            {
                Listview.selectedIndex = 0;
            }
        }

        protected virtual void BindListItem(VisualElement node, T obj)
        {
            var label = node.Q<Label>("label");
            if (label != null)
            {
                label.text = obj.Name;
            }

            var texture = node.Q<VisualElement>("texture");
            if (texture != null)
            {
                texture.style.backgroundImage = obj.Texture;
            }
        }

        // 可以做点其他事情, 比如打开对应编辑器直接编辑
        // protected virtual void OnChosenItem(IEnumerable<object> obj)
        // {
        //     foreach (var item in obj)
        //     {
        //         Debug.Log((item as XSUnitNode).gameObject.name);
        //     }
        // }
    }
}