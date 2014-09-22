using System;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using FuzzySets;

namespace FSP
{
    
    public enum FunctionTypes
    {
        SFunction = 0,
        ZFunction = 1,
        PFunction = 2,
        Custom = 3
    }

    public partial class MainForm : Form
    {
        #region Params
        class Params
        {
            private double A = 0;
            private double B = 0;
            private double C = 0;

            public double a { get { return A; } }
            public double b { get { return B; } }
            public double c { get { return C; } }
            
            public Params() { }

            public Params(double a, double b, double c)
            {
                this.A = a;
                this.B = b;
                this.C = c;
            }

            public void TryParse(string s1, string s2)
            {
                if (s1 == "" && s2 == "")
                    throw new ArgumentException("Incorrest argument");
                if (Double.TryParse(s1, out A) == false)
                    throw new ArgumentException("Incorrest argument");
                if (Double.TryParse(s2, out B) == false)
                    throw new ArgumentException("Incorrest argument");
            }

            public void TryParse(string s1, string s2, string s3)
            {
                if (s1 == "" && s2 == "" && s3 == "")
                    throw new ArgumentException("Incorrest argument");
                if (Double.TryParse(s1, out A) == false)
                    throw new ArgumentException("Incorrest argument");
                if (Double.TryParse(s2, out B) == false)
                    throw new ArgumentException("Incorrest argument");
                if (Double.TryParse(s3, out C) == false)
                    throw new ArgumentException("Incorrest argument");
            }

            public Params(string s1, string s2)
            {
                TryParse(s1, s2);
            }

            public Params(string s1, string s2, string s3)
            {
                TryParse(s1, s2, s3);
            }
        }
        #endregion
        #region
        private double[] GetPoints()
        {
            string[] values = textBox4.Text.Split(new string[] { " ", ",", "\r\n" },
                StringSplitOptions.RemoveEmptyEntries);

            double[] points = new double[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                points[i] = Double.Parse(values[i], CultureInfo.GetCultureInfo("es-MX"));
            }

            return points;
        }

        private void SendToText(string line, TextBox text)
        {
            string[] items = line.Split(new string[]{" ", ",", "\r\n"}, 
                StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < items.Length; i++)
                text.AppendText(items[i] + "\r\n");
        }

        private string[] ReadSimpleStringFromText(TextBox text)
        {
            string[] items = text.Text.Split(new string[] { " ", ",", "\r\n" },
                StringSplitOptions.RemoveEmptyEntries);
            string[] src = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
                src[i] = items[i];
            return src;
        }

        private List<KeyValuePair<string, double>> ReadFromText(TextBox text)
        {
            List<KeyValuePair<string, double>> list = new List<KeyValuePair<string, double>>();
            for (int i = 0; i < text.Lines.Length; i++)
            {
                string[] line = text.Lines[i].Split(new char[] { ' ' });
                double val = Double.Parse(line[1], CultureInfo.GetCultureInfo("es-MX"));
                KeyValuePair<string, double> pair = new KeyValuePair<string, double>(line[0], val);
                list.Add(pair);
            }
            return list;
        }

        private void FillComboBox(ComboBox[] boxes,
            ICollection<string> collection)
        {
            IEnumerator<string> it = collection.GetEnumerator();
            while (it.MoveNext())
            {
                for (int i = 0; i < boxes.Length; i++)
                    boxes[i].Items.Add(it.Current);
            }
        }

        private List<KeyValuePair<double, double>> CreateContiniousSet(double a, double b, double h,
            TextBox text)
        {
            List<KeyValuePair<double, double>> data = new List<KeyValuePair<double, double>>();
            string[] items = text.Text.Split(new string[] { " ", ",", "\r\n" },
                StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            double x = a;
            while (i < items.Length && x <= b)
            {
                KeyValuePair<double, double> pair = new KeyValuePair<double, double>(x,
                    Double.Parse(items[i++], CultureInfo.GetCultureInfo("es-MX")));
                data.Add(pair);
                x += h;
            }
            return data;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            try
            {
                openFileDialog1.ShowDialog();
                StreamReader reader = new StreamReader(openFileDialog1.FileName);
                string line = string.Empty;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    SendToText(line, textBox4);
                }
                reader.Close();
            }
            catch (Exception)
            { }
        }

