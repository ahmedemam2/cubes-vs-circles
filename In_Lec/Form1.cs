using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace In_Lec
{
    public partial class Form1 : Form
    {
        Bitmap off;


        _3D_Model Cube = new _3D_Model();
        _3D_Point v1, v2;
        List<_3D_Model> Plane = new List<_3D_Model>();
        List<fall> fallList = new List<fall>();
        List<Circle> CircleList = new List<Circle>();
        Camera cam = new Camera();
        Random rr = new Random();
        Color[] cl = { Color.Red, Color.Green, Color.Black, Color.Blue };
        Timer T = new Timer();
        bool moving = true, moveleft = false, moveright = false, moveforward = true, movebackward = false, gameover = false;
        int pos = 4, caminc = 4;
        int ctmove = 0;
        class fall
        {
            public int check;
        }
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.Load += new EventHandler(Form1_Load);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            T.Tick += new EventHandler(timer_Tick);
            T.Start();
        }
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameover == false && !moving&&ctmove%6==0)
            {

                switch (e.KeyCode)
                {

                    case Keys.Up:
                        moveforward = true;
                        moving = true;
                        break;


                    case Keys.Down:
                        if (pos > 9)
                        {
                            moveforward = false;
                            movebackward = true;
                            moving = true;
                        }
                        break;
                    case Keys.Right:
                        if (Cube.L_3D_Pts[0].X <= 55)
                        {
                            moveforward = false;
                            moveright = true;
                            moving = true;
                        }

                        break;

                    case Keys.Left:
                        if (Cube.L_3D_Pts[0].X >= -215)
                        {
                            moveforward = false;
                            moveleft = true;
                            moving = true;
                        }

                        break;
                    case Keys.W:
                        cam.cop.Y += 20;
                        break;
                }
            }
            DrawDubble(this.CreateGraphics());
        }
        void move(_3D_Point v1, _3D_Point v2, _3D_Model Cube, int val)
        {
            Transformation.RotateArbitrary(Cube.L_3D_Pts, v1, v2, val);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < fallList.Count; i++)
            {
                if (pos == fallList[i].check)
                {
                    Transformation.TranslateZ(Cube.L_3D_Pts, 10);
                    gameover = true;
                    moveforward = false;
                    break;
                }
            }
            if (moveforward == true)
            {
                v1 = new _3D_Point(Plane[pos].L_3D_Pts[2]);
                v2 = new _3D_Point(Plane[pos].L_3D_Pts[3]);
                cam.cop.Y += 5;

                //Transformation.RotateArbitrary(Cube.L_3D_Pts, v1, v2, -10);
                move(v1, v2, Cube, -15);
                ctmove++;
                if (ctmove == 6)
                {
                    ctmove = 0;
                    pos += 10;
                    //moveforward = false;
                    moving = false;
                }
            }
            if (moveleft == true)
            {
                v1 = new _3D_Point(Plane[pos].L_3D_Pts[3]);
                v2 = new _3D_Point(Plane[pos].L_3D_Pts[0]);

                //Transformation.RotateArbitrary(Cube.L_3D_Pts, v1, v2, -10);
                move(v1, v2, Cube, -10);
                ctmove++;
                if (ctmove == 9)
                {
                    ctmove = 0;
                    pos -= 1;
                    moveleft = false;
                    moving = false;
                    moveforward = true;
                }
            }
            if (moveright == true)
            {
                v1 = new _3D_Point(Plane[pos].L_3D_Pts[1]);
                v2 = new _3D_Point(Plane[pos].L_3D_Pts[2]);

                //Transformation.RotateArbitrary(Cube.L_3D_Pts, v1, v2, -10);
                move(v1, v2, Cube, -10);
                ctmove++;
                if (ctmove == 9)
                {
                    ctmove = 0;
                    pos += 1;
                    moveright = false;
                    moving = false;
                    moveforward = true;
                }
            }
            if (movebackward == true)
            {
                v1 = new _3D_Point(Plane[pos].L_3D_Pts[0]);
                v2 = new _3D_Point(Plane[pos].L_3D_Pts[1]);
                cam.cop.Y -= 2;

                move(v1, v2, Cube, -10);
                ctmove++;
                if (ctmove == 9)
                {
                    ctmove = 0;
                    pos -= 10;
                    movebackward = false;
                    moving = false;
                    moveforward = true;
                }

            }
            DrawDubble(this.CreateGraphics());
        }

        void CreateCube(_3D_Model M, float XS, float YS, float ZS, Color vvv)
        {
            float[] vert =
                            {
                                    -100,100,-100,
                                    100,100,-100,
                                    100,-100,-100,
                                    -100,-100,-100,
                                    -100,100,100,
                                    100,100,100,
                                    100,-100,100,
                                    -100,-100,100,

                            };


            _3D_Point pnn;
            int j = 0;
            for (int i = 0; i < 8; i++)
            {
                pnn = new _3D_Point(vert[j] + XS, vert[j + 1] + YS, vert[j + 2] + ZS);
                j += 3;
                M.AddPoint(pnn);
            }


            int[] Edges = {
                                0,1,
                                1,2,
                                2,3,
                                3,0,
                                4,5,
                                5,6,
                                6,7,
                                7,4,
                                0,4,
                                3,7,
                                2,6,
                                1,5
                          };
            j = 0;
            for (int i = 0; i < 12; i++)
            {
                M.AddEdge(Edges[j], Edges[j + 1], vvv);

                j += 2;
            }
        }
        void CreatePlane(_3D_Model pln, float XS, float YS, float ZS)
        {
            float[] vert =
                            {
                                0  ,  0  ,0,
                                100   , 0   ,0,
                                100   , 100   ,0,
                                0  ,  100 , 0
                            };


            _3D_Point pnn;
            int j = 0;
            for (int i = 0; i < 4; i++)
            {
                pnn = new _3D_Point(vert[j] + XS, vert[j + 1] + YS, vert[j + 2] + ZS);
                j += 3;
                pln.AddPoint(pnn);
            }


            int[] Edges = {
                              0,1 ,
                              1,2 ,
                              2,3 ,
                              3,0
                          };
            j = 0;
            Color[] cl = { Color.Red, Color.Yellow, Color.Black, Color.Blue };
            for (int i = 0; i < 4; i++)
            {
                pln.AddEdge(Edges[j], Edges[j + 1], Color.Red);

                j += 2;
            }
        }
        void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);


            int cx = 400;
            int cy = 400;
            cam.ceneterX = (this.ClientSize.Width / 2);
            cam.ceneterY = (this.ClientSize.Height / 2);
            cam.cxScreen = cx;
            int x = -700, y = -600, z = 100;
            cam.cyScreen = cy;
            cam.cop.Z -= 100;
            cam.BuildNewSystem();
            for (int i = 1; i < 2001; i++)
            {
                _3D_Model pnn = new _3D_Model();
                pnn.fall = false;
                CreatePlane(pnn, x, y, z);
                pnn.cam = cam;
                Transformation.Scale(pnn.L_3D_Pts, 0.38f, 0.38f, 0.2f);
                x += 100;
                Plane.Add(pnn);

                if (i % 10 == 0)
                {
                    x = -700;
                    y += 100;
                }
            }
            CreateCube(Cube, -480, -1050, 100, Color.Goldenrod);
            Transformation.Scale(Cube.L_3D_Pts, 0.2f, 0.2f, 0.2f);
            Cube.cam = cam;
            int value;
            int Circleno = rr.Next(300);
            for (int i = 0; i < Circleno; i++)
            {
                value = rr.Next(2000);
                fall pnnfall = new fall();
                pnnfall.check = value;
                fallList.Add(pnnfall);
                Circle pnn = new Circle();
                pnn.Design();
                pnn.cam = cam;
                CircleList.Add(pnn);

                Plane[value].fall = true;
                //CircleList[i].L_3D_Pts.X = Plane[value].L_3D_Pts[0].X;
                //CircleList[i].L_3D_Pts.Y = Plane[value].L_3D_Pts[0].Y;

                Transformation.TranslateY(CircleList[i].L_3D_Pts, Plane[value].L_3D_Pts[0].Y + 18);
                Transformation.TranslateX(CircleList[i].L_3D_Pts, Plane[value].L_3D_Pts[0].X + 18);
                Transformation.TranslateZ(CircleList[i].L_3D_Pts,18);

            }
            Transformation.TranslateZ(Cube.L_3D_Pts, -20);
            cam.cop.Z -= 100;
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubble(e.Graphics);
        }

        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);
            Pen Pn = new Pen(Color.Green, 2);
            Cube.DrawYourSelf(g,3);
            for (int i = 0; i < Plane.Count; i++)
            {
                Plane[i].DrawYourSelf(g,2);

            }
            for (int i = 0; i < CircleList.Count; i++)
            {
                CircleList[i].DrawYourSelf(g,3);
            }


        }

        void DrawDubble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
