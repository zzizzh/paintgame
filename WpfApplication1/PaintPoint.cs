using System.Windows;

namespace WpfApplication1
{
    public class PaintPoint
    {
        private double a1, a2, b1, b2;
        private int SmallX1, BigX1, SmallX2, BigX2;
        public int LengthX1, LengthX2;

        public HandPoint HP;

        public Point[] L_PaintingPoint;
        public Point[] R_PaintingPoint;

        Point LP, TLP, RP, TRP;

        public PaintPoint(HandPoint hp)
        {
            HP = hp;

            LP = HP.Get_Point((int)pointname.L_Point);
            TLP = HP.Get_Point((int)pointname.T_L_Point);
            RP = HP.Get_Point((int)pointname.R_Point);
            TRP = HP.Get_Point((int)pointname.T_R_Point);

            a1 = (LP.Y - TLP.Y) / (LP.X - TLP.X);
            a2 = (RP.Y - TRP.Y) / (RP.X - TRP.X);

            b1 = (LP.Y - (LP.X * a1));
            b2 = (RP.Y - (RP.X * a2));

            Set_Big_small();

            Set_Painting_Point();
        }

        private void Set_Big_small()
        {
            if (LP.X > TLP.X)
            {
                BigX1 = (int)LP.X;
                SmallX1 = (int)TLP.X;
            }
            else
            {
                BigX1 = (int)TLP.X;
                SmallX1 = (int)LP.X;
            }
            if (RP.X > TRP.X)
            {
                BigX2 = (int)RP.X;
                SmallX2 = (int)TRP.X;
            }
            else
            {
                BigX2 = (int)TRP.X;
                SmallX2 = (int)RP.X;
            }

            LengthX1 = BigX1 - SmallX1;
            LengthX2 = BigX2 - SmallX2;
        }

        public void Set_Painting_Point()
        {
            this.L_PaintingPoint = new Point[LengthX1];
            this.R_PaintingPoint = new Point[LengthX2];

            for (int i = 0; i < LengthX1; i++)
            {
                this.L_PaintingPoint[i] = new Point
                    (SmallX1, a1 * SmallX1 + b1);
                SmallX1 ++;
            }
            for (int i = 0; i < LengthX2; i++)
            {
                R_PaintingPoint[i] = new Point
                    (SmallX2, a2 * SmallX2 + b2);
                SmallX2 ++;
            }
        }
        /*
        public int Get_Int(int choice)
        {
            switch (choice)
            {
                case 0: return (int)a1;
                case 1: return (int)a2;
                case 2: return (int)b1;
                case 3: return (int)b2;
                case 4: return SmallX1;
                case 5: return SmallX2;
                case 6: return BigX1;
                case 7: return BigX2;
                case 8: return LengthX1;
                case 9: return LengthX2;
                default: return -1;
            }
        }
        */
        public Point[] Get_Point(int hand)
        {
            if (hand == GameSet.LEFT)
                return L_PaintingPoint;
            else if (hand == GameSet.RIGHT)
                return R_PaintingPoint;
            else
                return new Point[1];
        }
    }
}
