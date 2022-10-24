using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Linq;
using System.Collections.Generic;

namespace XSSLG
{

    public class XSUnitEditorView : VisualElement
    {
        protected ListView listview;

        protected XSUnitNode SelectUnit { get; set; }

        protected XSListView<SkillData> AbilityListView { get; set; }

        public XSUnitEditorView(GroupType groupType)
        {
            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/common/XSUnitEditorView.uxml");
            visualTree.CloneTree(this);

            this.InitSkillListView();

            this.listview = this.Q<ListView>("unitlist");
            this.listview.XSInit(new List<XSUnitNode>(),
                "Assets/XSGridEditor/Scripts/Editor/UIBuilder/uxml/common/XSListViewItem.uxml",
                this.BindListItem,
                this.OnSelectionItem
            );
            this.listview.onItemsChosen += this.OnChosenItem;

            this.RefreshView(groupType);

        }

        public void RefreshView(GroupType groupType)
        {
            this.ResetValue();

            var sceneObjects = XSUE.UnitMgrEditMode?.Dict.Select(pair => (XSUnitNode)pair.Value).ToList();
            if (sceneObjects == null)
            {
                listview.itemsSource = new List<XSUnitNode>();

            }
            else
            {
                sceneObjects = sceneObjects.Where(obj => obj.Group == groupType).ToList();
                listview.itemsSource = sceneObjects;
                if (sceneObjects.Count > 0)
                {
                    listview.selectedIndex = 0;
                }
            }
        }

        protected virtual void InitSkillListView()
        {
            var listRoot = this.Q<VisualElement>("skill_list_root");
            if (listRoot == null)
            {
                return;
            }

            this.AbilityListView = this.CreateSkillListView("特技列表", SkillType.Ability, () => this.SelectUnit.Data.AbilityKeyList);
            listRoot.Add(this.AbilityListView);

            this.CombatArtListView = this.CreateSkillListView("战技列表", SkillType.CombatArt, () => this.SelectUnit.Data.CombatArtKeyList);
            listRoot.Add(this.CombatArtListView);

            this.MagicListView = this.CreateSkillListView("魔法列表", SkillType.Magic, () => this.SelectUnit.Data.MagicKeyList);
            listRoot.Add(this.MagicListView);
        }

        protected virtual void CreateSkillListView(string title, SkillType type, Func<List<SkillData>> GetWatchListFunc)
        {
            var ret = new XSListView<SkillData>(new List<SkillData>(),
                title,
                () =>
                {
                    if (this.SelectUnit != null)
                    {
                        var skillList = TableManager.Instance.SkillDataManager.GetList().Where(skill => skill.Type == type).ToList();
                        XSListWindow.ShowExample(skillList, title,
                        skill =>
                        {
                            if (skill != null)
                            {
                                Undo.RecordObject(this.SelectUnit, "Add Skill");
                                EditorUtility.SetDirty(this.SelectUnit);
                                var unitSkillList = GetWatchListFunc?.Invoke()?? new List<SkillData>();
                                unitSkillList.Add(skill.Key);
                                ret.RefreshView(unitSkillList.Select(key => TableManager.Instance.SkillDataManager.GetItem(key)).ToList());
                            }
                        });
                    }
                },
                skill =>
                {
                    if (skill != null && this.SelectUnit != null)
                    {
                        var unitSkillList = GetWatchListFunc?.Invoke()?? new List<SkillData>();
                        if (unitSkillList.Contains(skill.Key))
                        {
                            Undo.RecordObject(this.SelectUnit, "Remove Skill");
                            EditorUtility.SetDirty(this.SelectUnit);
                            unitSkillList.Remove(skill.Key);
                            ret.RefreshView(unitSkillList.Select(key => TableManager.Instance.SkillDataManager.GetItem(key)).ToList());
                        }
                    }
                }
            );

            return ret;
        }


        protected virtual void OnChosenItem(IEnumerable<object> obj)
        {
            foreach (var item in obj)
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

            node.Q<Label>("label")?.XSInit(obj, "data.name");
        }

