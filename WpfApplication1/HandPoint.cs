using Microsoft.Kinect;
using System.Windows;

namespace WpfApplication1
{
    public class HandPoint
    {
        private short Player;

        private Point L_Point;
        private Point R_Point;

        private Point T_L_Point;
        private Point T_R_Point;

        public HandPoint(short Player_Num)
        {
            this.L_Point = new Point(0, 0);
            this.R_Point = new Point(0, 0);

            this.T_L_Point = new Point(0, 0);
            this.T_R_Point = new Point(0, 0);

            this.Player = Player_Num;
        }

        public void converse_Point(int I_Width, int I_Height, DepthImagePoint[] DP, int D_Width, int D_Height)
        {
            this.L_Point.X = (int)(I_Width * DP[0].X / D_Width)/2 + GameSet.RAD*2;
            this.L_Point.Y = (int)(I_Height * DP[0].Y / D_Height); // + 50);

            this.R_Point.X = (int)(I_Width * DP[1].X / D_Width)/2 + GameSet.RAD*2;
            this.R_Point.Y = (int)(I_Height * DP[1].Y / D_Height); // + 50);
        }

        public void Set_T_Point()
        {
            this.T_L_Point = this.L_Point;
            this.T_R_Point = this.R_Point;
        }

        public Point Get_Point(int choice)
        {
            switch (choice)
            {
                case 0: return this.L_Point;
                case 1: return this.R_Point;
                case 2: return this.T_L_Point;
                case 3: return this.T_R_Point;
                default:
                    return new Point();
            }
        }
        public short Get_Player()
        {
            return this.Player;
        }
    }
}
