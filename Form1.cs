using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Constant_Length_Number_Generator
{  public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string genBase = "";
            string[] ranges = textBox2.Text.Split('/');
            int rangeLow = ranges[0] == "" ? 0 : int.Parse(ranges[0]);
            int rangeHigh = (int)Math.Pow(10, (double)numericUpDown1.Value) - 1;
            try
            {
                if (int.Parse(ranges[1]) < (int)Math.Pow(10, (double)numericUpDown1.Value) - 1)
                    rangeHigh = int.Parse(ranges[1]);
            }
            catch (IndexOutOfRangeException ex)
            {
                //This happens when the range box is either blank or just a single number without a '/'
                if (ex.Message.Contains("Index was outside the bounds of the array")) ;
                else
                    throw ex;
            }
            //Tell users what is wrong with thier inputs?

            //Generate the base that will be used to fill in blank spaces. A length of 4 will make a base of '0000';
            for (int i = 0; i < rangeHigh.ToString().Length; i++) { genBase += "0"; }
            progressBar1.Minimum = rangeLow;
            progressBar1.Maximum = rangeHigh;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string[] outputArray = new string[rangeHigh - rangeLow + 1];
            int index = 0;
            for (int i = rangeLow; i <= rangeHigh; i++)
            {
                //TODO: make update interval dynamic
                if (i % 10 == 0) progressBar1.Value = i;
                try
                {
                    string iString = i.ToString();
                    outputArray[index] = $"{genBase.Substring(iString.Length)}{iString.ToString()}";
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    //This happens when the rangeMax is larger than the largest number possible with the specified length
                    if (ex.Message.Contains("startIndex cannot be larger than length of string"))
                        break;
                    else
                        throw ex;
                }
                index += 1;
            }
            textBox1.Text = string.Join("\r\n",outputArray);
            progressBar1.Value = rangeHigh;
            stopwatch.Stop();
            label4.Text = $"{stopwatch.ElapsedMilliseconds}ms";
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only allow one '/'
            if (e.KeyChar == '/' && textBox2.Text.Contains('/'))
            {
                e.Handled = true;
            }
            //Only allow numbers and control characters
            else if (e.KeyChar != '/' && !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = "";
        }
    }
}