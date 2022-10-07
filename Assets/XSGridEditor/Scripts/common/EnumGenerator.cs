using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XSSLG
{
    /// <summary>  </summary>
    public class EnumGenerator
    {
        /// <summary> do not change this string`s format </summary>
        static private readonly string PREFIX = 
@"/// <summary>
/// this file is created by code generator, do not modify it
/// </summary>
namespace XSSLG
{{
    public partial class XSDefine
    {{
        /// <summary> weapon type </summary>
        {0}
        
        /// <summary> other type </summary>
    }}
}}";
        static public void AddEnumValue(Type type, string addEnum)
        { 
            var nameValueDict = GetNameValueDict(type);
            if (nameValueDict.ContainsKey(addEnum))
            {
                return;
            }

            var maxValue = nameValueDict.Max(pair => pair.Value);
            nameValueDict.Add(addEnum, maxValue + 1);

            var str = GetEnumString(type, nameValueDict);
            XSResLoadMgr.SaveFileToPath(str, XSGridDefine.XSENUM_FILE_PATH);
        }

        static public void ReduceEnumValue(Type type, string reduceEnum)
        { 
            var nameValueDict = GetNameValueDict(type);
            if (!nameValueDict.ContainsKey(reduceEnum))
            {
                return;
            }

            nameValueDict.Remove(reduceEnum);
            var str = GetEnumString(type, nameValueDict);
            XSResLoadMgr.SaveFileToPath(str, XSGridDefine.XSENUM_FILE_PATH);
        }

        static public Dictionary<string, int> GetNameValueDict(Type type)
        { 
            var fieldList = new List<FieldInfo>(type.GetFields(BindingFlags.Static | BindingFlags.Public));
            var ret = fieldList.ToDictionary(x => x.Name, x => (int)x.GetValue(null));
            return ret;
        }

        static private string GetEnumString(Type type, Dictionary<string, int> nameValueDict)
        { 
            string str = @"
        public enum " + type.Name + @"
        {";
            nameValueDict.ToList().ForEach(pair => str = str + @"
            " + pair.Key + " = " + pair.Value + ",");
            str = str + @"
        }";
            return string.Format(PREFIX, str);
        }
    }
}