        private void ViewSet(ContinuousFuzzySet<string, double> set, Chart chart1)
        {
            chart1.Series.Clear();
            chart1.Series.Add("Fuzzy Set");
            chart1.Series["Fuzzy Set"].ChartType = SeriesChartType.Column;
            IEnumerator<KeyValuePair<string, double>> it = set.GetEnumerator();           
            while (it.MoveNext())
            {
                KeyValuePair<string, double> pair = it.Current;
                chart1.Series[0].Points.AddXY(pair.Key, pair.Value);
            }
        }

        private void ViewSet(ContinuousFuzzySet<double, double> set, Chart chart1)
        {
            chart1.Series.Clear();
            chart1.Series.Add("Fuzzy Set");
            chart1.Series["Fuzzy Set"].ChartType = SeriesChartType.Spline;
            chart1.Series["Fuzzy Set"].BorderDashStyle = ChartDashStyle.Solid;
            chart1.Series["Fuzzy Set"].BorderWidth = 3;
            IEnumerator<KeyValuePair<double, double>> it = set.GetEnumerator();
            while (it.MoveNext())
            {
                KeyValuePair<double, double> pair = it.Current;
                chart1.Series[0].Points.AddXY(pair.Key, pair.Value);
            }
        }
        #endregion

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double left = 0;
            double right = 0;
            double step = 0;
            ContinuousFuzzySet<double, double> ConSet = null;
            DiscreteFuzzyStringSet DiscSet = null;
            Params p = new Params();
            if (radioButton2.Checked)
            {
                try
                {
                    if (comboBox1.SelectedIndex == 1 ||
                        comboBox1.SelectedIndex == 0)
                        p.TryParse(textBox5.Text, textBox6.Text);
                    else if (comboBox1.SelectedIndex == 2)
                        p.TryParse(textBox5.Text, textBox6.Text, textBox7.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Неверный аргумент!", "Ошибка");
                    return;
                }
                try
                {
                    Double.TryParse(textBox1.Text, out left);
                    Double.TryParse(textBox2.Text, out right);
                    Double.TryParse(textBox3.Text, out step);
                }
                catch (Exception)
                {
                    MessageBox.Show("Неверный аргумент!", "Ошибка");
                    return;
                }
                ConSet = CreateContinuousSet(left, right, step, p.a, p.b, p.c);
                ElementsCache.Add(ConSet);
            }
            else if (radioButton1.Checked)
            {
                string[] src = textBox4.Text.Split(new string[] { " ", ".", ",", ":", "\r\n" },
                    StringSplitOptions.RemoveEmptyEntries);
                if (comboBox2.SelectedIndex < 0 && comboBox3.SelectedIndex < 0 &&
                    comboBox4.SelectedIndex < 0 && comboBox1.SelectedIndex != 3)
                {
                    MessageBox.Show("Неверный аргумент!", "Ошибка");
                    return;
                }
                DiscSet = CreateDiscreteStringSet(comboBox2.SelectedIndex,
                    comboBox3.SelectedIndex, comboBox4.SelectedIndex, src);
                ElementsCache.Add(DiscSet);
            }
            listBox1.Items.Add("set #" + ElementsCache.Count.ToString());
        }


