
using System;
using System.Collections.Generic;

namespace XSSLG
{
    public partial class LearnSkillData
    {
        /// <summary>ID</summary>
        [System.ComponentModel.DisplayName("ID")]
        public long Id { get; private set; }

        /// <summary>名字</summary>
        [System.ComponentModel.DisplayName("名字")]
        public string Name { get; private set; }

        /// <summary>需要角色等级</summary>
        [System.ComponentModel.DisplayName("需要角色等级")]
        public int NeedLv { get; private set; }

        /// <summary>需要技巧等级</summary>
        [System.ComponentModel.DisplayName("需要技巧等级")]
        public global::XSSLG.TechniqueLevel TechnieuqLv { get; private set; }

        /// <summary>学会技能ID</summary>
        [System.ComponentModel.DisplayName("学会技能ID")]
        public long SkillID { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

    public partial class LearnSkillDataArray
    {
        public List<long> Keys { get; private set; }

        public List<global::XSSLG.LearnSkillData> Items { get; private set; }

        public string TableHash { get; private set; }


        public static Int32 PID { get { return 0;}}
    }

}
