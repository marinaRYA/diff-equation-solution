using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace vicmmat6
{
     public partial class Form1 : Form
    {
        methods M = new methods();
        public double x=0;
        public double y=10;
        public double h=0.1;
        public double xmax=6;
        public double func_analitic(double x) { return 0.008 * (25 * x * x - 10 * x + 2) + 9.984 * Math.Exp(-5 * x); }
            
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
      

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                double a = double.Parse(textBox1.Text);
                x = a;
                a = double.Parse(textBox2.Text);
                y = a;
                a = double.Parse(textBox3.Text);
                xmax = a;
                a = double.Parse(textBox4.Text);
                h = a;
            }
            catch (System.FormatException)
            {
                MessageBox.Show(
       "Wrong data",
       "Error",
       MessageBoxButtons.OK,
       MessageBoxIcon.Information,
       MessageBoxDefaultButton.Button1,
       MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            Tuple<double[], double[]> result = M.EulerMethod(x,y,xmax,h);
            double[] X = result.Item1;
            double[] Y = result.Item2;
            for (int i = 0; i < X.Length; i++) this.chart1.Series[0].Points.AddXY(X[i], Y[i]);
            
       
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            chart1.Series[1].Points.Clear();
            Tuple<double[], double[]> result = M.ModifiedEulerMethod(x,y,xmax,h);
            double[] X = result.Item1;
            double[] Y = result.Item2;
            for (int i = 0; i < X.Length; i++) this.chart1.Series[1].Points.AddXY(X[i], Y[i]);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Series[2].Points.Clear();
            Tuple<List <double>,List<double>> result = M.RungeKuttaMersonMethod(x,y,xmax,h);
            List <double> X = result.Item1;
            List<double> Y = result.Item2;
            for (int i = 0; i < X.Count; i++) this.chart1.Series[2].Points.AddXY(X[i], Y[i]);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            chart1.Series[3].Points.Clear();
            Tuple<double[], double[]> result = M.AdamsMethod5(x,y,xmax,h);
            double[] X = result.Item1;
            double[] Y = result.Item2;
            for (int i = 0; i < X.Length; i++) this.chart1.Series[3].Points.AddXY(X[i], Y[i]);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            chart1.Series[4].Points.Clear();
            int n = (int)Math.Ceiling((xmax - x) / h);
            for (double i=x; i<=xmax;i+=h ) this.chart1.Series[4].Points.AddXY(i, func_analitic(i));

        }

        private void button6_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.Series[4].Points.Clear();

        }

       
    }
}
