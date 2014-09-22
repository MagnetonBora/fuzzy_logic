using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.IO;
using Utils;
using FuzzySets;

namespace FuzzySets
{
    public partial class Form1 : Form
    {

        #region
        
        private class Params
        {
            private double A = 0;
            private double B = 0;

            public double a
            {
                get { return A; }
                set { A = value; }
            }

            public double b
            {
                get { return B; }
                set { B = value; }
            }

            public Params() { }
            public Params(double A, double B)
            {
                this.A = A;
                this.B = B;
            }
        }

        private void ViewSet(ContinuousFuzzySet<double, double> set, string name, 
            Chart chart)
        {
            chart.Series.Clear();
            chart.ResetAutoValues();
            chart.Series.Add(name);
            chart.Series[name].ChartType = SeriesChartType.Spline;
            chart.Series[name].BorderWidth = 3;
            IEnumerator<KeyValuePair<double, double>> it = set.GetEnumerator();
            while (it.MoveNext())
            {
                KeyValuePair<double, double> pair = it.Current;
                chart.Series[name].Points.AddXY(pair.Key, pair.Value);
            }
        }      

        private void ViewSet(ContinuousFuzzySet<double, double> set, Chart chart)
        {
            chart.Series.Clear();
            chart.ResetAutoValues();
            chart.Series.Add("");
            chart.Series[0].ChartType = SeriesChartType.Spline;
            chart.Series[0].BorderWidth = 3;
            IEnumerator<KeyValuePair<double, double>> it = set.GetEnumerator();
            while (it.MoveNext())
            {
                KeyValuePair<double, double> pair = it.Current;
                chart.Series[0].Points.AddXY(pair.Key, pair.Value);
            }
        }
        
        #endregion
        
