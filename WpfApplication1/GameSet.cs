using System;
enum pointname { L_Point = 0, R_Point, T_L_Point, T_R_Point };
enum ColorValue {RED = 0, BLUE, GREEN, BLUESKY, YELLOW}

namespace WpfApplication1
{
    public static class GameSet
    {
        public const short Player1 = 1;
        public const short Player2 = 2;

        public const int PaintLineLength = 100;
        public const int Player1_Color = (int)ColorValue.RED;
        public const int Player2_Color = (int)ColorValue.BLUE;
        public const int RAD = 20;
        public const int TIME = 100;
        
        public const Boolean END = false;
        public const Boolean NON_END = true;
        public const int TF_SIZE = 1228800 / 4;
        public const short LEFT = 0;
        public const short RIGHT = 1;

    }
}
