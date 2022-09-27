using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XSSLG
{
    /// <summary>  </summary>
    public class EnumGenerator
    {
        static public List<FieldInfo> GetEnumList(Type type)
        { 
            var fieldList = new List<FieldInfo>(type.GetFields(BindingFlags.Static | BindingFlags.Public));
            return fieldList;
        }

        static public void AddEnumValue(Type type, string addEnum)
        { 
            string str = "public enum ";
            str = str + type.Name + " {";
            var fieldList = GetEnumList(type);

            var maxValue = fieldList.Max(x => (int)x.GetValue(null));
            // fieldList.Add(new FieldInfo() { Name = addEnum, Value = maxValue + 1 });
            
            fieldList.ForEach(fi => 
            {
                var value = fi.GetValue(null);
                str = str + fi.Name + " = " + value + ",\n";
            });
            str = str + "}";
        }

        static public void ReduceEnumValue(Type type, string reduceEnum)
        { 
            string str = "public enum ";
            str = str + type.Name + " {";
            var fieldList = GetEnumList(type);

            // fieldList = fieldList.Where(x => x.Name != reduceEnum);

            fieldList.ForEach(fi => 
            {
                var value = fi.GetValue(null);
                str = str + fi.Name + " = " + value + ",\n";
            });
            str = str + "}";
        }
    }
}