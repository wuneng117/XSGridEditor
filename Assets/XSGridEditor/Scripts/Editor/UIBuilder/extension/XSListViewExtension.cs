using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace XSSLG
{

    public static class XSListViewExtension
    {
        /// <summary>
        /// extension ListView Init
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void XSInit<T>(this ListView listview, List<T> itemsSource, string itemAssetPath, Action<VisualElement, T> bindFunc) where T : class
        {
            listview.XSInit(itemsSource, itemAssetPath, bindFunc, (item) => { });
        }

        /// <summary>
        /// extension ListView Init
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void XSInit<T>(this ListView listview, List<T> itemsSource, string itemAssetPath, Action<VisualElement, T> bindFunc, Action<T> selFunc) where T : class
        {
            listview.makeItem = () =>
                {
                    var node = new VisualElement();
                    var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(itemAssetPath);
                    visualTree?.CloneTree(node);
                    var texture = node.Q<VisualElement>("texture");
                    if (texture != null)
                    {
                        texture.style.maxWidth = listview.itemHeight;
                    }
                    return node;
                };
            listview.InitFunc(itemsSource, bindFunc, selFunc);
        }

        /// <summary>
        /// extension ListView Init
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void XSInit<T>(this ListView listview, List<T> itemsSource, Func<VisualElement> makeFunc, Action<VisualElement, T> bindFunc) where T : class
        {
            listview.XSInit(itemsSource, makeFunc, bindFunc, (item) => { });
        }

        /// <summary>
        /// extension ListView Init
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void XSInit<T>(this ListView listview, List<T> itemsSource, Func<VisualElement> makeFunc, Action<VisualElement, T> bindFunc, Action<T> selFunc) where T : class
        {
            listview.makeItem = makeFunc;
            listview.InitFunc(itemsSource, bindFunc, selFunc);
        }

        private static void InitFunc<T>(this ListView listview, List<T> itemsSource, Action<VisualElement, T> bindFunc, Action<T> selFunc) where T : class
        {
            listview.itemsSource = itemsSource;
            listview.bindItem = (VisualElement node, int index) =>
            {
                var obj = listview.itemsSource[index];
                if (obj != null)
                {
                    bindFunc(node, obj as T);
                }
            };

            listview.onSelectionChange += (IEnumerable<object> obj) =>
            {
                foreach (var o in obj)
                {
                    selFunc(o as T);
                }
            };
        }

        /// <summary>
        /// extension ListView Init
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void XSInitEX<T>(this ListView listview, List<T> itemsSource) where T : class, XSIListViewData
        {
            listview.XSInitEX(itemsSource, (item) => { });
        }

        public static void XSInitEX<T>(this ListView listview, List<T> itemsSource, Action<T> selFunc) where T : class, XSIListViewData
        {
            listview.XSInit(itemsSource,
                "Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/common/XSListViewItem.uxml",
                (node, obj) =>
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
            );
            // 可以做点其他事情, 比如打开对应编辑器直接编辑
            // this.listview.onItemsChosen += (IEnumerable<object> obj)
            // {
            //     foreach (var item in obj)
            //     {
            //         Debug.Log((item as XSUnitNode).gameObject.name);
            //     }
            // }
            if (itemsSource.Count > 0)
            {
                listview.selectedIndex = 0;
            }
        }
    }
}
