
namespace ComCalcLib
{
    internal class UnaryOperatorAtom : OperatorAtom
    {

        public Atom value;
        
        public static UnaryOperatorAtom Get(Atom v, string f)
        {
            var atom = ObjPool<UnaryOperatorAtom>.Get();
            atom.value = v;
            atom.func = f;
            return atom;
        }

        public override void Flush()
        {
            if (value != null)
                value.Flush();
            value = null;
            parent = null;
            ObjPool<UnaryOperatorAtom>.Release(this);
        }

        public override double Compute(CompEnvironment environment)
        {
            if (value == null)
                return ComHelper.UnaryOperands[func](0);
            return ComHelper.UnaryOperands[func](value.Compute(environment));
        }


        public override void DebugHierarchy(int indent)
        {
            base.DebugHierarchy(indent++);
            _hierarchySingleton.Append("Unary Op : ");
            _hierarchySingleton.Append(func);
            _hierarchySingleton.AppendLine();
            value.DebugHelper(indent);
        }

    }
}