using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;


    class MainClass
    {
        private static string inputPath = "in.txt";
        private static string keyPath = "key.txt";

        public static void Main(string[] args)
        {
            var input = GetFromFile(inputPath);
            var key = GetFromFile(keyPath);

            var encoded = Encode(input, key);
            var decoded = Decode(encoded, key);

            Console.WriteLine("encoded: {0}", encoded);
            Console.WriteLine("decoded: {0}", decoded);

            Console.ReadLine();
        }

        private static string Encode(string input, string key)
        {
            var toEncryptArray = UTF8Encoding.UTF8.GetBytes(input);

            var hashmd5 = new MD5CryptoServiceProvider();

            var keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            var cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        private static string Decode(string input, string key)
        {
            var toEncryptArray = Convert.FromBase64String(input);

            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;

            tdes.Mode = CipherMode.ECB;

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        private static string GetFromFile(string path)
        {
            var fileContent = File.ReadAllText(path);
            return fileContent;
        }

    }
