using System.Collections;
using System.Reflection;
using System.Linq;
using System.Text;
using System;

namespace ComCalcLib {
	internal class FunctionAtom : Atom
	{

		public Atom value;
		public string func;
	
		public static FunctionAtom Get(Atom v, string f)
		{
			var atom = ObjPool<FunctionAtom>.Get();
			atom.value = v;
			atom.func = f;
			return atom;
		}
		
		public override void Flush()
		{
			ObjPool<FunctionAtom>.Release(this);
		}
	
		public override double Compute (CompEnvironment environment)
		{
            if (value == null)
                return ComHelper.Functions [func](0);
            return ComHelper.Functions[func] (value.Compute (environment));
		}

        public override string ToString()
        {
            return base.ToString() + ": " + func;
        }

        public override void DebugHierarchy(int indent)
        {
            base.DebugHierarchy(indent++);
            _hierarchySingleton.Append("Function : ");
            _hierarchySingleton.Append(func);
            _hierarchySingleton.AppendLine();
            value.DebugHelper(indent);
        }

    }
}