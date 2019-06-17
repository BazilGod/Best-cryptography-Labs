using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "84320";
            var start = "11100111";
            Console.WriteLine("input = {0}\r\nstart = {1}", input, start);
            var max_v = 0;
            foreach (var c in input)
            {
                var e = (int)char.GetNumericValue(c);
                if (e > max_v)
                    max_v = e;
            }
            if (start == "")
                for (int i = 0; i < max_v; i++)
                    start += input.Contains(Convert.ToString(i + 1)) ? "1" : "0";
            var test = start;
            var counter = 0;
            Console.WriteLine(string.Format("\t{0,10} {1,10} {2,10} {3,10}", "step", "bin", "ОС", "OUTPUT"));
            while (true)
            {
                var f = (int)char.GetNumericValue(test.First());
                var l = (int)char.GetNumericValue(test.Last());
                var h = (int)char.GetNumericValue(test[(int)char.GetNumericValue(input[0]) - 1]);
                for (int i = 1; i < input.Length && input[i] != '0'; i++)
                    h ^= (int)char.GetNumericValue(test[(int)char.GetNumericValue(input[i]) - 1]);
                Console.WriteLine(string.Format("#\t{0,10} {1,10} {2,10} {3,10}", counter++, test, h, l));
                if (test.Equals(start) && counter > 1)
                    break;
                test = h + test.Substring(0, test.Length - 1);
            }
            Console.WriteLine(string.Format("max_count = {0}", Math.Pow(2, max_v) - 1));
        }
    }
}
