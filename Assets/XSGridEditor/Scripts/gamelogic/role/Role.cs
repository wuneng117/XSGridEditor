/// <summary>
/// @Author: zhoutao
/// @Date: 2021/5/4
/// @Description: 角色类
/// </summary>
using System.Collections.Generic;
using System.Linq;

namespace XSSLG
{
    /// <summary> 角色信息 </summary>
    public class Role : DataDecorate<RoleData>
    {
        /// <summary> 玩家等级 </summary>
        public RoleLevel Level { get; private set; }

        /// <summary> 玩家当前职业 </summary>
        public RoleClass Class { get; private set; }

        /// <summary> 玩家所有职业等级 </summary>
        public List<RoleClass> ClassArray { get; private set; }

        /// <summary> 移动类型 </summary>
        public RoleDataMovType MoveType { get; private set; } = RoleDataMovType.Ground;

        /// <summary> 拥有战技 </summary>
        public List<SkillData> CombatArtArray { get; } = new List<SkillData>();

        /// <summary> 拥有特技 </summary>
        public List<SkillData> AbilityArray { get; } = new List<SkillData>();

        /// <summary> 拥有魔法 </summary>
        public List<SkillData> LearnMagicArray { get; } = new List<SkillData>();

        /// <summary> 拥有纹章 </summary>
        public List<SkillData> CrestArray { get; } = new List<SkillData>();

        /// <summary> 技巧管理 </summary>
        public TechniqueLevelEx Technique { get; private set; }

        /// <summary> 玩家属性 </summary>
        public Stat Stat { get; set; }

        /// <summary> 从data初始化 </summary>
        public override bool Init(RoleData data)
        {
            var ret = base.Init(data);
            if (!ret)
                return ret;
                
            this.Level = new RoleLevel(data.Lv);

            this.Class = new RoleClass(data.ClassData);
            this.ClassArray = data.ClassDataArray.Select(classData => new RoleClass(classData)).ToList<RoleClass>();

            this.MoveType = data.MoveType;

            this.CombatArtArray.AddRange(data.CombatArtArray);
            this.AbilityArray.AddRange(data.AbilityArray);
            this.LearnMagicArray.AddRange(data.LearnMagicArray);
            this.CrestArray.AddRange(data.CrestArray);

            this.Technique = new TechniqueLevelEx(data.TechniqueLvArray ?? new List<TechniqueLevel>());

            this.Stat = new Stat(data.Stat);
            return true;
        }
    }
}