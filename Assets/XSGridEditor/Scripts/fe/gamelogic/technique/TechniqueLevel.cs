/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 继承LevelBase，表示角色的每个技巧的等级
/// </summary>
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace XSSLG
{
    /// <summary> 技能等级类 </summary>
    public class TechniqueLevelEx
    {
        /// <summary> index为TechniqueType，值是对应的LevelBase数据，数组长度固定 </summary>
        private List<LevelBase> TechniqueArray { get; }

        /// <summary> 根据技能类型获取技能等级 </summary>
        public int GetLv(TechniqueType type) => type >= TechniqueType.Max ? 0 : this.TechniqueArray[(int)type].Lv;
        
        /// <summary> 增加经验值并看看能否升级 </summary>
        public bool AddExpAndCheckLevel(TechniqueType type, int addExp) => type >= TechniqueType.Max ? false : this.TechniqueArray[(int)type].AddExpAndCheckLevel(addExp);
        public TechniqueLevelEx(List<TechniqueLevel> dataArray)
        {
            // 生成初值，因为this.TechniqueArray[2]没有赋值这样调用会报错。。。
            int max = (int)TechniqueType.Max - 1;
            this.TechniqueArray = new List<LevelBase>(Enumerable.Repeat(new LevelBase(Config.TECHNIQUE_EXP_ARRAY), max));
            
            // 先过滤掉类型不符合的，然后赋值到TEchniqueArray，如果类型相同取最后一个
            dataArray.FindAll(data => (int)data.Type < this.TechniqueArray.Count)
                     .ForEach( data => {
                         var lv = Mathf.Clamp(data.Lv, 0, GameConst.TECHNIQUE_LEVEL_MAX);
                         var index = (int)data.Type;
                         this.TechniqueArray[index] = new LevelBase(Config.TECHNIQUE_EXP_ARRAY, lv);
                     });
        }
    }
}