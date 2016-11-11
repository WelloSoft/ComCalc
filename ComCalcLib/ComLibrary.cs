
using System.Collections;
using System;
using System.Collections.Generic;

namespace ComCalcLib
{
	  public class DefaultComLibrary
    {

        static public Random _randSingleton = new Random();
        // ------------------------- OPERATOR LISTS ----------------------------

        [ComOperator('+', 1)]
        public static double Add(double a, double b)
        {
            return a + b;
        }

        [ComOperator('-', 1)]
        public static double Subtract(double a, double b)
        {
            return a - b;
        }

        [ComOperator('*', 2)]
        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        [ComOperator('/', 2)]
        public static double Division(double a, double b)
        {
            if (b == 0)
                throw ComCalcException.e_DivByZero;
            return a / b;
        }

        [ComOperator('_', 3)]
        public static double Root(double a, double b)
        {
            return Math.Pow(a, 1 / b);
        }


        [ComOperator('^', 3)]
        public static double Power(double a, double b)
        {
            if (b != Math.Floor(b) && a < 0)
                throw ComCalcException.e_PowImaginer;
            return Math.Pow(a, b);
        }

        [ComOperator('~', 3)]
        public static double Log(double a, double b)
        {
            return Math.Log(b, a);
        }

        [ComOperator('%', 4)]
        public static double Modulo(double a, double b)
        {
            if (b == 0)
                throw ComCalcException.e_ModByZero;
            return a % b;
        }

        [ComOperator('@', 5)]
        public static double Exponent(double a, double b)
        {
            return a * Math.Pow(10, b);
        }

        [ComOperator('&', 6)]
        public static double Minb(double a, double b)
        {
            return Math.Min(a, b);
        }

        [ComOperator('|', 6)]
        public static double Maxb(double a, double b)
        {
            return Math.Max(a, b);
        }

        /*public static double Crop (double a, double b)
        {
            double p = Power(10, b);
            return Math.Floor(a*p)/p;
        }*/

    // --------------------- UNARY OPERATORS ----------------------------
    
        [ComOperator('-', 2, true)]
        public static double Negate(double a)
        {
            return -a;
        }

        [ComOperator('!', 5, true)]
        public static double Factorial(double a)
        {
            var facto = _gamma(a + 1);
            if (a == Math.Floor(a))
                return Math.Round(facto);
            return facto;
        }

        [ComOperator('~', 5, true)]
        public static double Inverse(double a)
        {
            return a > 0 ? 0 : 1;
        }


        // -------------------- FUNCTION LISTS --------------------------

        [ComFunction]
        public static double abs(double v)
        {
            return Math.Abs(v);
        }

        [ComFunction]
        public static double sign(double v)
        {
            return Math.Sign(v);
        }

        [ComFunction]
        public static double floor(double v)
        {
            return Math.Floor(v);
        }

        [ComFunction]
        public static double ceil(double v)
        {
            return Math.Floor(v) + 1;
        }

        [ComFunction]
        public static double round(double v)
        {
            return Math.Round(v);
        }

        [ComFunction]
        public static double sqr(double v)
        {
            return v * v;
        }

        [ComFunction]
        public static double sqrt(double v)
        {
            return Math.Sqrt(v);
        }

        [ComFunction]
        public static double log(double v)
        {
            return Math.Log10(v);
        }

        [ComFunction]
        public static double ln(double v)
        {
            return Math.Log(v);
        }

        [ComFunction]
        public static double inv(double v)
        {
            return 1.0 / v;
        }

        [ComFunction]
        public static double pow(double v)
        {
            return Math.Pow(10, v);
        }

        [ComFunction]
        public static double deg(double v)
        {
            return v * 360.0 / _dpi;
        }

        [ComFunction]
        public static double rad(double v)
        {
            return v / 360.0 * _dpi;
        }

        [ComFunction]
        public static double sin(double v)
        {
            return Math.Sin(v);
        }

