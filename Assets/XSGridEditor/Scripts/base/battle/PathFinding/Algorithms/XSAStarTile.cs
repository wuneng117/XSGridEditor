namespace XSSLG
{
    public class XSAStarTile
    {
        public XSAStarTile(int cost, XSTile prevTile) => (this.cost, this.prevTile) = (cost, prevTile);

        protected int cost;
        public int Cost { get => cost; }

        protected XSTile prevTile;
        public XSTile PrevTile { get => prevTile; }

    }
}