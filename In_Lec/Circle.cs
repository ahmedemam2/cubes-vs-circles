using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace In_Lec
{
    class Circle : _3D_Model
    {
        public void Design()
        {
            float xx, yy, ZZ = 0;
            int i = 0;
            float inc = 10, Rad = 17;
            int j = 0;
            for (float th = 0; th < 360; th += inc)
            {
                xx = (float)(Math.Cos(th * Math.PI / 180) * Rad);
                yy = (float)(Math.Sin(th * Math.PI / 180) * Rad);

                L_3D_Pts.Add(new _3D_Point(xx, yy, ZZ));

                if (j > 0)
                {
                    AddEdge(i, i - 1, Color.Green);
                }
                i++;
                j++;
            }
            AddEdge(i - 1, i - j, Color.Green);

        }
    }
}
