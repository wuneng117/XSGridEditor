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

            this.Class = new RoleClass(TableManager.Instance.ClassDataManager.GetItem(data.ClassDataName));
            this.ClassArray = data.ClassDataNameArray.Select(name => new RoleClass(TableManager.Instance.ClassDataManager.GetItem(name))).ToList<RoleClass>();


            this.CombatArtArray.AddRange(data.CombatArtNameArray.Select(name => TableManager.Instance.SkillDataManager.GetItem(name)));
            this.AbilityArray.AddRange(data.AbilityNameArray.Select(name => TableManager.Instance.SkillDataManager.GetItem(name)));
            this.LearnMagicArray.AddRange(data.MagicNameArray.Select(name => TableManager.Instance.SkillDataManager.GetItem(name)));
            this.CrestArray.AddRange(data.CrestNameArray.Select(name => TableManager.Instance.SkillDataManager.GetItem(name)));

            // this.Technique = new TechniqueLevelEx(data.TechniqueLvArray ?? new List<TechniqueLevel>());

            this.Stat = new Stat(data.Stat);
            return true;
        }
    }
}