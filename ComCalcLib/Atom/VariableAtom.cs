
namespace ComCalcLib
{
    internal class VariableAtom : Atom
    {

        public string variable;

        public static VariableAtom Get(string name)
        {
            var atom = ObjPool<VariableAtom>.Get();
            atom.variable = name;
            return atom;
        }

        public override void Flush()
        {
            parent = null;
            ObjPool<VariableAtom>.Release(this);
        }


        public override double Compute(CompEnvironment environment)
        {
            if (environment.variables != null && environment.variables.ContainsKey(variable))
                return environment.variables[variable];
            else if (!double.IsNaN(ComHelper.OverrideNullVariables))
            {
                environment.variables[variable] = ComHelper.OverrideNullVariables;
                return ComHelper.OverrideNullVariables;
            }
            else
                throw ComCalcException.e_NoMatchVariable;
        }


        public override void DebugHierarchy(int indent)
        {
            base.DebugHierarchy(indent++);
            _hierarchySingleton.Append("Variable : ");
            _hierarchySingleton.Append(variable);
            _hierarchySingleton.AppendLine();
          
        }

    }
}