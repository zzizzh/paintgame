using Microsoft.Kinect;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        //public PlayerSet playerset = new PlayerSet();
        HandPoint HP = new HandPoint(1);

        Frame PaintingPosition = new Frame();
        ColorList color = new ColorList();

        double score1, score2;  // 색칠 점수
        Boolean end = false;    // 게임 종료 : true
        int time = 100;
     
        public MainWindow()
        {
            InitializeComponent();

            InitializeNui();

        }

        KinectSensor nui = null;

        void InitializeNui()
        {
            Timer tm = new Timer();
            tm.Interval = 1000;
            tm.Enabled = true;

            tm.Tick += new EventHandler(tm_Tick);

            nui = KinectSensor.KinectSensors[0];    // 키넥트 센서

            nui.ColorStream.Enable();          // 색상
            nui.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(nui_ColorFrameReady);

            nui.DepthStream.Enable();          // 깊이 
            nui.SkeletonStream.Enable();       // 졸라맨

            nui.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(nui_AllFramesReady);
            // 모든 프레임이 준비되었을 때 이벤트 발생

            nui.Start();    // 키넥트 작동
        }

        void nui_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            SkeletonFrame sf = e.OpenSkeletonFrame();

            if (sf == null) return;

            Skeleton[] SD = new Skeleton[sf.SkeletonArrayLength];

            //textBlock6.Text = SD.Length.ToString();
            sf.CopySkeletonDataTo(SD);
            using (DepthImageFrame depthImageFrame = e.OpenDepthImageFrame())
            {
                if (depthImageFrame != null)
                {
                    foreach (Skeleton sd in SD)
                    //for (int i = 0; i < 2; i++)
                    {
                        //    System.Diagnostics.Debug.WriteLine("상태에 상관없이, Skeleton의 Joint개수 : {0}", sd.Joints.Count); // 20

                        if (sd.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            /* (!) */
                            Joint[] joints = new Joint[2];
                            DepthImagePoint[] depthPoint = new DepthImagePoint[2];

                            joints[0] = sd.Joints[JointType.HandLeft];
                            joints[1] = sd.Joints[JointType.HandRight];

                            for (int j = 0; j < 2; j++)
                            {
                                CoordinateMapper coordMapper = new CoordinateMapper(nui);

                                //System.Diagnostics.Debug.WriteLine("joint.Position >> X : {0}, Y : {1}, Z : {2}", 
                                //                                                          joint.Position.X, joint.Position.Y, joint.Position.Z);
                                // 왼손 오른손 좌표 잡기

                                depthPoint[j] = coordMapper.MapSkeletonPointToDepthPoint
                                    (joints[j].Position, depthImageFrame.Format);
                            }
                            HP.converse_Point((int)image1.Width, (int)image1.Height, depthPoint, (int)depthImageFrame.Width, (int)depthImageFrame.Height);
                        }// if
                    }// foreach
                }// if
            }// using

            sf.Dispose();
        }

        void nui_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            ColorImageFrame IP = e.OpenColorImageFrame();

            if (IP == null) return;

            byte[] IB = new byte[IP.PixelDataLength];

            textBlock6.Text = IP.PixelDataLength.ToString();

            IP.CopyPixelDataTo(IB);
            BitmapSource src = null;

            Print_Circle(IB, IP.Width, IP.Height);
      //      textBlock4.Text = IP.Width.ToString();
      //      textBlock5.Text = IP.Height.ToString();

            src = BitmapSource.Create(IP.Width, IP.Height, 96, 96, PixelFormats.Bgr32, null, IB,
                IP.Width * IP.BytesPerPixel);

            image1.Source = src;

            WriteableBitmap wb = new WriteableBitmap(IP.Width, IP.Height, 96, 96, PixelFormats.Bgr32, null);

            IP.Dispose();
        }

        ///////////////////////////////////////////////////////////////////////////

        /* PrintCircle : 좌표를 설정하고 그 좌표에 색칠을 함. 그리고 색칠된 픽셀 수로 점수 계산
            * 좌표를 받아서 그에 대응하는 픽셀에 색칠 여부를 확인.
            *  TF의 값이 true 일 때 색상 유지.
            *  좌표를 중심으로 원 반경으로 색칠됨.
         */

        void Print_Circle(byte[] IB, int Width, int Height)
        {
            score1 = 1;
            score2 = 1;

            Set_Color(Width, Height);        // 색칠할 좌표 지정

            Paint_Color(IB, Width, Height, GameSet.Player1);  // 색칠하기
            Paint_Color(IB, Width, Height, GameSet.Player2);

            Score(score1, score2);  // 점수 계산
        }


        /*  SetColer : 색칠할 좌표 설정
         *  복잡하니깐 알아서 봐
         *  생각대로 잘 안됬는데 되긴 됨
        */

        void Set_Color(int Width, int Height)
        {
            PaintPoint paint_point1 = new PaintPoint(HP);
            //    PaintPoint paint_point2 = new PaintPoint(P2);
            
            ///////////// 색칠할 좌표 설정 //////////////////

            if (!end) // 게임이 끝나지 않았을 때
            {
                PaintingPosition.SetFrame(paint_point1, Width, Height);
                //     PaintingPosition.SetFrame(paint_point2, Width, Height);
            }
            HP.Set_T_Point();
            //   P2.Set_T_Point();
        }

        /*  PaintColor : 좌표에 색칠
         *  바이트는 4개로 쪼개져서 RGB로 되있음
         *  나머지 하나는 빈값인듯
         */

        void Paint_Color(byte[] IB, int Width, int Height, short Player)
        {
            int PlayerColor=0;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (PaintingPosition.TF[(i + Width * j)] == Player)    //  (i * 4) + (IP.Width * j) 
                    {
                        if (Player == 1)
                        {
                            score1++;
                            PlayerColor = GameSet.Player1_Color;
                        }
                        else if (Player == 2)
                        {
                            score2++;
                            PlayerColor = GameSet.Player2_Color;
                        }
                       color.Color_Set[PlayerColor](IB, i, j, Width);
                    }
                }
            }
        }

        void SetColor(int i, int j, int Width, int Color)
        { 
        
        }
        /* Score : 점수계산
        * 색칠 된 픽셀 수로 결정 됨
        * 계산된 점수를 TextBox에 출력
        */

        void Score(double score1, double score2)
        {
            double SmallX1 = 0, BigX1 = 0;

            SmallX1 = (score1 / (score1 + score2)) * 100;
            score1 = SmallX1;

            BigX1 = (100 - SmallX1);
            score2 = BigX1;

            textBlock1.Text = score1.ToString("N0");
            textBlock2.Text = score2.ToString("N0");

            // System.Diagnostics.Debug.WriteLine("score1 : {0} / score2 : {1} / SmallX1 : {2} / BigX1 : {3}", score1, score2, SmallX1, BigX1);

        }

        /*  tm_Tick : 타이머 함수
         *  1초마다 게임 시간이 줄어듬(초단위)
         *  게임 시간이 0이 되었을 때 게임종료 (end = true)
         */

        void tm_Tick(object sender, EventArgs e)
        {
            if (end == false)
                this.textBlock5.Text = (--time).ToString();
            //     color1-=10;   
            //     if (color1 == 0)
            //         color1 = 255;
            if (time == 0)
                end = true;
        }
    }
}
