using System;
using System.Threading;

namespace ElipticCurve3
{   
    class Program
    {
        static void Main(string[] args)
        {
            #region lab3
            int q = 241;
            Point G = new Point(2, 2);
            //Point result = G * q;

            int da = 121;
            Point Ha = G * da;
            Console.WriteLine($"Открытый ключ стороны а x:{Ha.x} y:{Ha.y}");

            int db = 204;
            Point Hb = G * db;
            Console.WriteLine($"Открытый ключ стороны b x:{Hb.x} y:{Hb.y}");

            Point S = Hb * da;
            Console.WriteLine($"Общий ключ а x:{S.x} y:{S.y}");

            S = Ha * db;
            Console.WriteLine($"Общий ключ b x:{S.x} y:{S.y}");
            #endregion
            Point.DrawFunction();
            Console.ReadKey();
            Console.WriteLine(ECDSA.DoCrypt("Hello world", 402299));
            Console.ReadKey();

        }
    }
}
