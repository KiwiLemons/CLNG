using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Constant_Length_Number_Generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string genBase = "", output = "";
            string[] ranges = textBox2.Text.Split('/');
            int rangeLow = ranges[0] == "" ? 0 : int.Parse(ranges[0]);
            int rangeHigh = 0;
            try
            {
                rangeHigh = ranges[1] == "" ? (int)Math.Pow(10, (double)numericUpDown1.Value) - 1 : int.Parse(ranges[1]);
            }
            catch (IndexOutOfRangeException ex)
            {
                if (ex.Message.Contains("Index was outside the bounds of the array"))
                {
                    rangeHigh = (int)Math.Pow(10, (double)numericUpDown1.Value) - 1;
                }
                else
                    throw ex;
            }
            //need to add check if we have enough information to generate numbers.

            for (int i = 0; i < numericUpDown1.Value; i++) { genBase += "0"; }
            for (int i = rangeLow; i <= rangeHigh; i++)
            {
                output += $"{genBase.Substring(i.ToString().Length)}{i.ToString()}\r\n";
            }
            textBox1.Text = output;
        }
    }
}