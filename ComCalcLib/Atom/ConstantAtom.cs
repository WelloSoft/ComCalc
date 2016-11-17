

namespace ComCalcLib
{
    internal class ConstantAtom : Atom
    {

        public string func;

        public static ConstantAtom Get(string f)
        {
            var atom = ObjPool<ConstantAtom>.Get();
            atom.func = f;
            return atom;
        }

        public override void Flush()
        {
            ObjPool<ConstantAtom>.Release(this);
        }

        public override double Compute(CompEnvironment environment)
        {
            return ComHelper.Constant[func]();
        }

        public override void DebugHierarchy(int indent)
        {
            base.DebugHierarchy(indent++);
            _hierarchySingleton.Append("Constant : ");
            _hierarchySingleton.Append(func);
            _hierarchySingleton.AppendLine();
        }

        public override string ToString()
        {
            return base.ToString() + ": " + func;
        }

    }
}