        protected virtual void OnSelectionItem(XSUnitNode obj)
        {
            Selection.activeGameObject = obj.gameObject;
            this.SelectUnit = obj;
            var texture_field = this.Q<ObjectField>("fg");
            texture_field.objectType = typeof(Texture2D);
            texture_field?.XSInit(obj, "data.texture");
            texture_field?.RegisterValueChangedCallback((evt) => this.RefreshIcon(evt.newValue as Texture2D));

            var name_text = this.Q<TextField>("name_text");
            name_text?.XSInit(obj, "data.name");

            var desc_text = this.Q<TextField>("desc_text");
            desc_text?.XSInit(obj, "data.Desc");

            var lv = this.Q<IntegerField>("lv");
            lv?.XSInit(obj, "data.Lv");

            var hp = this.Q<IntegerField>("hp");
            hp?.XSInit(obj, "data.Stat.HP.Val");

            var str = this.Q<IntegerField>("str");
            str?.XSInit(obj, "data.Stat.Str.Val");
            var mag = this.Q<IntegerField>("mag");
            mag?.XSInit(obj, "data.Stat.Mag.Val");
            var res = this.Q<IntegerField>("res");
            res?.XSInit(obj, "data.Stat.Res.Val");
            var spd = this.Q<IntegerField>("spd");
            spd?.XSInit(obj, "data.Stat.Spd.Val");
            var lck = this.Q<IntegerField>("lck");
            lck?.XSInit(obj, "data.Stat.Lck.Val");
            var def = this.Q<IntegerField>("def");
            def?.XSInit(obj, "data.Stat.Def.Val");
            var dex = this.Q<IntegerField>("dex");
            dex?.XSInit(obj, "data.Stat.Dex.Val");
            var cha = this.Q<IntegerField>("cha");
            cha?.XSInit(obj, "data.Stat.Cha.Val");
            var mov = this.Q<IntegerField>("mov");
            mov?.XSInit(obj, "data.Stat.Mov.Val");
            
            obj.CheckSkillKeyList(obj.Data.AbilityKeyList);
            this.AbilityListView?.RefreshView(obj.Data.AbilityKeyList.Select(key => TableManager.Instance.SkillDataManager.GetItem(key)).ToList());      

            obj.CheckSkillKeyList(obj.Data.CombatArtKeyList);
            this.CombatArtListView?.RefreshView(obj.Data.CombatArtKeyList.Select(key => TableManager.Instance.SkillDataManager.GetItem(key)).ToList());   

            obj.CheckSkillKeyList(obj.Data.MagicArtKeyList);
            this.MagicListView?.RefreshView(obj.Data.MagicArtKeyList.Select(key => TableManager.Instance.SkillDataManager.GetItem(key)).ToList());

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

        protected virtual void RefreshIcon(Texture2D texture)
        {
            var icon = this.Q<VisualElement>("icon");
            if (texture != null)
            {
                icon.style.backgroundImage = texture;
            }
            else
            {
                icon.style.backgroundImage = null;
            }
        }

        protected virtual void ResetValue()
        {
            this.RefreshIcon(null);
            var name_text = this.Q<TextField>("name_text");
            name_text?.Unbind();
            var desc_text = this.Q<TextField>("desc_text");
            desc_text?.Unbind();
            var lv = this.Q<IntegerField>("lv");
            lv?.Unbind();
            var hp = this.Q<IntegerField>("hp");
            hp?.Unbind();
            var str = this.Q<IntegerField>("str");
            str?.Unbind();
            var mag = this.Q<IntegerField>("mag");
            mag?.Unbind();
            var res = this.Q<IntegerField>("res");
            res?.Unbind();
            var spd = this.Q<IntegerField>("spd");
            spd?.Unbind();
            var lck = this.Q<IntegerField>("lck");
            lck?.Unbind();
            var def = this.Q<IntegerField>("def");
            def?.Unbind();
            var dex = this.Q<IntegerField>("dex");
            dex?.Unbind();
            var cha = this.Q<IntegerField>("cha");
            cha?.Unbind();
            var mov = this.Q<IntegerField>("mov");
            mov?.Unbind();

            this.AbilityListView?.RefreshView(new List<SkillData>());
            this.CombatArtListView?.RefreshView(new List<SkillData>());
            this.MagicListView?.RefreshView(new List<SkillData>());
        }
    }

}