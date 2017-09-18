
namespace WpfApplication1
{
    public class PlayerSet
    {
        public HandPoint[] HP = new HandPoint[2];

        PlayerSet()
        {
            HP[0] = new HandPoint(1);
            HP[1] = new HandPoint(2);
        }
    }
}
