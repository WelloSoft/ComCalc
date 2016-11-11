
using System.Collections.Generic;
using System.Reflection;

namespace ComCalcLib
{
    internal class MultiFunctionAtom : Atom
    {
        public List<Atom> value = new List<Atom>();
        public string func;
        //static System.Type helper = typeof(MultiFunctionLibrary);
        const BindingFlags flag = BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod;

        public static MultiFunctionAtom Get(List<Atom> v, string f)
        {
            var atom = ObjPool<MultiFunctionAtom>.Get();
            atom.value.AddRange(v);
            atom.func = f;
            return atom;
        }

        public override void Flush()
        {
            parent = null;
            value.Clear();
            ObjPool<MultiFunctionAtom>.Release(this);
        }

        static List<double> s_pooledDigits = new List<double>(4);

        public override double Compute(CompEnvironment environment)
        {
            s_pooledDigits.Clear();
            for (int i = 0; i < value.Count; i++)
            {
                s_pooledDigits.Add(value[i].Compute(environment));
            }
            return CallReflection(func);
        }

        public static double CallReflection(string func)
        {
            return ComHelper.MultiFunctions[func](s_pooledDigits);
        }

        public override void DebugHierarchy(int indent)
        {
            base.DebugHierarchy(indent++);
            _hierarchySingleton.Append("Multi-Function : ");
            _hierarchySingleton.Append(value.Count);
            _hierarchySingleton.AppendLine();
            for (int i = 0; i < value.Count; i++)
            {
                value[i].DebugHelper(indent);
            }
        }

    }

}