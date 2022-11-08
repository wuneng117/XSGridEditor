namespace XSSLG
{
    /// <summary>  </summary>
    public class TableManager
    {
        protected static readonly TableManager instance = new TableManager();
        public static TableManager Instance { get => instance; }
        public DataManager<ClassData> ClassDataManager { get; }
        public DataManager<RoleData> RoleDataManager { get; }
        public DataManager<SkillData> SkillDataManager { get; }
        public DataManager<BuffData> BuffDataManager { get; }

        public TableManager()
        {
            this.ClassDataManager = DataManager<ClassData>.Instance;
            this.RoleDataManager = DataManager<RoleData>.Instance;
            this.SkillDataManager = DataManager<SkillData>.Instance;
            this.BuffDataManager = DataManager<BuffData>.Instance;
        }
    }
}