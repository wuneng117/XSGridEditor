namespace XSSLG
{
    public class AStarTile
    {
        public AStarTile(int cost, XSTile prevTile) => (this.Cost, this.PrevTile) = (cost, prevTile);

        public int Cost = 0;
        public XSTile PrevTile = null;
    }
}