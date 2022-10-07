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
        // public SkillDataManagerEx SkillDataManager { get; }
        // public StatDataManager StatDataManager { get; }
        // public TriggerDataManager TriggerDataManager { get; }
        // public BuffDataManager BuffDataManager { get; }

        public TableManager()
        {
            this.ClassDataManager = DataManager<ClassData>.Instance;
        //     this.LearnSkillDataManager = LearnSkillDataManager.Instance;
        //     this.RoleDataManager = new RoleDataManagerEx();
        //     this.SkillDataManager = new SkillDataManagerEx();
        //     this.StatDataManager = StatDataManager.Instance;
        //     this.TriggerDataManager = TriggerDataManager.Instance;
        //     this.BuffDataManager = new BuffDataManagerEx();
        }
    }
}