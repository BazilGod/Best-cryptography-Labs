using System;
using System.Numerics;

namespace ElipticCurve3
{
    public class ECDSA
    {
        public static string DoCrypt(string message, BigInteger d)
        {
            Random random = new Random();
            BigInteger a = message.GetHashCode();
            BigInteger e;
            BigInteger n = 62771017;
            e = a % n;

            if (e == 0)
                e = 1;

            BigInteger v = BigInteger.ModPow(e, n - 2, 2);
            BigInteger k;
            Point G = new Point(2, 2);
            Point C;
            BigInteger r;
            BigInteger s;
            //a_inverse = BigInteger.ModPow(a, n - 2, n)

            do
            {
                do
                {
                    k = random.Next(0, (int)n);
                } while ((k < 0) || (k > n));
                C = G * (int)k;
                r = C.x % n;
                s = ((r * d) + (k * e)) % n;
            } while ((r == 0) || (s == 0));

            string Rvector = r.ToString("x");
            string Svector = s.ToString("x");
            return Rvector + Svector;
        }
    }
}