        public Form1()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double left = Double.Parse(textBox1.Text);
                double right = Double.Parse(textBox2.Text);
                double step = Double.Parse(textBox3.Text);
                double a = Double.Parse(textBox4.Text);
                double b = Double.Parse(textBox5.Text);
                ContinuousFuzzySet<double, double> set = null;
                switch (comboBox1.SelectedIndex)
                {
                    case 0: FuzzySetCreator<double, double>.MemberShipFunction3 SFunction =
                        new FuzzySetCreator<double, double>.MemberShipFunction3(FuzzyHelper.SFunction);
                        set = FuzzySetCreator<double, double>.CreateInstance(SFunction, left, right, step, a, b);
                        ElementsCache.Add(set);
                        break;
                    case 1: FuzzySetCreator<double, double>.MemberShipFunction3 ZFunction =
                        new FuzzySetCreator<double, double>.MemberShipFunction3(FuzzyHelper.ZFunction);
                        set = FuzzySetCreator<double, double>.CreateInstance(ZFunction, left, right, step, a, b);
                        ElementsCache.Add(set);
                        break;
                    case 2: FuzzySetCreator<double, double>.MemberShipFunction4 PFunction =
                        new FuzzySetCreator<double, double>.MemberShipFunction4(FuzzyHelper.PFunction);
                        double c = Double.Parse(textBox7.Text);
                        set = FuzzySetCreator<double, double>.CreateInstance(PFunction, left, right, step, a, b, c);
                        ElementsCache.Add(set);
                        break;
                    default: throw new Exception("The membership function hasn't been selected.");
                }
                listBox1.Items.Add("Set #" + ElementsCache.Count.ToString());
                ViewSet(set, chart1);
            }
            catch (Exception ex)
            {
                string message = "An exception has occured. Details: " +
                    ex.Message;
                MessageBox.Show(message, "Message");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
                return;
            ContinuousFuzzySet<double, double> set = 
                (ContinuousFuzzySet<double, double>)ElementsCache.GetAt(listBox1.SelectedIndex);
            ViewSet(set, chart1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
                return;
            ElementsCache.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            if (listBox1.Items.Count == 0)
                chart1.Series.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
                return;
            ListBox.SelectedIndexCollection items = listBox1.SelectedIndices;
            textBox6.Text = string.Empty;
            for (int i = 0; i < items.Count; i++)
            {
                ContinuousFuzzySet<double, double> set =
                    (ContinuousFuzzySet<double, double>)ElementsCache.GetAt(i);
                double val = FuzzySetDescriptor.Entropy(set);
                textBox6.AppendText("Энтропия множества #" + i.ToString() + ": " +
                    val.ToString() + "\r\n");
            }
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = i + 1; j < items.Count; j++)
                {
                    textBox6.AppendText("Эвклидово расстояние между "
                        + (i + 1).ToString() + " и " + (j + 1).ToString() + ": ");
                    ContinuousFuzzySet<double, double> set1 =
                        (ContinuousFuzzySet<double, double>)ElementsCache.GetAt(i);
                    ContinuousFuzzySet<double, double> set2 =
                        (ContinuousFuzzySet<double, double>)ElementsCache.GetAt(j);
                    if (set1.Cardinality() != set2.Cardinality())
                    {
                        textBox6.AppendText("Sets must have equals cardinality!!!");
                        return;
                    }
                    double distance = 0;
                    distance = FuzzySetDescriptor.EuclidianDistance(set1, set2);
                    textBox6.AppendText(distance.ToString() + "\r\n");
                    textBox6.AppendText("Расстояние Хемминга между "
                        + (i + 1).ToString() + " и " + (j + 1).ToString() + ": ");
                    distance = FuzzySetDescriptor.HammingDistance(set1, set2);
                    textBox6.AppendText(distance.ToString() + "\r\n");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count != 2)
            {
                MessageBox.Show("Нужно выбрать 2 множества!", "Сообщение");
                return;
            }            
            int id = listBox1.SelectedIndices[0];
            ContinuousFuzzySet<double, double> A = 
                (ContinuousFuzzySet<double, double>)ElementsCache.GetAt(id);
            id = listBox1.SelectedIndices[1];
            ContinuousFuzzySet<double, double> B =
                (ContinuousFuzzySet<double, double>)ElementsCache.GetAt(id);
            if (A.Cardinality() != B.Cardinality())
            {
                MessageBox.Show("Sets must have equals cardinality!!!", "Message");
                return;
            }
            ContinuousFuzzySet<double, double> Op1 = null;
            ContinuousFuzzySet<double, double> Op2 = null;
            ViewSet(A, "Set A", chart2);
            ViewSet(B, "Set B", chart3);
            ContinuousFuzzySet<double, double> C =
                FuzzyOperations.Sum(A, B);
            Op1 = FuzzyOperations.Inversion(C);
            ViewSet(C, "A + B", chart4);
            ViewSet(Op1, "not(A + B)", chart5);
            ViewSet(A, "Set A", chart6);
            ViewSet(B, "Set B", chart7);
            ViewSet(FuzzyOperations.Inversion(A), "not(A)", chart8);
            ViewSet(FuzzyOperations.Inversion(B), "not(B)", chart9);
            Op2 = FuzzyOperations.Multiply(FuzzyOperations.Inversion(A),
                FuzzyOperations.Inversion(B));
            ViewSet(Op2, "not(A) * not(B)", chart10);
            double d1 = FuzzySetDescriptor.EuclidianDistance(Op1, Op2);
            double d2 = FuzzySetDescriptor.HammingDistance(Op1, Op2);
            label6.Text = "Расстояние Эвклида: " + d1.ToString();
            label7.Text = "Расстояние Хемминга: " + d2.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count != 3)
            {
                MessageBox.Show("Нужно выбрать 3 множества!", "Сообщение");
                return;
            }
            int id = listBox1.SelectedIndices[0];
            ContinuousFuzzySet<double, double> A =
                (ContinuousFuzzySet<double, double>)ElementsCache.GetAt(id);
            id = listBox1.SelectedIndices[1];
            ContinuousFuzzySet<double, double> B =
                (ContinuousFuzzySet<double, double>)ElementsCache.GetAt(id);
            id = listBox1.SelectedIndices[2];
            ContinuousFuzzySet<double, double> C =
                (ContinuousFuzzySet<double, double>)ElementsCache.GetAt(id);
            if (A.Cardinality() + B.Cardinality() 
                + C.Cardinality() != 3 * A.Cardinality())
            {
                MessageBox.Show("Sets must have equals cardinality!!!", "Message");
                return;
            }
            ContinuousFuzzySet<double, double> L = null;
            ContinuousFuzzySet<double, double> R = null;
            ViewSet(A, "Set A", chart11);
            ViewSet(B, "Set B", chart12);
            ViewSet(FuzzyOperations.Inversion(B),
                "not(B)", chart13);
            ViewSet(C, "Set C", chart14);
            ContinuousFuzzySet<double, double> Q = 
                FuzzyOperations.Sum(C, FuzzyOperations.Inversion(B));          
            ViewSet(Q, "not(B) + C", chart15);
            L = FuzzyOperations.Multiply(A, Q);
            ViewSet(L, "A * (not(B) + C)", chart16);
            ContinuousFuzzySet<double, double> Op1 = 
                FuzzyOperations.Multiply(A, FuzzyOperations.Inversion(B));
            ContinuousFuzzySet<double, double> Op2 =
                FuzzyOperations.Multiply(A, C);
            ViewSet(Op1, "A * not(B)", chart17);
            ViewSet(Op2, "A * C", chart18);
            R = FuzzyOperations.Sum(Op1, Op2);
            ViewSet(R, "(A*not(B)+(A*C)", chart19);
            double d1 = FuzzySetDescriptor.EuclidianDistance(L, R);
            double d2 = FuzzySetDescriptor.HammingDistance(L, R);
            label8.Text = "Расстояние Эвклида: " + d1.ToString();
            label9.Text = "Расстояние Хемминга: " + d2.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;
            try
            {
                StreamReader reader = new StreamReader(file);
                string line = string.Empty;
                ICollection<KeyValuePair<double, double>> list =
                    new List<KeyValuePair<double, double>>();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    string[] items = line.Split(new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);
                    if (items.Length != 2)
                        throw new IOException("Incorrect number of arguments!");
                    KeyValuePair<double, double> pair =
                        new KeyValuePair<double, double>(Convert.ToDouble(items[0]), Convert.ToDouble(items[1]));
                    list.Add(pair);
                }
                ContinuousFuzzySet<double, double> set = new ContinuousFuzzySet<double, double>(list);
                ElementsCache.Add(set);
                listBox1.Items.Add("Set #" + ElementsCache.Count.ToString());
                ViewSet(set, chart1);
            }
            catch (Exception ex)
            {
                string message = "An exception has occured. Details: " +
                    ex.Message;
                MessageBox.Show(message, "Message");
            }
        }
    }
}
