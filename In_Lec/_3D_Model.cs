using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace In_Lec
{
    class _3D_Model
    {
        public List<_3D_Point> L_3D_Pts = new List<_3D_Point>();
        public List<Edge> L_Edges = new List<Edge>();
        public Camera cam;
        public bool  draw = false;
        public bool fall;
        int ct = 0;
        public void AddPoint(_3D_Point pnn)
        {
            L_3D_Pts.Add(pnn);
        }

        public void AddEdge(int i , int j , Color cl)
        {
            Edge pnn = new Edge(i, j);
            pnn.cl = cl;
            L_Edges.Add(pnn);
        }

        public void DrawYourSelf(Graphics g,int val)
        {
            Font FF = new Font("System", 10);
            for (int k = 0; k < L_Edges.Count; k++)
            {
                int i = L_Edges[k].i;
                int j = L_Edges[k].j;

                _3D_Point pi = L_3D_Pts[i];
                _3D_Point pj = L_3D_Pts[j];

                PointF pi_2D = cam.TransformToOrigin_And_Rotate_And_Project(pi);
                PointF pj_2D = cam.TransformToOrigin_And_Rotate_And_Project(pj);
                Edge ptrv = (Edge)L_Edges[i];
                //g.DrawString(ptrv.i.ToString(), new Font("Times New Roman", 10), Brushes.Blue, pi_2D.X, pi_2D.Y);


                Pen Pn = new Pen(L_Edges[k].cl, val);

                g.DrawLine(Pn, pi_2D.X, pi_2D.Y, pj_2D.X, pj_2D.Y);
            }
        }
    }
}
