using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LinguisticVariables
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // НЕ
        private double SemanticRule1(double y)
        {
            return 1 - y;
        }

        // ОЧЕНЬ
        private double SemanticRule2(double y)
        {
            return y * y;
        }

        // БОЛЕЕ-МЕНЕЕ
        private double SemanticRule3(double y)
        {
            return Math.Sqrt(y);
        }

        private double SemanticRuleCold(double x)
        {
            return 1.0 / (1 + Math.Pow((x-10)/7, 12));
        }

        private double SemanticComfortable(double x)
        {
            return 1.0 / (1 + Math.Pow((x - 20) / 3, 6));
        }

        private double SemanticHot(double x)
        {
            return 1.0 / (1 + Math.Pow((x - 30) / 6, 10));
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double a = 5;
            double b = 50;
            double h = 1;
            List<KeyValuePair<double, double>>[] arr
                = new List<KeyValuePair<double, double>>[3];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = new List<KeyValuePair<double, double>>();

            for (double x = a; x <= b; x += h)
            {
                arr[0].Add(new KeyValuePair<double, double>(x, SemanticRuleCold(x)));
                arr[1].Add(new KeyValuePair<double, double>(x, SemanticComfortable(x)));
                arr[2].Add(new KeyValuePair<double, double>(x, SemanticHot(x)));
            }

            List<Term> terms = new List<Term>();
            terms.Add(new Term("Холодно", new FuzzySets.ContinuousFuzzySet<double, double>(arr[0])));
            terms.Add(new Term("Комфортно", new FuzzySets.ContinuousFuzzySet<double, double>(arr[1])));
            terms.Add(new Term("Жарко", new FuzzySets.ContinuousFuzzySet<double, double>(arr[2])));

            List<Rule> rules = new List<Rule>();
            rules.Add(new Rule("Не", new Rule.SemanticRuleDelegate(SemanticRule1)));
            rules.Add(new Rule("Очень", new Rule.SemanticRuleDelegate(SemanticRule2)));
            rules.Add(new Rule("Более-менее", new Rule.SemanticRuleDelegate(SemanticRule3)));

            LinguisticVariable var = new LinguisticVariable("Температура в комнате", terms, rules);
            List<Term> NewTerms = LinguisticVariableHelper.GenerateTerms(var);

            chart1.Series.Clear();
            chart1.ResetAutoValues();
            for (int i = 0; i < 3; i++)
            {
                chart1.Series.Add(NewTerms[i].Name);
                chart1.Series[NewTerms[i].Name].ChartType = SeriesChartType.Spline;
                chart1.Series[NewTerms[i].Name].BorderWidth = 3;
                Dictionary<double, double>.Enumerator it
                    = NewTerms[i].FuzzySet.GetEnumerator();
                while (it.MoveNext())
                    chart1.Series[NewTerms[i].Name].Points.AddXY(it.Current.Key, it.Current.Value);
            }

            chart2.Series.Clear();
            chart2.ResetAutoValues();
            for (int i = 3; i < 6; i++)
            {
                chart2.Series.Add(NewTerms[i].Name);
                chart2.Series[NewTerms[i].Name].ChartType = SeriesChartType.Spline;
                chart2.Series[NewTerms[i].Name].BorderWidth = 3;
                Dictionary<double, double>.Enumerator it
                    = NewTerms[i].FuzzySet.GetEnumerator();
                while (it.MoveNext())
                    chart2.Series[NewTerms[i].Name].Points.AddXY(it.Current.Key, it.Current.Value);
            }

            chart3.Series.Clear();
            chart3.ResetAutoValues();
            for (int i = 6; i < 9; i++)
            {
                chart3.Series.Add(NewTerms[i].Name);
                chart3.Series[NewTerms[i].Name].ChartType = SeriesChartType.Spline;
                chart3.Series[NewTerms[i].Name].BorderWidth = 3;
                Dictionary<double, double>.Enumerator it
                    = NewTerms[i].FuzzySet.GetEnumerator();
                while (it.MoveNext())
                    chart3.Series[NewTerms[i].Name].Points.AddXY(it.Current.Key, it.Current.Value);
            }

            chart4.Series.Clear();
            chart4.ResetAutoValues();
            for (int i = 9; i < NewTerms.Count; i++)
            {
                chart4.Series.Add(NewTerms[i].Name);
                chart4.Series[NewTerms[i].Name].ChartType = SeriesChartType.Spline;
                chart4.Series[NewTerms[i].Name].BorderWidth = 3;
                Dictionary<double, double>.Enumerator it
                    = NewTerms[i].FuzzySet.GetEnumerator();
                while (it.MoveNext())
                    chart4.Series[NewTerms[i].Name].Points.AddXY(it.Current.Key, it.Current.Value);
            }

        }

    }
}
