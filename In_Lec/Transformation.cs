using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace In_Lec
{
    class Transformation
    {
        public static void Scale(List<_3D_Point> L_Pts, float sx, float sy, float sz)
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                L_Pts[i].X *= sx;
                L_Pts[i].Y *= sy;
                L_Pts[i].Z *= sz;

            }
        }
        public static void RotatX(List<_3D_Point> L_Pts, float theta)//Rotate a list of 3D point using angle theta on the X axis
        {

            float th = (float)(Math.PI * theta / 180);//Simila to Graphics1 the correct theta is calculated using the given theta
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3D_Point p = L_Pts[i];

                float x_ = p.X;//X will remain the same
                float y_ = (float)(p.Y * Math.Cos(th) - p.Z * Math.Sin(th));//Y changes based on its mathematical equation
                float z_ = (float)(p.Y * Math.Sin(th) + p.Z * Math.Cos(th));//Z changes based on its mathematical equation

                //The new values are placed in the 3D list
                p.X = x_;
                p.Y = y_;
                p.Z = z_;
            }
        }

        public static void RotatY(List<_3D_Point> L_Pts, float theta)
        {

            float th = (float)(Math.PI * theta / 180);
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3D_Point p = L_Pts[i];


                float x_ = (float)(p.Z * Math.Sin(th) + p.X * Math.Cos(th));
                float y_ = p.Y;
                float z_ = (float)(p.Z * Math.Cos(th) - p.X * Math.Sin(th));                

                p.X = x_;
                p.Y = y_;
                p.Z = z_;
            }
        }

        public static void RotatZ(List<_3D_Point> L_Pts, float theta)
        {

            float th = (float)(Math.PI * theta / 180);
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3D_Point p = L_Pts[i];


                float x_ = (float)(p.X * Math.Cos(th) - p.Y * Math.Sin(th));
                float y_ = (float)(p.X * Math.Sin(th) + p.Y * Math.Cos(th));
                float z_ = p.Z;

                p.X = x_;
                p.Y = y_;
                p.Z = z_;
            }
        }

        public static void TranslateX(List<_3D_Point> L_Pts, float tx)//The X values of the given list are translted (moved), usually to start its plane from x,y,z of 0,0,0
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3D_Point p = L_Pts[i];
                p.X += tx;
            }
        }

        public static void TranslateY(List<_3D_Point> L_Pts, float ty)
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3D_Point p = L_Pts[i];
                p.Y += ty;
            }
        }

        public static void TranslateZ(List<_3D_Point> L_Pts, float tz)
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3D_Point p = L_Pts[i];
                p.Z += tz;
            }
        }
        public static void Translate(List<_3D_Point> L_Pts, float xr, float yr, float zr)//Used to translate all 
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                L_Pts[i].X += xr;
                L_Pts[i].Y += yr;
                L_Pts[i].X += zr;

            }
        }


        public static void RotateArbitrary(List<_3D_Point> L_Pts,
                                            _3D_Point v1,
                                            _3D_Point v2,
                                            float ang)//The list of 3D points will rotate around 2 specific 3D point v1 and v2 using a given angle
        {
            Transformation.TranslateX(L_Pts, v1.X * -1);//The X values of the 3D list is translated to have the first 3D point start at 0,0,0
            Transformation.TranslateY(L_Pts, v1.Y * -1);//The Y values of the 3D list is translated to have the first 3D point start at 0,0,0
            Transformation.TranslateZ(L_Pts, v1.Z * -1);//The Z values of the 3D list is translated to have the first 3D point start at 0,0,0

            float dx = v2.X - v1.X;//The difference between the X values of the second 3D point and the translated X of the first 3D point
            float dy = v2.Y - v1.Y;//The difference between the Y values of the second 3D point and the translated X of the first 3D point
            float dz = v2.Z - v1.Z;//The difference between the Z values of the second 3D point and the translated X of the first 3D point

            float theta = (float)Math.Atan2(dy, dx);//Theta is calculated based on the proof in the project files
            float phi = (float)Math.Atan2(Math.Sqrt(dx * dx + dy * dy), dz);//phi is calculated based on the proof in the project files

            theta = (float)(theta * 180 / Math.PI);//Theta is then converted to radian value as per graphics 1
            phi = (float)(phi * 180 / Math.PI);//Phi is also converted to radian value

            //To rotate the given 3d list around V1 and V2, the following code lines are used based on the proof in the project file
            Transformation.RotatZ(L_Pts, theta * -1);//Start inverse rotation around Z axis using theta
            Transformation.RotatY(L_Pts, phi * -1);//Start inverse rotation around Y axis using phi

            Transformation.RotatZ(L_Pts, ang);//Start rotation around Z axis using ang

            Transformation.RotatY(L_Pts, phi * 1);//Start rotation around Y axis using phi
            Transformation.RotatZ(L_Pts, theta * 1);//Start rotation around Z axis using theta


            Transformation.TranslateZ(L_Pts, v1.Z * 1);//Returns the Z values of v1 to original place
            Transformation.TranslateY(L_Pts, v1.Y * 1);//Returns the Y values of v1 to original place
            Transformation.TranslateX(L_Pts, v1.X * 1);//Returns the X values of v1 to original place
        }


    }
}
