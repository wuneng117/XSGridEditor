using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace XSSLG
{

    public class XSBattleUnitEditorView : EditorWindow
    {
        protected VisualElement root;
        private ListView listview;

        public XSUnitNode SelectUnit { get; protected set; }

        [MenuItem("Window/UI Toolkit/XSBattleUnitEditorView")]
        public static void ShowExample()
        {
            XSBattleUnitEditorView wnd = GetWindow<XSBattleUnitEditorView>();
            wnd.titleContent = new GUIContent("XSBattleUnitEditorView");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            this.root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/XSGridEditor/Scripts/Editor/UIBuilder/XSBattleUnitEditorView.uxml");
            visualTree.CloneTree(this.root);

            this.listview = this.root.Q<ListView>("unitlist");

            // 设置页签
            var toolbar_toggle = this.root.Q("toolbar_toggle");
            XSUE.XSInitToggle(toolbar_toggle?.Children().Select(child => child as ToolbarToggle).ToList(), this.RefreshView);
        }

        protected virtual void RefreshView(int index)
        {
            if (XSUE.UnitMgrEditMode == null)
            {
                return;
            }

            var sceneObjects = XSUE.UnitMgrEditMode.Dict.Select(pair => (XSUnitNode)pair.Value).ToList();
            if (sceneObjects == null)
            {
                return;
            }

            sceneObjects = sceneObjects.Where(obj => (int)obj.Group == index).ToList();
            this.listview.XSInit(sceneObjects,
                "Assets/XSGridEditor/Scripts/Editor/UIBuilder/XSBattleUnitEditorView_ListItem.uxml", 
                this.BindListItem,
                this.OnSelectionItem
            );
            this.listview.onItemsChosen += this.OnChosenItem;
        }

        private void OnChosenItem(IEnumerable<object> obj)
        {
            foreach(var item in obj)
            {
                Debug.Log((item as XSUnitNode).gameObject.name);
            }
        }

        protected virtual void BindListItem(VisualElement node, XSUnitNode obj)
        {
            var texture = node.Q<VisualElement>("texture");
            if (texture != null && UnityEditor.PrefabUtility.IsPartOfPrefabInstance(obj.gameObject))
            {
                var prefab = UnityEditor.PrefabUtility.GetCorrespondingObjectFromOriginalSource(obj.gameObject);
                texture.style.backgroundImage = AssetPreview.GetAssetPreview(prefab);
                // texture.style.backgroundImage = Resources.Load<Texture2D>("path_to_png");
            }

            var label = node.Q<Label>("label");
            label?.XSInit(obj, "data.name");
        }

        private void OnSelectionItem(XSUnitNode obj)
        {
            Selection.activeGameObject = obj.gameObject;
            this.SelectUnit = obj;
            var name_text = this.root.Q<TextField>("name_text");
            name_text?.XSInit(obj, "data.name");

            var desc_text = this.root.Q<TextField>("desc_text");
            desc_text?.XSInit(obj, "data.desc");

            var lv = this.root.Q<IntegerField>("lv");
            lv?.XSInit(obj, "data.lv");

            var hp = this.root.Q<IntegerField>("hp");
            hp?.XSInit(obj, "data.stat.hp.val");

            var str = this.root.Q<IntegerField>("str");
            str?.XSInit(obj, "data.stat.str.val");
            var mag = this.root.Q<IntegerField>("mag");
            mag?.XSInit(obj, "data.stat.mag.val");
            var res = this.root.Q<IntegerField>("res");
            res?.XSInit(obj, "data.stat.res.val");
            var spd = this.root.Q<IntegerField>("spd");
            spd?.XSInit(obj, "data.stat.spd.val");
            var lck = this.root.Q<IntegerField>("lck");
            lck?.XSInit(obj, "data.stat.lck.val");
            var def = this.root.Q<IntegerField>("def");
            def?.XSInit(obj, "data.stat.def.val");
            var dex = this.root.Q<IntegerField>("dex");
            dex?.XSInit(obj, "data.stat.dex.val");
            var cha = this.root.Q<IntegerField>("cha");
            cha?.XSInit(obj, "data.stat.cha.val");
            var mov = this.root.Q<IntegerField>("mov");
            mov?.XSInit(obj, "data.stat.mov.val");

            // var bf = new BinaryFormatter();
            // var path = "test/role.dat";

            // obj.Data.lvType = XSDefine.ClassLvType.Master;
            // var dir = Path.GetDirectoryName(path);
            // if (dir != "")
            // {
            //     Directory.CreateDirectory(dir);
            // }
            // var file = File.Create(path);
            // bf.Serialize(file, obj.Data);
            // file.Close();


            // if (File.Exists(path))
            // {
            //     var file2 = File.Open(path, FileMode.Open);
            //     var data = bf.Deserialize(file2) as RoleData;
            //     file2.Close();
            // }

            // var instance = ClassDataManager.Instance;
            // var data = new ClassData();
            // data.Name = "test";
            // instance.AddItem(data);
            // ClassDataManager.Save();
            // ClassDataManager.Load();
            // instance = ClassDataManager.Instance;
        }
    }

}