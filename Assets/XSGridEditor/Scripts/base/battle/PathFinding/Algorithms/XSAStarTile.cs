namespace XSSLG
{
    public class XSAStarTile
    {
        public XSAStarTile(int cost, int f, XSTile prevTile) => (this.cost, this.f, this.prevTile) = (cost, f, prevTile);

        protected int cost;
        public int Cost { get => cost; }
        /// <summary> f = cost + Manhattan distance </summary>
        protected int f;
        public int F { get => f; }

        protected XSTile prevTile;
        public XSTile PrevTile { get => prevTile; }

    }
}