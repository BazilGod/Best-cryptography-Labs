using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sha256_salting
{
    public class Program
    {
        static void Main(string[] args)
        {
            string hashed = "Password12345";
            string salt = CreateSalt(15);
            string hash = GenerateSHA256(hashed, salt);
            Console.WriteLine(hashed+ "=");
            Console.WriteLine("Соль: "+salt + "\n Хэш:" + hash);
            Console.WriteLine("{0:x2}");
            Console.ReadKey();
        }

        public static string CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateSHA256(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return ToHex(hash);
        }

        public static string ToHex(byte[] ba)
        {

            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach(byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
    }
}
