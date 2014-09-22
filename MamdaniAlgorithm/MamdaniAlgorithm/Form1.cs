using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FuzzySets;
using LinguisticVariables;
using FuzzyConclusion;

namespace MamdaniAlgorithm
{
    public partial class Form1 : Form
    {

        private string path = string.Empty;
      
        public Form1()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 10;

            progressBar2.Minimum = 0;
            progressBar2.Maximum = 10;

            progressBar3.Minimum = 0;
            progressBar3.Maximum = 10;

            progressBar4.Minimum = 0;
            progressBar4.Maximum = 10;

            progressBar5.Minimum = 0;
            progressBar5.Maximum = 10;

            progressBar6.Minimum = 0;
            progressBar6.Maximum = 100;

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            progressBar1.Value = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            progressBar2.Value = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            progressBar3.Value = trackBar3.Value;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            progressBar4.Value = trackBar4.Value;
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            progressBar5.Value = trackBar5.Value;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (path == string.Empty)
                return;
            FuzzyConclusion.Rule[] rules = RulesInitializer.GetRules(path);
            FuzzyConclusion.MamdaniAlgorithm alg = 
                new FuzzyConclusion.MamdaniAlgorithm(rules);
            double[] data = new double[5];
            data[0] = trackBar1.Value;
            data[1] = trackBar2.Value;
            data[2] = trackBar3.Value;
            data[3] = trackBar4.Value;
            data[4] = trackBar5.Value;
            double r = alg.Process(data);
            progressBar6.Value = Convert.ToInt16(100 * r);
            label7.Text = "Вероятность критического удара: " +
                Convert.ToInt16(100 * r) + "%";
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "")
                MessageBox.Show("Файл не выбран", "Сообщение");
            path = openFileDialog1.FileName;
        }

    }
}
