using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XSSLG
{
    /// <summary>  </summary>
    public class EnumGenerator
    {
        static public void AddEnumValue(Type type, string addEnum)
        { 
            // var action = (Dict<string, int> nameValueDict) =>
            // {
            //     var maxValue = nameValueDict.Max((key, value) => value);
            //     nameValueDict.Add(addEnum, maxValue + 1);
            // };
            // var str = GetEnumString(type, action);
        }

        static public void ReduceEnumValue(Type type, string reduceEnum)
        { 
            // var fieldList = GetFieldList(type);
            // var action = (Dict<string, int> nameValueDict) => nameValueDict.RemoveAt(reduceEnum);
            // var str = GetEnumString(type, action);
        }

        // static public Dict<string, int> GetNameValueDict(Type type)
        // { 
        //     var fieldList = new List<FieldInfo>(type.GetFields(BindingFlags.Static | BindingFlags.Public));
        //     var ret = filedList.ToDict(x => x.Name, x => (int)x.GetValue(null));
        //     return ret;
        // }

        // static private string GetEnumString(Type type, Action<Dict<string, int>> action)
        // { 
        //     string str = "public enum ";
        //     str = str + type.Name + " {";

        //     var fieldList = GetNameValueDict(type);
        //     action(filedList);

        //     fieldList.ForEach((key, value) => str = str + key + " = " + value + ",\n");
        //     str = str + "}";
        //     return str;
        // }
    }
}