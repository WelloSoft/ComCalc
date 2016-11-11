using System.Collections;

namespace ComCalcLib {
	internal class DigitAtom : Atom
	{
	
		public static DigitAtom Get (double n)
		{
			var atom = ObjPool<DigitAtom>.Get();
			atom.Number = n;
			return atom;
		}
		
		public override void Flush()
		{
			parent = null;
			ObjPool<DigitAtom>.Release(this);
		}
		
		public double Number;
	
		public override double Compute (CompEnvironment environment)
		{
			return Number;
		}

       
        public override void DebugHierarchy(int indent)
        {
            base.DebugHierarchy(indent);
            _hierarchySingleton.Append("Digit : ");
            _hierarchySingleton.Append(Number);
            _hierarchySingleton.AppendLine();
        }
    }
}
