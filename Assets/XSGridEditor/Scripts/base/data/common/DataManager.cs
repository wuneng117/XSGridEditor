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
        public List<T> GetList() => new List<T>(this.itemArray);
        
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
                var path = GameConst.DATA_FILE_PATH_EDITOR + typeof(T) + ".json";
                if (File.Exists(path))
                {
                    var file = File.OpenText(path);
                    // var bf = new BinaryFormatter();
                    // instance = bf.Deserialize(file) as DataManager<T>;
                    instance = JsonUtility.FromJson<DataManager<T>>(file.ReadToEnd());
                    file.Close();
                }
                else
                {
                    instance = new DataManager<T>();
                }
            }
            else
            {
                var path = GameConst.DATA_FILE_PATH_EDITOR + typeof(T) + ".json";
                var textAsset = Resources.Load<TextAsset>(GameConst.DATA_FILE_PATH_RUNTIME + typeof(T));
                if (textAsset != null)
                {
                    // var stream = new MemoryStream(textAsset.bytes);
                    // var bf = new BinaryFormatter();
                    // instance = bf.Deserialize(stream) as DataManager<T>;
                    instance = JsonUtility.FromJson<DataManager<T>>(textAsset.text);
                }
                else
                {
                    instance = new DataManager<T>();
                }
            }

            for (int i = 0; i < instance.itemArray.Count; i++)
            {
                var item = instance.itemArray[i];
                if (!instance.keyIndexMap.ContainsKey(item.Key))
                {
                    instance.keyIndexMap.Add(item.Key, i);
                }
            }
        }

        public void Save()
        {
            if (XSU.IsEditor())
            {
                var dir = Path.GetDirectoryName(GameConst.DATA_FILE_PATH_EDITOR);
                if (dir != "")
                {
                    Directory.CreateDirectory(dir);
                }
                var path = GameConst.DATA_FILE_PATH_EDITOR + typeof(T) + ".json";
                var file = File.CreateText(path);
                var data = JsonUtility.ToJson(Instance);
                file.Write(data);
                // var bf = new BinaryFormatter();
                // bf.Serialize(file, instance);
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
            if (item.Key == null || item.Key.Length == 0 || this.keyIndexMap.ContainsKey(item.Key))
            {
                return;
            }
            this.itemArray.Add(item);
            this.keyIndexMap.Add(item.Key, this.itemArray.Count - 1);
        }
    }
}
