using System.Text;
using System.Collections;

namespace ComCalcLib {
    internal abstract class Atom : IFlushable
    {

        public Atom parent
        {
            get;
            set;
        }

        public virtual double Compute(CompEnvironment environment)
        {
            return 0.0;
        }

        ///used Internally, check whether it's already released or not
        ///Public for convience, you shouldn't set this manually
        bool m_flushed = false;
        public bool IsFlushed { get { return m_flushed; } set { m_flushed = value; } }

        abstract public void Flush();

        internal static StringBuilder _hierarchySingleton = new StringBuilder(1024);

        public virtual void DebugHierarchy(int indent)
        {
            for (int i = indent; i --> 0;)
            {
                _hierarchySingleton.Append("|\t");
            }
            //_hierarchySingleton.AppendLine(ToString());
        }
    }
}