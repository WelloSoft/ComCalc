using System.Collections.Generic;
using System.Reflection;
using System;

namespace ComCalcLib
{
    public static class ComHelper
    {
        static internal Dictionary<string, Func<double>> Constant = new Dictionary<string, Func<double>>();
        static internal Dictionary<char, string> OperatorBinarys = new Dictionary<char, string>();
        static internal Dictionary<char, string> OperatorUnarys = new Dictionary<char, string>();
        static internal Dictionary<string, string> OperatorBinaryToUnary = new Dictionary<string, string>();
        static internal Dictionary<string, int> OperatorLevels = new Dictionary<string, int>();
        static internal Dictionary<string, Func<double, double>> Functions = new Dictionary<string, Func<double, double>>();
        static internal Dictionary<string, Func<double, double, double>> BinaryOperands = new Dictionary<string, Func<double, double, double>>();
        static internal Dictionary<string, Func<double, double>> UnaryOperands = new Dictionary<string, Func<double, double>>();
        static internal Dictionary<string, Func<List<double>, double>> MultiFunctions = new Dictionary<string, Func<List<double>, double>>();
        static internal Dictionary<string, WeakReference> CachedExpressions = new Dictionary<string, WeakReference>();
        static bool hasInitDefault = false;

        public static double OverrideNullVariables = double.NaN;
        public static double DefaultErrorValue = double.NaN;
        public static bool UseOneCharForVariableNames = false;
        public static bool EnableCaching = true;




        public static void checkAndReload()
        {
            if (!hasInitDefault) 
                ClearAndLoadDefaultLibrary();
        }

        const BindingFlags flag = BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod;


        public static void LoadLibrary(Type lib)
        {
            if (!hasInitDefault)
                ClearAndLoadDefaultLibrary();

            
            var Methods = lib.GetMethods(flag);

            for (int i = 0; i < Methods.Length; i++)
            {
                var meth = Methods[i];
                var atts = meth.GetCustomAttributes(true);
                if (atts.Length == 0)
                    continue;
                var att = atts[0];
                if (att is ComOperatorAttribute)
                {
                    var a = (ComOperatorAttribute)att;
                    OperatorLevels[meth.Name] = a.level;
                    if (!a.isUnary)
                    {
                        OperatorBinarys[a.symbol] = meth.Name;
                        BinaryOperands[meth.Name] = (Func<double, double, double>)Delegate.CreateDelegate(typeof(Func<double, double, double>), meth);
                        if (OperatorUnarys.ContainsKey(a.symbol))
                            OperatorBinaryToUnary[meth.Name] = OperatorUnarys[a.symbol];
                    }
                    else
                    {
                        OperatorUnarys[a.symbol] = meth.Name;
                        UnaryOperands[meth.Name] = (Func<double, double>)Delegate.CreateDelegate(typeof(Func<double, double>), meth);
                        if (OperatorBinarys.ContainsKey(a.symbol))
                            OperatorBinaryToUnary[OperatorBinarys[a.symbol]] = meth.Name;
                    }
                }
                else if (att is ComFunctionAttribute)
                {
                    var a = (ComFunctionAttribute)att;
                    Functions[string.IsNullOrEmpty(a.altName) ? meth.Name : a.altName] = (Func<double, double>)Delegate.CreateDelegate(typeof(Func<double, double>), meth);
                }
                else if (att is ComMultiFunctionAttribute)
                {
                    var a = (ComMultiFunctionAttribute)att;
                    MultiFunctions[string.IsNullOrEmpty(a.altName) ? meth.Name : a.altName] = (Func<List<double>, double>)Delegate.CreateDelegate(typeof(Func<List<double>, double>), meth);
                }
                else if (att is ComConstantAttribute)
                {
                    var a = (ComConstantAttribute)att;
                    Constant[string.IsNullOrEmpty(a.altName) ? meth.Name : a.altName] = (Func<double>)Delegate.CreateDelegate(typeof(Func<double>), meth);
                } 
                
            }
        }

        public static void ClearAndLoadDefaultLibrary()
        {
            Constant.Clear();
            BinaryOperands.Clear();
            OperatorLevels.Clear();
            OperatorBinarys.Clear();
            Functions.Clear();
            MultiFunctions.Clear();
            hasInitDefault = true;
            LoadLibrary(typeof(DefaultComLibrary));
        }

        internal static bool compareOpLevel(string a, string b)
        {
            return OperatorLevels[a] > OperatorLevels[b];
        }



        public static ComFormula ParseExpression(this string s)
        {
            checkAndReload();
            var r = new ComFormula();
            r.Parse(s);
            return r;
        }


        public static double Evaluate(this string s)
        {
            checkAndReload();
            var r = new ComFormula();
            r.Parse(s);
            return r.Compute();
        }

        public static double Evaluate(this string s, double x)
        {
            checkAndReload();
            var r = new ComFormula();
            r.Parse(s);
            r.variables["x"] = x;
            return r.Compute();
        }

        public static double Evaluate(this string s, double x, double y)
        {
            checkAndReload();
            var r = new ComFormula();
            r.Parse(s);
            r.variables["x"] = x;
            r.variables["y"] = y;
            return r.Compute();
        }

        public static double Evaluate(this string s, double x, double y, double z)
        {
            checkAndReload();
            var r = new ComFormula();
            r.Parse(s);
            r.variables["x"] = x;
            r.variables["y"] = y;
            r.variables["z"] = z;
            return r.Compute();
        }

        internal static void DebugHelper (this Atom atom, int indent)
        {
            if (atom == null)
            {
                for (int i = indent; i --> 0;)
                {
                    Atom._hierarchySingleton.Append("|\t");
                }
                Atom._hierarchySingleton.Append("<null>");
             } else
            atom.DebugHierarchy(indent);
        }

        public static void ClearCaches ()
        {
            foreach (var item in CachedExpressions)
            {
                CachedExpressions.Remove(item.Key);
            }
        }

        internal static void RegisterToCache (string exp, FormulaAtom rootAtom)
        {
            if (!EnableCaching)
                return;
            if (CachedExpressions.ContainsKey(exp) && !CachedExpressions[exp].IsAlive)
                CachedExpressions[exp].Target = rootAtom;
            else
                CachedExpressions[exp] = new WeakReference(rootAtom);
        }
        
        internal static FormulaAtom PopFromCache (string exp)
        {
            if (!EnableCaching)
                return null;
            if (CachedExpressions.ContainsKey(exp) && CachedExpressions[exp].IsAlive)
                return (FormulaAtom)CachedExpressions[exp].Target;
            return null;
        }

    }
}