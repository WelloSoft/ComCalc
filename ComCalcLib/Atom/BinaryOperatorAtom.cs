
namespace ComCalcLib
{
    internal abstract class OperatorAtom : Atom
    {
        public string func;

        public override string ToString()
        {
            return base.ToString () + ": " + func;
        }
    }
    internal class BinaryOperatorAtom : OperatorAtom
    {

        public Atom left;
        public Atom right;
       
        public static BinaryOperatorAtom Get(Atom l, Atom r, string f)
        {
            var atom = ObjPool<BinaryOperatorAtom>.Get();
            atom.left = l;
            atom.right = r;
            atom.func = f;
            return atom;
        }

        public override void Flush()
        {
            if (left != null)
                left.Flush();
            if (right != null)
                right.Flush();
            left = null;
            right = null;
            parent = null;
            ObjPool<BinaryOperatorAtom>.Release(this);
        }

        public bool TrySwitchToUnary (ref Atom thisInstance)
        {
            if (!ComHelper.OperatorBinaryToUnary.ContainsKey(func))
                return false;
            var unary = UnaryOperatorAtom.Get(left, ComHelper.OperatorBinaryToUnary[func]);
            thisInstance = unary;
            left = null;
            Flush(); 
            return true;
        }

        public override double Compute(CompEnvironment environment)
        {
            var r = right != null ? right.Compute(environment) : 0.0;
            return ComHelper.BinaryOperands[func](left.Compute(environment), r);
        }


        public override void DebugHierarchy(int indent)
        {
            base.DebugHierarchy(indent++);
            _hierarchySingleton.Append("Binary Op : ");
            _hierarchySingleton.Append(func);
            _hierarchySingleton.AppendLine();
            left.DebugHelper(indent);
            right.DebugHelper(indent);
        }
    }
}