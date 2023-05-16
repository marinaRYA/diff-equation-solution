using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vicmmat6
{
    public class methods
    {

       
        public double f(double x, double y) { return x*x-5*y; }
        public Tuple<double[], double[]> EulerMethod(double x0, double y0, double xn, double h)
        {
            // Вычисляем количество шагов
            int n = (int)Math.Ceiling((xn - x0) / h);


            double[] x = new double[n + 1];
            double[] y = new double[n + 1];


            x[0] = x0;
            y[0] = y0;

            // Итерируемся по шагам
            for (int i = 0; i < n; i++)
            {
                // Вычисляем следующее значение y с помощью метода Эйлера
                double k1 = f(x[i], y[i]);
                double k2 = f(x[i] + h, y[i] + h * k1);
                y[i + 1] = y[i] + h * (k1 + k2) / 2;

                // Вычисляем следующее значение x
                x[i + 1] = x[i] + h;
            }

            // Возвращаем массив значений  y
            return Tuple.Create(x, y);
        }
        public Tuple<List <double>, List<double>> RungeKuttaMersonMethod(double x0, double y0, double xn, double h)
        {
            List <double> x = new List <double>();
            List<double> y = new List<double>();
            int i = 0;
            double atol = 0.01;
            x.Add(x0);
            y.Add(y0);
            while (x[i] < xn)
            {
                // Рассчитываем коэффициенты K1-K5
                double k1 = h * f(x[i], y[i]);
                double k2 = h * f(x[i] + h / 3, y[i] + k1 / 3);
                double k3 = h * f(x[i] + h / 3, y[i] + k1 / 6 + k2 / 6);
                double k4 = h * f(x[i] + h / 2, y[i] + k1 / 8 + 3 / 8 * k3);
                double k5 = h * f(x[i] + h, y[i] + k1 / 2 - 3 / 2 * k3 + 2 * k4);
                double eps = Math.Abs((2 * k1 - 9 * k3 + 8 * k4 - k5) / h);

                if (eps < atol / 32) h *= 2;
              
                else if (eps > atol)
                    {
                        h /= 2;
                    }
                
                else
                {
                    double x_new = x[i] + h;
                    double y_new = y[i] + (k1 + 4 * k4 + k5) / 6;
                    i++;
                    x.Add(x_new);
                    y.Add(y_new);
                    x[i] = x_new;
                    y[i] = y_new;
                }
            }
            
            // Возвращаем массив значений y и x
            return Tuple.Create(x, y);
        }
        public Tuple<double[], double[]> ModifiedEulerMethod(double x0, double y0, double xn, double h)
        {
            int n = (int)Math.Ceiling((xn - x0) / h);
            double[] y = new double[n + 1];
            double[] x = new double[n + 1];

            y[0] = y0;
            x[0] = x0;
            double k1, k2;
            for (int i = 0; i < n; i++)
            {
                k1 = f(x[i], y[i]);
                k2 = f(x[i] + h / 2, y[i] + h / 2 * k1);
                y[i + 1] = y[i] + h * k2;
                x[i + 1] = x0 + i * h;
            }
            return Tuple.Create(x, y);
        }
        public Tuple<double[],double[]> AdamsMethod5(double x0, double y0,double xn, double h)
        {
            int n = (int)Math.Ceiling((xn - x0) / h);
            double[] y = new double[n + 1];
            double[] x = new double[n + 1];

            y[0] = y0;
            x[0] = x0;
            for (int i = 0; i < 5; i++)
            {
                // Вычисляем коэффициенты метода Рунге-Кутта
                double k1 = f(x[i], y[i]);
                double k2 = f(x[i] + h / 2, y[i] + h / 2 * k1);
                double k3 = f(x[i] + h / 2, y[i] + h / 2 * k2);
                double k4 = f(x[i] + h, y[i] + h * k3);

                // Вычисляем следующее значение y с помощью метода Рунге-Кутта
                y[i + 1] = y[i] + h * (k1 + 2 * k2 + 2 * k3 + k4) / 6;

                // Вычисляем следующее значение x
                x[i + 1] = x[i] + h;
            }

            for (int i = 4; i < n; i++)
            {
                y[i + 1] = y[i] + h / 720 * (1901 * f(x[i], y[i]) - 2774 * f(x[i - 1], y[i - 1]) + 2616 * f(x[i - 2], y[i - 2]) - 1274 * f(x[i - 3], y[i - 3]) + 251 * f(x[i - 4], y[i - 4]));
                x[i + 1] = x[i] + h;
            }
            return Tuple.Create(x, y);
        }

    }
    internal static class Program
    {

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
      
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