        [ComFunction]
        public static double cos(double v)
        {
            return Math.Cos(v);
        }

        [ComFunction]
        public static double tan(double v)
        {
            return Math.Tan(v);
        }

        [ComFunction]
        public static double asin(double v)
        {
            return Math.Asin(v);
        }

        [ComFunction]
        public static double acos(double v)
        {
            return Math.Acos(v);
        }

        [ComFunction]
        public static double atan(double v)
        {
            return Math.Atan(v);
        }
        
        [ComFunction]
        public static double randint(double v)
        {
            return _randSingleton.Next(0, (int)v);
        }

        [ComFunction("gamma")]
        public static double _gamma(double alpha)
        {
            //https://www.experts-exchange.com/questions/27178124/Gamma-function-in-C.html
            double gamma = 0;
            if (alpha > 0)
            {
                if (alpha > 0 && alpha < 1)
                {
                    gamma = _gamma(alpha + 1) / alpha;
                }
                else if (alpha >= 1 && alpha <= 2)
                {
                    gamma = 1 - 0.577191652 * Math.Pow(alpha - 1, 1) + 0.988205891 * Math.Pow(alpha - 1, 2) -
                            0.897056937 * Math.Pow(alpha - 1, 3) + 0.918206857 * Math.Pow(alpha - 1, 4) -
                            0.756704078 * Math.Pow(alpha - 1, 5) + 0.482199394 * Math.Pow(alpha - 1, 6) -
                            0.193527818 * Math.Pow(alpha - 1, 7) + 0.03586843 * Math.Pow(alpha - 1, 8);
                }
                else
                {
                    gamma = (alpha - 1) * _gamma(alpha - 1);
                }
            }
            if (alpha > 171)
            {
                gamma = Math.Pow(10, 307);
            }
            return gamma;
        }

        [ComMultiFunction]
        public static double min(List<double> v)
        {
            double lowest = double.PositiveInfinity;
            for (int i = v.Count; i-- > 0;)
            {
                if (v[i] < lowest)
                    lowest = v[i];
            }
            return lowest;
        }

        [ComMultiFunction]
        public static double max(List<double> v)
        {
            double highest = double.NegativeInfinity;
            for (int i = v.Count; i-- > 0;)
            {
                if (v[i] < highest)
                    highest = v[i];
            }
            return highest;
        }

        [ComMultiFunction]
        public static double sel(List<double> v)
        {
            int sel = _randSingleton.Next(0, v.Count);
            return v[sel];
        }

        [ComMultiFunction]
        public static double lerp(List<double> v)
        {
            if (v.Count < 3)
                return 0;
            var iter = v[v.Count - 1];
            var count = v.Count - 2;
            if (iter > count)
                iter = count;
            else if (iter < 0)
                iter = 0;
            var frac = (iter - Math.Floor(iter));
            if (frac < 0.001 || frac > 0.999)
                return frac < 0.001 ? v[(int)Math.Floor(iter)] : v[(int)Math.Ceiling(iter)];
            var upper = v[(int)Math.Ceiling(iter)];
            var lower = v[(int)Math.Floor(iter)];
            return lower + (upper - lower) * frac;
        }


    

	// ------------------------- CONSTANT LISTS ----------------------------
    
        [ComConstant]
        public static double pi ()
        {
            return Math.PI;
        }

        [ComConstant]
        public static double dpi ()
        {
            return 2 * Math.PI;
        }

        public const double _dpi = 2 * Math.PI;

        [ComConstant]
        public static double euler()
        {
            return Math.E;
        }

        [ComConstant]
        public static double e()
        {
            return Math.E;
        }

        [ComConstant]
        public static double rand ()
        {
            return _randSingleton.NextDouble();
        }

        [ComConstant]
        public static double goldr()
        {
            return 1.61803398875;
        }

        [ComConstant("true")]
        public static double _true()
        {
            return 1;
        }

        [ComConstant("false")]
        public static double _false()
        {
            return 0;
        }
    }
} 