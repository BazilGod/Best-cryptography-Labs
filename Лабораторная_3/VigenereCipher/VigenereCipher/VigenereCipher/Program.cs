using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] table = new char[27, 27];
            FillTable(table);
            bool ok = true;
            while (ok)
            {
                Console.WriteLine("---------- VIGENERE CIPHER ----------");
                Console.WriteLine("Press (1) to encrypt a message");
                Console.WriteLine("Press (2) to decrypt a message");
                Console.WriteLine("Press (3) to exit the application");
                Console.WriteLine("-------------------------------------");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.WriteLine("Type your message:");
                        string messageE = Console.ReadLine().ToUpper();
                        Console.WriteLine("Type your key:");
                        string keyE = Console.ReadLine().ToUpper();
                        keyE = BuildKey(messageE.Length, keyE);
                        string enc = Encrypt(messageE, keyE, table);
                        Console.WriteLine("Your encrypted message is: " + enc);
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine("Type your message:");
                        string messageD = Console.ReadLine().ToUpper();
                        Console.WriteLine("Type your key:");
                        string keyD = Console.ReadLine().ToUpper();
                        keyD = BuildKey(messageD.Length, keyD);
                        string dec = Decrypt(messageD, keyD, table);
                        Console.WriteLine("Your decrypted message is: " + dec);
                        Console.WriteLine();
                        break;
                    case "3":
                        ok = false;
                        break;
                }

            }
        }

        static void FillTable(char[,] table)
        {
            for (int i = 1; i <= 26; i++)
                table[i, 1] = table[1, i] = Convert.ToChar('A' + i - 1);

            for (int i = 2; i <= 26; i++)
                for (int j = 2; j <= 26; j++)
                    if (table[i - 1, j].Equals('Z'))
                        table[i, j] = 'A';
                    else
                        table[i, j] = Convert.ToChar(table[i - 1, j] + 1);
        }

        static string Encrypt(string message, string key, char[,] table)
        {
            string cipher = null;

            int[] messagePos = CalculatePosition(message);
            int[] keyPos = CalculatePosition(key);

            for (int i = 0; i < keyPos.Length; i++)
            {
                cipher += table[keyPos[i], messagePos[i]];
            }
            return cipher;
        }

        static string Decrypt(string message, string key, char[,] table)
        {
            string cipher = null;

            char[] messageChar = message.ToCharArray();
            int[] keyPos = CalculatePosition(key);

            for (int i = 0; i < keyPos.Length; i++)
            {

                for (int j = 1; j <= 26; j++)
                {
                    if (table[keyPos[i], j].Equals(messageChar[i]))
                    {
                        cipher += table[1, j];
                        break;
                    }
                }
            }
            return cipher;
        }

        static string BuildKey(int msgLenght, string key)
        {
            while (key.Length < msgLenght)
            {
                if (key.Length < msgLenght - key.Length)
                    key += key;
                else key += key.Substring(0, msgLenght - key.Length);
            }

            return key;
        }

        static int[] CalculatePosition(string str)
        {
            char[] strChar = str.ToCharArray();
            int[] pos = new int[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                pos[i] = strChar[i] - 'A' ;
                Console.WriteLine(pos[i]);
            }

            return pos;
        }
    }
}
