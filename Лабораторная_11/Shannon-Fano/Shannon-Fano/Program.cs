using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shannon_Fano
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;



    class A
    {

        double[] P1 = { 0.062, 0.014, 0.038, 0.013, 0.025, 0.072, 0.072, 0.007, 0.016, 0.062, 0.010, 0.028, 0.035, 0.026, 0.053, 0.090, 0.023, 0.040, 0.045, 0.053, 0.021, 0.002, 0.009, 0.004, 0.012, 0.006, 0.003, 0.014, 0.003, 0.014, 0.003, 0.006, 0.018 };
        char[] Alpha = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };

        string[] Res = new string[33];

        double schet1 = 0;
        double schet2 = 0;


        public void Sort()
        {
            for (int i = 0; i < P1.Length; i++)
            {
                for (int j = 0; j < P1.Length - i - 1; j++)
                {
                    if (P1[j] < P1[j + 1])
                    {
                        char temp2;
                        double temp1 = 0;

                        temp1 = P1[j];
                        temp2 = Alpha[j];
                        P1[j] = P1[j + 1];
                        Alpha[j] = Alpha[j + 1];
                        P1[j + 1] = temp1;
                        Alpha[j + 1] = temp2;

                    }
                }
            }


        }
        int m;

        public int Delenie_Posledovatelnosty(int L, int R)
        {

            schet1 = 0;
            for (int i = L; i <= R - 1; i++)
            {
                schet1 = schet1 + P1[i];
            }

            schet2 = P1[R];
            m = R;
            while (schet1 >= schet2)
            {
                m = m - 1;
                schet1 = schet1 - P1[m];
                schet2 = schet2 + P1[m];
            }
            return m;


        }
        int g = 0;

        public void Fano(int L, int R)
        {
            int n;

            if (L < R)
            {

                n = Delenie_Posledovatelnosty(L, R);
                Console.WriteLine(n);
                for (int i = L; i <= R; i++)
                {
                    if (i <= n)
                    {
                        Res[i] += Convert.ToByte(0);
                    }
                    else
                    {
                        Res[i] += Convert.ToByte(1);
                    }
                }



                Fano1(L, n);

                Fano(n + 1, R);

            }


        }

        public void Fano1(int L, int R)
        {
            int n;

            if (L < R)
            {

                n = Delenie_Posledovatelnosty(L, R);
                Console.WriteLine(n);
                for (int i = L; i <= R; i++)
                {
                    if (i <= n)
                    {
                        Res[i] += Convert.ToByte(0);
                    }
                    else
                    {
                        Res[i] += Convert.ToByte(1);
                    }
                }

                Fano(L, n);

                Fano1(n + 1, R);

            }


        }
        public static void Main()
        {
            A ob = new A();


            ob.Sort();

            ob.Fano(0, 32);
            for (int i = 0; i < 33; i++)
            {


                Console.WriteLine(ob.Alpha[i] + " " + ob.Res[i]);

            }

        }

    }
}