        private DiscreteFuzzyStringSet CreateDiscreteStringSet(double a, double b, double c, string[] src)
        {
            DiscreteFuzzyStringSet set = null;
            switch (comboBox1.SelectedIndex)
            {
                case (int)FunctionTypes.SFunction:
                    FuzzySetCreator<double, double>.MemberShipFunction3 Sfunc =
                        new FuzzySetCreator<double, double>.MemberShipFunction3(FuzzyHelper.SFunction);
                    set = FuzzySetCreator<double, double>.CreateInstance(Sfunc, a, b, src);
                    break;
                case (int)FunctionTypes.ZFunction:
                    FuzzySetCreator<double, double>.MemberShipFunction3 Zfunc =
                        new FuzzySetCreator<double, double>.MemberShipFunction3(FuzzyHelper.ZFunction);
                    set = FuzzySetCreator<double, double>.CreateInstance(Zfunc, a, b, src);
                    break;
                case (int)FunctionTypes.PFunction:
                    FuzzySetCreator<double, double>.MemberShipFunction4 Pfunc =
                        new FuzzySetCreator<double, double>.MemberShipFunction4(FuzzyHelper.PFunction);
                    set = FuzzySetCreator<double, double>.CreateInstance(Pfunc, a, b, c, src);
                    break;
                case (int)FunctionTypes.Custom:
                    List<KeyValuePair<string, double>> list = ReadFromText(textBox4);
                    set = FuzzySetCreator<string, double>.CreateInstance(list);
                    break;
            }
            return set;
        }

        private ContinuousFuzzySet<double, double> CreateContinuousSet(double left, double right, double step,
            double a, double b, double c)
        {
            ContinuousFuzzySet<double, double> set = null;
            switch (comboBox1.SelectedIndex)
            {
                case (int)FunctionTypes.SFunction:
                    FuzzySetCreator<double, double>.MemberShipFunction3 Sfunc =
                        new FuzzySetCreator<double, double>.MemberShipFunction3(FuzzyHelper.SFunction);
                    set = FuzzySetCreator<double, double>.CreateInstance(Sfunc, left, right, step, a, b);
                    break;
                case (int)FunctionTypes.ZFunction:
                    FuzzySetCreator<double, double>.MemberShipFunction3 Zfunc =
                        new FuzzySetCreator<double, double>.MemberShipFunction3(FuzzyHelper.ZFunction);
                    set = FuzzySetCreator<double, double>.CreateInstance(Zfunc, left, right, step, a, b);
                    break;
                case (int)FunctionTypes.PFunction:
                    FuzzySetCreator<double, double>.MemberShipFunction4 Pfunc =
                        new FuzzySetCreator<double, double>.MemberShipFunction4(FuzzyHelper.PFunction);
                    set = FuzzySetCreator<double, double>.CreateInstance(Pfunc, left, right, step, a, b, c);
                    break;
                case (int)FunctionTypes.Custom:
                    List<KeyValuePair<double, double>> DataList = CreateContiniousSet(left, right, step,
                        textBox4);
                    set = FuzzySetCreator<double, double>.CreateInstance(DataList);
                    break;
            }
            return set;
        }

        private void удалитьИзКэшаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = listBox1.SelectedIndex;
            if (id == -1)
                return;
            ElementsCache.RemoveAt(id);
            listBox1.Items.RemoveAt(id);
        }

        private void графикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart1.ResetAutoValues();
            int id = listBox1.SelectedIndex;
            ContinuousFuzzySet<double, double> ConSet = null;
            DiscreteFuzzyStringSet DiskSet = null;
            if (ElementsCache.GetAt(id).GetType() == typeof(ContinuousFuzzySet<double, double>))
            {
                ConSet = (ContinuousFuzzySet<double, double>)ElementsCache.GetAt(id);
                ViewSet(ConSet, chart1);
            }
            else if (ElementsCache.GetAt(id).GetType() == typeof(DiscreteFuzzyStringSet))
            {
                DiskSet = (DiscreteFuzzyStringSet)ElementsCache.GetAt(id);
                ViewSet(DiskSet, chart1);
            }
        }

        private void очиститьЧартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
        }

        private void очиститьКэшToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElementsCache.Clear();
            listBox1.Items.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox3.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            string[] src = ReadSimpleStringFromText(textBox4);
            FillComboBox(new ComboBox[] { comboBox2, comboBox3, comboBox4 }, src);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
        }
    }
}
