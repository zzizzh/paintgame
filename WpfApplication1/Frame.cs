using System.Windows;

namespace WpfApplication1
{

    public class Frame
    {
        public short[] TF = new short[GameSet.TF_SIZE];

        public Frame() { }
        
       ///*
        public void SetFrame(PaintPoint paint_point, int Width, int Height)
        {
            setHandFrame(paint_point.L_PaintingPoint, paint_point.LengthX1, Width, 1);
            setHandFrame(paint_point.R_PaintingPoint, paint_point.LengthX2, Width, 2);
        }
       // */
        /*
         * 
        public void SetFrame(PaintPoint paint_point, int Width, int Height)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (paint_point.LengthX1 < 20)
                        for (int k = 0; k < paint_point.LengthX1; k++)
                        {
                            if (((i - paint_point.L_PaintingPoint[k].X) * (i - paint_point.L_PaintingPoint[k].X) +
                                (j - paint_point.L_PaintingPoint[k].Y) * (j - paint_point.L_PaintingPoint[k].Y) < GameSet.RAD * GameSet.RAD))
                            {
                                TF[(i + j * Width)] = paint_point.HP.Get_Player();
                            } // if
                        }
                    
                    if (paint_point.LengthX2 < 20)
                        for (int k = 0; k < paint_point.LengthX2; k++)
                        {
                            if (((i - paint_point.R_PaintingPoint[k].X) * (i - paint_point.R_PaintingPoint[k].X) +
                                (j - paint_point.R_PaintingPoint[k].Y) * (j - paint_point.R_PaintingPoint[k].Y) < GameSet.RAD * GameSet.RAD))
                            {
                                TF[(i + j * Width)] = 2;    //paint_point.HP.Get_Player();
                            } // if
                        } // for
                     
                } // for
            } // for
        } // end function
         */

        void setHandFrame(Point[] paint_point, int length, int Width, short player)
        {
            Point RangePoint = new Point();

            if (length < GameSet.PaintLineLength)
            {
                for (int k = 0; k < length; k++)
                {
                    if (paint_point[k].X - GameSet.RAD < 0)
                        RangePoint.X = 1;
                    else if (paint_point[k].X - GameSet.RAD > 639)
                        RangePoint.X = 638;
                    else
                        RangePoint.X = paint_point[k].X - GameSet.RAD;

                    if (k == 0)
                        for (int m = 0; m < 2 * GameSet.RAD; m++)
                        {
                            if(paint_point[k].Y - GameSet.RAD < 0)
                                RangePoint.Y = 1;
                            else if(paint_point[k].Y - GameSet.RAD > 479)
                                RangePoint.Y = 478;
                            else
                                RangePoint.Y = paint_point[k].Y - GameSet.RAD;

                            for (int n = 0; n < 2 * GameSet.RAD; n++)
                            {

                                if ((RangePoint.X - paint_point[k].X) * (RangePoint.X - paint_point[k].X) +
                                    (RangePoint.Y - paint_point[k].Y) * (RangePoint.Y - paint_point[k].Y) 
                                    < GameSet.RAD * GameSet.RAD)
                                    
                                    // (x-a)^2 + (y-b)^2 < r^2
   
                                { TF[(int)(RangePoint.X + (int)(RangePoint.Y) * Width)] = player; }

                                RangePoint.Y++;

                                if (RangePoint.Y + 2 > 480)
                                    break;
                            }
                            RangePoint.X++;

                            if (RangePoint.X + 2 > Width)
                                break;
                        }   // if
                    else
                        for (int m = GameSet.RAD / 2; m < 2 * GameSet.RAD; m++)
                        {
                            if (paint_point[k].Y - GameSet.RAD < 0)
                                RangePoint.Y = 1;
                            else if (paint_point[k].Y - GameSet.RAD > 479)
                                RangePoint.Y = 478;
                            else
                                RangePoint.Y = paint_point[k].Y - GameSet.RAD;


                            for (int n = GameSet.RAD / 2; n < 2 * GameSet.RAD; n++)
                            {

                                if ((RangePoint.X - paint_point[k].X) * (RangePoint.X - paint_point[k].X) +
                                    (RangePoint.Y - paint_point[k].Y) * (RangePoint.Y - paint_point[k].Y) < GameSet.RAD * GameSet.RAD)
                                { TF[(int)(RangePoint.X + (int)(RangePoint.Y) * Width)] = player; }

                                RangePoint.Y++;

                                if (RangePoint.Y + 2 > 480)
                                    break;
                            }
                            RangePoint.X++;

                            if (RangePoint.X + 2 > Width)
                                break;
                        }
                }   // for
            }
        }
    }
}
