using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AngouriMath;
using AngouriMath.Extensions;
using static AngouriMath.MathS;

namespace GPSS_IDZ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int function_num = 0;
        int new_function = 0;
        Methods methods = new Methods();

        private void Calculation(object sender, RoutedEventArgs e)
        {
            double Mo1, Mo2, a, S1, bd, ce, Mo1DivMo2, PointsCount;
            methods.LogOut = "";
            try
            {
                bd = (double)start.Text.EvalNumerical();
                ce = (double)end.Text.EvalNumerical();
                Mo1DivMo2 = (double)mo1divmo2.Text.EvalNumerical();
                PointsCount = (double)pointscount.Text.EvalNumerical();

                List<double> D = new List<double>() { (double)d0.Text.EvalNumerical(), (double)d1.Text.EvalNumerical(), (double)d2.Text.EvalNumerical(), (double)d3.Text.EvalNumerical() };
                List<double> E = new List<double>() { (double)e0.Text.EvalNumerical(), (double)e1.Text.EvalNumerical(), (double)e2.Text.EvalNumerical(), (double)e3.Text.EvalNumerical() };
                List<double> K = new List<double>() { (double)k0.Text.EvalNumerical(), (double)k1.Text.EvalNumerical(), (double)k2.Text.EvalNumerical(), (double)k3.Text.EvalNumerical() };
                List<double> Y = new List<double>() { (double)y0.Text.EvalNumerical(), (double)y1.Text.EvalNumerical(), (double)y2.Text.EvalNumerical(), (double)y3.Text.EvalNumerical() };
                
                if(PointsCount > 4)
                {
                    D.Add((double)d4.Text.EvalNumerical());
                    E.Add((double)e4.Text.EvalNumerical());
                    K.Add((double)k4.Text.EvalNumerical());
                    Y.Add((double)y4.Text.EvalNumerical());
                }
                if (PointsCount > 5)
                {
                    D.Add((double)d5.Text.EvalNumerical());
                    E.Add((double)e5.Text.EvalNumerical());
                    K.Add((double)k5.Text.EvalNumerical());
                    Y.Add((double)y5.Text.EvalNumerical());
                }
                if (PointsCount > 6)
                {
                    D.Add((double)d6.Text.EvalNumerical());
                    E.Add((double)e6.Text.EvalNumerical());
                    K.Add((double)k6.Text.EvalNumerical());
                    Y.Add((double)y6.Text.EvalNumerical());
                }

                List<double> A = new List<double>() { (double)a0.Text.EvalNumerical(), (double)a1.Text.EvalNumerical(), (double)a2.Text.EvalNumerical(), (double)a3.Text.EvalNumerical() };
                List<double> B = new List<double>() { (double)b0.Text.EvalNumerical(), (double)b1.Text.EvalNumerical(), (double)b2.Text.EvalNumerical(), (double)b3.Text.EvalNumerical() };
                List<double> P = new List<double>() { (double)p0.Text.EvalNumerical(), (double)p1.Text.EvalNumerical(), (double)p2.Text.EvalNumerical(), (double)p3.Text.EvalNumerical() };

                S1 = methods.GetS1(bd, ce, D, E, K, Y);

                Mo1 = methods.GetMo1(S1, bd, ce, D, E, K, Y);

                Mo2 = methods.FindMo2(Mo1 / Mo1DivMo2, Mo.Text, function_num, new_function, fx.Text, bd, ce);

                a = methods.GetP0(A, B, P, Mo1, Mo2);

                methods.PrintP1(S1, bd, ce, D, E, K, Y);

                log.Text = methods.LogOut;

                Mo1TB.Text = Mo1.ToString("F4");
                Mo2TB.Text = Mo2.ToString("F4");
                MoDivTB.Text = (Mo1/Mo2).ToString("F1");
                aTB.Text = a.ToString("F2");
            }
            catch
            {
                MessageBox.Show("Ошибка");
                return;
            }


        }

        #region "OBRABOTCHIK"
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            new_function = 1;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            new_function = 0;
        }


        private void tester(object sender, RoutedEventArgs e)
        {
            Mo.Text = "e^(a + b^2 / 2) + c";
            function_num = 15;
            pointscount.Text = "6";
            start.Text = "0";
            end.Text = "22";
            mo1divmo2.Text = "3";

            a0.Text = "1";
            a1.Text = "1";
            a2.Text = "7";
            a3.Text = "8";
            b0.Text = "0";
            b1.Text = "1";
            b2.Text = "0";
            b3.Text = "0";
            p0.Text = "0.4";
            p1.Text = "0.42";
            p2.Text = "0.11";
            p3.Text = "0.07";

            d0.Text = "1";
            e0.Text = "0";
            k0.Text = "1";
            y0.Text = "1";

            d1.Text = "4";
            e1.Text = "1";
            k1.Text = "5";
            y1.Text = "1";

            d2.Text = "3";
            e2.Text = "2";
            k2.Text = "5";
            y2.Text = "0";

            d3.Text = "2";
            e3.Text = "3";
            k3.Text = "5";
            y3.Text = "0";

            d4.Text = "1";
            e4.Text = "4";
            k4.Text = "5";
            y4.Text = "2";

            d5.Text = "0";
            e5.Text = "1";
            k5.Text = "1";
            y5.Text = "2";
        }

        private void Mo_set_1(object sender, RoutedEventArgs e)
        {
            Mo.Text = "c * (b - a) / (c + d) + a";
            function_num = 1;
        }
        private void Mo_set_2(object sender, RoutedEventArgs e)
        {
            Mo.Text = "a * b";
            function_num = 2;
        }
        private void Mo_set_3(object sender, RoutedEventArgs e)
        {
            Mo.Text = "(a + b) / 2";
            function_num = 3;
        }
        private void Mo_set_4(object sender, RoutedEventArgs e)
        {
            Mo.Text = "a + b";
            function_num = 4;
        }
        private void Mo_set_7(object sender, RoutedEventArgs e)
        {
            Mo.Text = "a * b + c";
            function_num = 7;
        }
        private void Mo_set_8(object sender, RoutedEventArgs e)
        {
            Mo.Text = "(1 - a) / a";
            function_num = 8;
        }
        private void Mo_set_11(object sender, RoutedEventArgs e)
        {
            Mo.Text = "a";
            function_num = 11;
        }
        private void Mo_set_12(object sender, RoutedEventArgs e)
        {
            Mo.Text = "a";
            function_num = 12;
        }
        private void Mo_set_15(object sender, RoutedEventArgs e)
        {
            Mo.Text = "e^(a + b^2 / 2.0) + c";
            function_num = 15;
        }
        private void Mo_set_16(object sender, RoutedEventArgs e)
        {
            Mo.Text = "a * (1 - b) / b";
            function_num = 16;
        }
        private void Mo_set_17(object sender, RoutedEventArgs e)
        {
            Mo.Text = "a";
            function_num = 17;
        }
        private void Mo_set_18(object sender, RoutedEventArgs e)
        {
            Mo.Text = "(a * b) / (a - 1)";
            function_num = 18;
        }
        private void Mo_set_19(object sender, RoutedEventArgs e)
        {
            Mo.Text = "b / (a - 1) + c";
            function_num = 19;
        }
        private void Mo_set_21(object sender, RoutedEventArgs e)
        {
            Mo.Text = "a";
            function_num = 21;
        }
        private void Mo_set_22(object sender, RoutedEventArgs e)
        {
            Mo.Text = "(a + b + c) / 3";
            function_num = 22;
        }
        private void Mo_set_23(object sender, RoutedEventArgs e)
        {
            Mo.Text = "(a + b) / 2";
            function_num = 23;
        }

        #endregion
    }
}
