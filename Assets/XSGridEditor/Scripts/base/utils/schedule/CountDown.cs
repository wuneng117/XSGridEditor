namespace XSSLG
{
    /// <summary>
    /// 倒计时，就是技能里常用的cd啦
    /// </summary>
    public class CountDown
    {
        public bool Active { get; protected set; } = false;
        protected int CountTurn { get; set; } = 0;
        protected int EndTurn { get; set; } = 0;
        public bool Finish { get; private set; } = false;

        public void Start(int endTurn)
        {
            this.EndTurn = endTurn;
            this.Active = true;
            this.Finish = false;
        }

        public void Stop()
        {
            this.Active = false;
            this.Finish = false;
        }

        public void Update(int turn)
        {
            if (this.Active == false)
                return;

            this.CountTurn += turn;

            if (this.CountTurn < this.EndTurn)
                return;

            this.Active = false;
            this.Finish = true;
        }
    }
}