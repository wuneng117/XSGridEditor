/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 角色的职业类，由职业data和职业当前经验值组成
/// </summary>
using UnityEngine;

namespace XSSLG
{
    /// <summary> 角色职业 </summary>
    public class RoleClass
    {
        /// <summary> 数据data </summary>
        public ClassData Data { get; }

        /// <summary> 当前经验 </summary>
        public int Exp { get; private set; }

        public RoleClass(ClassData data, int exp = 0)
        {
            this.Data = data ?? new ClassData();
            this.Exp = exp;
        }
        
        /// <summary> 精通需要的经验值 </summary>
        private int GetMaxExp()
        {
            if (!Config.CLASS_EXP_ARRAY.ContainsKey(this.Data.LvType))
                return 0;
            
            return Config.CLASS_EXP_ARRAY[this.Data.LvType];
        } 

        /// <summary> 是否精通 </summary>
        public bool CheckPreficient() => this.GetMaxExp() >= this.Exp;


        /// <summary> 增加经验值并看看能否精通</summary>
        public bool AddExpAndCheckPreficient(int addExp)
        {
            this.Exp = Mathf.Clamp(this.Exp + addExp, 0, this.GetMaxExp());
            return this.CheckPreficient();
        }
    }
}