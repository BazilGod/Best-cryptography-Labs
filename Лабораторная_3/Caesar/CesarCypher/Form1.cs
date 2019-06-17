using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CesarCypher
{
    public partial class Form1 : Form
    {
        int index;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            char[] alpha = "abcdefghijklmnopqrstuvwxyz1234567890abcdefghijklmnopqrstuvwxyz".ToCharArray();
            string al = new string(alpha);

            Cipher(al);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            char[] revalpha = "zyxwvutsrqponmlkjihgfedcba0987654321zyxwvutsrqponmlkjihgfedcba".ToCharArray();
            string reval = new string(revalpha);

            Cipher(reval);  
        }

        private void Cipher(string al)
        {
            textBox2.Clear();

            char[] charArray = textBox1.Text.ToCharArray();
            string ch = new string(charArray);

            index = int.Parse(numericUpDown1.Text);

            for (int i = 0; i < ch.Length; i++)
            {

                if (al.Contains(charArray[i].ToString()))
                {
                    for (int j = 0; j < al.Length; j++)
                    {
                        if (ch[i] == al[j])
                        {
                            textBox2.Text += ((al[j + index]).ToString());
                            break;
                        }
                    }
                }
                else
                {
                    textBox2.Text += (charArray[i].ToString());
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
