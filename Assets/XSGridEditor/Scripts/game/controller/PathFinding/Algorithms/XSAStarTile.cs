namespace XSSLG
{
    public class XSAStarTile
    {
        public XSAStarTile(int cost, XSTile prevTile) => (this.Cost, this.PrevTile) = (cost, prevTile);

        public int Cost = 0;
        public XSTile PrevTile = null;
    }
}