
namespace WpfApplication1
{
    public class ColorList
    {
        public delegate void ColorSet(byte[] IB, int i, int j, int Width);

        public ColorSet[] Color_Set;

        public ColorList()
        {
            Color_Set = new ColorSet[5];

            Color_Set[0] = new ColorSet(Set_Red);
            Color_Set[1] = new ColorSet(Set_Blue);
            Color_Set[2] = new ColorSet(Set_Green);
            Color_Set[3] = new ColorSet(Set_BlueSky);
            Color_Set[4] = new ColorSet(Set_Yellow);
        }

        void Set_Red(byte[] IB, int i, int j, int Width)
        {
       //   IB[(i + Width * j) * 4] = color1;       // B
       //   IB[(i + Width * j) * 4 + 1] = color1;   // G
            IB[(i + Width * j) * 4 + 2] = 255;   // R
        }
        void Set_Blue(byte[] IB, int i, int j, int Width)
        {
            IB[(i + Width * j) * 4] = 255;      // B
            //   IB[(i + Width * j) * 4 + 1] = color1;   // G
            //  IB[(i + Width * j) * 4 + 2] = 255;   // R
        }
        void Set_Green(byte[] IB, int i, int j, int Width)
        {
            //   IB[(i + Width * j) * 4] = color1;       // B
            IB[(i + Width * j) * 4 + 1] = 255;   // G
            //     IB[(i + Width * j) * 4 + 2] = 255;   // R
        }
        void Set_BlueSky(byte[] IB, int i, int j, int Width)
        {
            IB[(i + Width * j) * 4] = 255;       // B
            IB[(i + Width * j) * 4 + 1] = 255;   // G
            //     IB[(i + Width * j) * 4 + 2] = 255;   // R
        }
        void Set_Yellow(byte[] IB, int i, int j, int Width)
        {
            //IB[(i + Width * j) * 4] = color1;       // B
            IB[(i + Width * j) * 4 + 1] = 255;   // G
            IB[(i + Width * j) * 4 + 2] = 255;   // R
        }
    }
}
