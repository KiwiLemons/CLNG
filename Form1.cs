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
{
    //My explanations are bad I'm sorry
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
                //This happens when the range box is either blank or just a single number without a '/'
                if (ex.Message.Contains("Index was outside the bounds of the array"))
                {
                    rangeHigh = (int)Math.Pow(10, (double)numericUpDown1.Value) - 1;
                }
                else
                    throw ex;
            }
            //Tell users what is wrong with thier inputs?

            //Generate the base that will be used to fill in blank spaces. A length of 4 will make a base of '0000';
            for (int i = 0; i < (numericUpDown1.Value != 0 ? numericUpDown1.Value : rangeHigh.ToString().Length); i++) { genBase += "0"; }
            progressBar1.Minimum = rangeLow;
            progressBar1.Maximum = rangeHigh;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = rangeLow; i <= rangeHigh; i++)
            {
                progressBar1.Value = i;
                try
                {
                    output += i == rangeLow ? $"{genBase.Substring(i.ToString().Length)}{i.ToString()}" : $"\r\n{new StringBuilder(genBase).Remove(0, i.ToString().Length)}{i.ToString()}";
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    //This happens when the rangeMax is larger than the largest number possible with the specified length Ex: 10000 has 5 digits and would trigger this is you specify length as 4
                    if (ex.Message.Contains("startIndex cannot be larger than length of string"))
                        break;
                    else
                        throw ex;
                }
            }
            stopwatch.Stop();
            textBox1.Text = output + $"\r\n\r\n{stopwatch.ElapsedMilliseconds}";
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
            if (Properties.Settings.Default.lengthPriority == true)
                radioButton2.Checked = true;
            else
                radioButton1.Checked = true;
        }
        //Length radio button check changed
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.lengthPriority = radioButton2.Checked;
            Properties.Settings.Default.Save();
        }
    }
}