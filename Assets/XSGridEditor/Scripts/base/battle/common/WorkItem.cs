namespace XSSLG
{
    public abstract class WorkItem<T, T1> : DataDecorate<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public WorkItem(T data)
        {
            this.Init(data);
        }

        public abstract void OnTurnStart(T1 data);
        public abstract void StartWork();
        public abstract void StopWork();
    }
}