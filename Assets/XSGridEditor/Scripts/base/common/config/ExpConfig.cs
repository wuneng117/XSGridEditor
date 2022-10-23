/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 所有经验值数值的定义
/// </summary>
using System.Collections.Generic;

namespace XSSLG
{
    public partial class Config
    {

        /// <summary> 经验值 </summary>
        /// <summary> 精通职业需要的经验值 </summary>
        public static readonly Dictionary<ClassLvType, int> CLASS_EXP_ARRAY = new Dictionary<ClassLvType, int> {
            {ClassLvType.Unique, 20},
            {ClassLvType.Beginner, 50},
            {ClassLvType.Intermediate, 100},
            {ClassLvType.Advanced, 150},
            {ClassLvType.Master, 250},
        };

        /// <summary> technique提升需要的经验值 </summary>
        public static readonly int[] TECHNIQUE_EXP_ARRAY = {
            40,
            100,
            180,
            300,
            460,
            580,
            860,
            1220,
            1660,
            2420,
            3500,
            0
        };    // 技巧等级描述
    }
}