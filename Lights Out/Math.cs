using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMath = System.Math;

namespace Lights_Out
{
    delegate int DistanceFunc(int x1, int x2, int x3, int x4);

    static class Math
    {
        static public int Distance_Manhattan(int x1, int y1, int x2, int y2)
        {
            return SMath.Abs(x1 - x2) + SMath.Abs(y1 - y2);
        }

        static public int Distance_Euclide(int x1, int y1, int x2, int y2)
        {
            int i1 = (int)SMath.Pow(SMath.Abs(x1 - x2), 2);
            int i2 = (int)SMath.Pow(SMath.Abs(y1 - y2), 2); 
            return (int)SMath.Sqrt(i1 + i2);
        }

        static public int Distance_King(int x1, int y1, int x2, int y2)
        {
            return SMath.Max(SMath.Abs(x1 - x2), SMath.Abs(y1 - y2));
        }
    }
}
