using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace XSSLG
{
    [Serializable]
    public class DataManager<T> where T : BaseData
    {
        [SerializeField]
        protected List<T> itemArray = new List<T>();
        [SerializeField]
        protected Dictionary<string, int> keyIndexMap = new Dictionary<string, int>();

        protected static DataManager<T> instance;
        public static DataManager<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    Load();
                }
                return instance;
            }
        }

        public static void Load()
        {
            if (XSU.IsEditor())
            {
                var path = GameConst.DATA_FILE_PATH_EDITOR + "ClassData.bytes";
                if (File.Exists(path))
                {
                    var file = File.Open(path, FileMode.Open);
                    var bf = new BinaryFormatter();
                    instance = bf.Deserialize(file) as DataManager<T>;
                    file.Close();
                }
                else
                {
                    instance = new DataManager<T>();
                }
            }
            else
            {
                var textAsset = Resources.Load<TextAsset>(GameConst.DATA_FILE_PATH_RUNTIME + "ClassData");
                if (textAsset != null)
                {
                    var stream = new MemoryStream(textAsset.bytes);
                    var bf = new BinaryFormatter();
                    instance = bf.Deserialize(stream) as DataManager<T>;
                }
                else
                {
                    instance = new DataManager<T>();
                }
            }

            for (int i = 0; i < instance.itemArray.Count; i++)
            {
                var item = instance.itemArray[i];
                if (!instance.keyIndexMap.ContainsKey(item.Name))
                {
                    instance.keyIndexMap.Add(item.Name, i);
                }
            }
        }

        public static void Save()
        {
            if (XSU.IsEditor())
            {
                var dir = Path.GetDirectoryName(GameConst.DATA_FILE_PATH_EDITOR);
                if (dir != "")
                {
                    Directory.CreateDirectory(dir);
                }
                var path = GameConst.DATA_FILE_PATH_EDITOR + "ClassData.bytes";
                var file = File.Create(path);
                var bf = new BinaryFormatter();
                bf.Serialize(file, instance);
                file.Close();
            }
        }

        public T GetItem(string name)
        {
            if (name ==  null || name.Length == 0)
            {
                return null;
            }

            if (this.keyIndexMap.TryGetValue(name, out var index))
            {
                return this.itemArray[index];
            }
            return null;
        }

        public void AddItem(T item)
        {
            if (item.Name == null || item.Name.Length == 0 || this.keyIndexMap.ContainsKey(item.Name))
            {
                return;
            }
            this.itemArray.Add(item);
            this.keyIndexMap.Add(item.Name, this.itemArray.Count - 1);
        }
    }
}
