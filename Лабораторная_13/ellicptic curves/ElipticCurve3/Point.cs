using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElipticCurve3
{
    class Point
    {
        static int p = 211;
        public static int a = 0;
        public static int b = -4;
        public long x;
        public long y;

        public Point(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        static public void DrawFunction()
        {
            bool flag = true;
            for (int i = 4; i < 10; i++)
            {
                int y = (int)Math.Sqrt((Math.Pow(i, 3) + (a * i) + b) % p);
                Console.SetCursorPosition(Console.CursorTop + y/1000, Console.CursorLeft + i);
                if (flag)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("*");
                flag = !flag;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static long GetLambda(Point p1, Point p2)
        {
            long lambda;

            if (p1 == p2)
            {
                long a, b;
                long g = gcd(2 * p1.y, Point.p, out a, out b);

                lambda = ((3 * p1.x * p1.x + Point.a) * a);
            }
            else
            {
                long a, b;
                long dx = p2.x - p1.x;
                dx = (dx % Point.p + Point.p) % Point.p;
                long dy = p2.y - p1.y;
                dy = (dy % Point.p + Point.p) % Point.p;
                long g = gcd(dx, Point.p, out a, out b);

                lambda = (dy * a);
            }
            lambda = (lambda % Point.p + Point.p) % Point.p;

            return lambda;
        }

        static long gcd(long a, long b, out long x, out long y)
        {
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            long x1, y1;
            long d = gcd(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }

        public static Point operator +(Point p1, Point p2)
        {
            long x;
            long y;
            long lambda = Point.GetLambda(p1, p2);

            if (p1.x == p2.x && p2.y == -(p1.y))
            {
                return new Point(0, 0);
            }

            x = lambda * lambda - p1.x - p2.x;
            x = (x % Point.p + Point.p) % Point.p;
            y = lambda * (p1.x - x) - p1.y;
            y = (y % Point.p + Point.p) % Point.p;

            return new Point(x, y);
        }

        public static Point operator *(Point p1, int multiplier)
        {
            Point tmp = p1;

            for (int i = 1; i < multiplier; ++i)
            {
                tmp += p1;
            }

            return new Point(tmp.x, tmp.y);
        }

        public static Point operator -(Point a)
        {
            return a * (Point.p - 1);
        }

        public static bool operator ==(Point p1, Point p2)
        {
            return p1.x == p2.x && p1.y == p2.y;
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return p1.x != p2.x && p1.y != p2.y;
        }

        public static int FindOrder(Point G)
        {
            Point tmp;

            for (int i = 1; i < 70000; i++)
            {
                tmp = G;
                tmp *= i;

                if (tmp == new Point(0, 0))
                    return i;
            }

            return -1;
        }
    }
}
