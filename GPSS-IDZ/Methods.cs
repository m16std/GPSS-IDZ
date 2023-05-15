using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using AngouriMath;
using AngouriMath.Extensions;
using static System.Console;
using static AngouriMath.MathS;
namespace GPSS_IDZ
{
    internal class Methods
    {
        public string LogOut = "";
        public void PrintP2(double a, double b, double c, double d, int variant, int new_function, string fx, double bd, double ce)
        {
            a = Math.Round(a * 100) / 100;
            b = Math.Round(b * 100) / 100;
            c = Math.Round(c * 100) / 100;
            d = Math.Round(d * 100) / 100;

            if (new_function == 1)
            {
                for (double xi = bd; xi < ce; xi += 1)
                {
                    LogOut += Math.Round((double)fx.Substitute("a", a).Substitute("b", b).Substitute("c", c).Substitute("d", d).Substitute("x", xi).EvalNumerical(), 3) + ", " + Math.Round(xi) + "/\n";
                }
            }

            if (variant == 1)//A1 A1 a1 a2
                LogOut += "BETA(1, " + a + ", " + b + ", " + c + ", " + d + ")";

            if (variant == 2)//t p - -
                LogOut += "BINORMAN(1, " + a + ", " + b + ")";

            if (variant == 3)//i j - -
                LogOut += "DUNIFORM(1, " + a + ", " + b + ")";

            if (variant == 4)//B A - -
                LogOut += "EXPONENTIAL(1, " + b + ", " + a + ")";

            if (variant == 7)//a b A -
                LogOut += "GAMMA(1, " + c + ", " + b + ", " + c + ")";

            if (variant == 8)//p - - -
                LogOut += "GEOMETRIC(1, " + a + ")";

            if (variant == 11)//A - - -
                LogOut += "LAPLACE(1, " + a + ", 1)";

            if (variant == 12)//A - - -
                LogOut += "LOGISTIC(1, " + a + ", 1)";

            if (variant == 15)//u o A -
                LogOut += "LOGNORMAL(1, " + c + ", " + a + ", " + b + ")";

            if (variant == 16)//s p - -
                LogOut += "NEGBINOM(1, " + a + ", " + b + ")";

            if (variant == 17)//u - - -
                LogOut += "NORMAL(1, " + a + ", 1)";

            if (variant == 18)//b A - -
                LogOut += "PARETO(1, " + b + ", " + a + ")";

            if (variant == 19)//a b A -
                LogOut += "PEARSON5(1, " + c + ", " + b + ", " + a + ")";

            if (variant == 21)//A - - -
                LogOut += "POISSON(1, " + a + ")";

            if (variant == 22)//a b c -
                LogOut += "TRIANGULAR(1, 10" + a + ", " + b + ", " + c + ")";

            if (variant == 23)//a b - -
                LogOut += "UNIFORM(1, " + a + ", " + b + ")";

            if (variant == 24)//a b A -
                LogOut += "WEIBULL(1, " + c + ", " + b + ", " + a + ")";

            LogOut += "\n\n";
        }

