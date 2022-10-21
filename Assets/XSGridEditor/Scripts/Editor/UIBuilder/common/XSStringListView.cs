using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

namespace XSSLG
{

    public class XSStringListView<T> : VisualElement where T : class
    {
        protected ListView listview;

        public XSUnitNode SelectUnit { get; protected set; }
        protected Action<T> okFunc { get; }

        public XSStringListView(List<T> itemList, Action<T> okFunc)
        {
            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/common/XSStringListView.uxml");
            visualTree.CloneTree(this);

            this.listview = this.Q<ListView>("unitlist");
            this.listview.XSInit(new List<T>(),
                () =>  new VisualElement(),
                this.BindListItem
            );
            // this.listview.onItemsChosen += this.OnChosenItem;

            this.RefreshView(itemList);
        }

        public void RefreshView(List<T> itemList)
        {
            listview.itemsSource = itemList;
            if (itemList.Count > 0)
            {
                listview.selectedIndex = 0;
            }
        }

        protected virtual void BindListItem(VisualElement node, T obj)
        {
            var label = new Label();
            // label.text = obj.ToString();
            node.Add(label);
        }

        // 可以做点其他事情, 比如打开对应编辑器直接编辑
        // protected virtual void OnChosenItem(IEnumerable<object> obj)
        // {
        //     foreach (var item in obj)
        //     {
        //         Debug.Log((item as XSUnitNode).gameObject.name);
        //     }
        // }

        protected virtual void okBtnClickEvent()
        {
            if (this.okFunc != null)
            {
                this.okFunc(this.listview.selectedItem as T);
            }
        }
    }

}