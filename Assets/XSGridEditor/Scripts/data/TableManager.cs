namespace XSSLG
{
    /// <summary>  </summary>
    public class TableManager
    {
        protected static readonly TableManager instance = new TableManager();
        public static TableManager Instance { get => instance; }
        public DataManager<ClassData> ClassDataManager { get; }
        // public LearnSkillDataManager LearnSkillDataManager { get; }
        // public RoleDataManagerEx RoleDataManager { get; }
        public DataManager<SkillData> SkillDataManager { get; }
        public DataManager<TriggerData> TriggerDataManager { get; }
        public DataManager<BuffData> BuffDataManager { get; }

        public TableManager()
        {
            this.ClassDataManager = DataManager<ClassData>.Instance;
        //     this.LearnSkillDataManager = LearnSkillDataManager.Instance;
        //     this.RoleDataManager = new RoleDataManagerEx();
            this.SkillDataManager = DataManager<SkillData>.Instance;
            this.TriggerDataManager = DataManager<TriggerData>.Instance;
            this.BuffDataManager = DataManager<BuffData>.Instance;
        }
    }
}