        public double FindMo2(double x, string Mo, int function_num, int new_function, string fx, double bd, double ce)
        {
            double delta;
            for (double d = 0; d < 100; d += 0.1)
            {
                for (double c = 0; c < 100; c += 0.1)
                {
                    for (double b = 0; b < 100; b += 0.1)
                    {
                        for (double a = 0; a < 100; a += 0.1)
                        {
                            delta = Math.Abs(x - (double)Mo.Substitute("a", a).Substitute("b", b).Substitute("c", c).Substitute("d", d).EvalNumerical());
                            if (delta < 0.1)
                            {
                                PrintP2(a, b, c, d, function_num, new_function, fx, bd, ce);
                                return (double)Mo.Substitute("a", a).Substitute("b", b).Substitute("c", c).Substitute("d", d).EvalNumerical();
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Мат. ожидание для первой функции не найдено");
            return 0; 
        }

        public double GetS1(double d, double e, List<double> d1, List<double> e1, List<double> k1, List<double> y1)
        {
            double interval, S1 = 0;
            for (int i = 0; i < d1.Count() - 1; i++)
            {
                interval = (d1[i + 1] * d + e1[i + 1] * e) / k1[i + 1] - (d1[i] * d + e1[i] * e) / k1[i];
                S1 += (y1[i] + y1[i + 1]) / 2.0 * interval;
            }
            return S1;
        }

        public double GetP1(double x, double d, double e, List<double> d1, List<double> e1, List<double> k1, List<double> y1)
        {
            int left_i = -1;
            for (int i = 0; i < d1.Count() - 1; i++)
            {
                if ((d1[i] * d + e1[i] * e) / k1[i] < x && x < (d1[i + 1] * d + e1[i + 1] * e) / k1[i + 1])
                {
                    left_i = i;
                    continue;
                }
                if (x == (d1[i] * d + e1[i] * e) / k1[i])
                {
                    return y1[i];
                }
                if (x == (d1[i + 1] * d + e1[i + 1] * e) / k1[i + 1])
                {
                    return y1[i + 1];
                }
            }


            if (x < (d1[0] * d + e1[0] * e) / k1[0])
            {
                return 0;
            }

            if (x > (d1[d1.Count() - 1] * d + e1[d1.Count() - 1] * e) / k1[d1.Count() - 1])
            {
                return 0;
            }

            double left_y = y1[left_i], right_y = y1[left_i + 1];

            if (left_y == right_y)
                return left_y;

            double interval = (d1[left_i + 1] * d + e1[left_i + 1] * e) / k1[left_i + 1] - (d1[left_i] * d + e1[left_i] * e) / k1[left_i];

            double betwen_x_and_left_x = x - (d1[left_i] * d + e1[left_i] * e) / k1[left_i];

            return right_y * betwen_x_and_left_x / interval + left_y * (interval - betwen_x_and_left_x) / interval;
        }

        public double GetP0(List<double> a0, List<double> x0, List<double> P0, double Mo1, double Mo2)
        {
            double Mo0_a = 0, Mo0_x = 0;

            for (int i = 0; i < a0.Count(); i++)
            {
                Mo0_a += a0[i] * P0[i];
                Mo0_x += x0[i] * P0[i];
            }

            double a, Mo0;
            Mo0 = 0.87 / (1.0 / Mo1 + 1.0 / Mo2);
            a = (Mo0 - Mo0_x) / Mo0_a;


            double P = 0;
            for (int i = 0; i < a0.Count(); i++)
            {
                P += P0[i];
                LogOut += Math.Round(P, 1) + ", " + Math.Round((a0[i] * a + x0[i]), 3) + "/\n";
            }

            return a;
        }

        public void PrintP1(double S1, double d, double e, List<double> d1, List<double> e1, List<double> k1, List<double> Y1)
        {
            LogOut += "\n";

            double P = 0, step = 0.001;
            for (double i = d; i < e + step; i += step)
            {
                if ((i % 1) < step / 2 || (i % 1) > 1 - step / 2)
                    LogOut += Math.Round(i, 1) + ", " + Math.Round(P, 3) + "/\n";
                P += GetP1(i, d, e, d1, e1, k1, Y1) * step / S1;
            }

        }

        public double GetMo1(double S1, double d, double e, List<double> d1, List<double> e1, List<double> k1, List<double> Y1)
        {
            double P = 0, step = 0.001, Mo1 = 0, best = 100;
            for (double i = d; i < e + step; i += step)
            {
                P += GetP1(i, d, e, d1, e1, k1, Y1) * step / S1;
                if (Math.Abs(P - 0.5) < best)
                {
                    best = Math.Abs(P - 0.5);
                    Mo1 = i;
                }
            }
            return Mo1;
        }
    }
}

