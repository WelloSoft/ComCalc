using System.Text;
using System.Collections.Generic;

namespace ComCalcLib {
	internal class FormulaAtom : Atom
	{
		public static FormulaAtom Get (FormulaAtom formula)
		{
			var atom = ObjPool<FormulaAtom>.Get ();
			
			atom.Add (formula);
			
			return atom;
		}
	
		public static FormulaAtom Get ()
		{
			return ObjPool<FormulaAtom>.Get ();
		}
	
		public override void Flush ()
		{
			if (RootAtom != null)
				RootAtom.Flush ();
			RootAtom = null;
			parent = null;
			ObjPool<FormulaAtom>.Release (this);
		}
	
		internal Atom RootAtom;
	
		Atom _last;
	
		public override double Compute (CompEnvironment environment)
		{
			return RootAtom != null ? RootAtom.Compute (environment) : 0f;
		}
	
		void replaceDigitAtom (Atom atom, Atom lastAtom, Atom newAtom)
		{
			atom.parent = newAtom;
			//Replace last Atom
			if (lastAtom.parent != null) {
				newAtom.parent = lastAtom.parent;
				if (lastAtom.parent is BinaryOperatorAtom)
					((BinaryOperatorAtom)lastAtom.parent).right = newAtom;
				else if (lastAtom.parent is FunctionAtom)
					((FunctionAtom)lastAtom.parent).value = newAtom;
			} else {
				RootAtom = newAtom;
			}
			//Fill the new Atom
			if (newAtom is BinaryOperatorAtom) {
				((BinaryOperatorAtom)newAtom).left = lastAtom;
				((BinaryOperatorAtom)newAtom).right = atom;
			}
			lastAtom.parent = newAtom;
		}

        /// <summary>
        /// Input lastAtom to right side of atom, so ... 
        /// (3+2)*.. => (3+(2*..))
        /// -(5)^.. => (-(5^..))
        /// -()!.. => (-(!..))
        /// sin().. => sin(..)
        /// </summary>
        /// <param name="atom"></param>
        /// <param name="lastAtom"></param>
        void replaceOpAtomR (Atom atom, Atom lastAtom)
		{
			//Replace last Atom
			if (lastAtom.parent != null) {
				atom.parent = lastAtom.parent;
				if (lastAtom.parent is BinaryOperatorAtom)
					((BinaryOperatorAtom)lastAtom.parent).right = atom;
                if (lastAtom.parent is UnaryOperatorAtom)
                    ((UnaryOperatorAtom)lastAtom.parent).value = atom;
                else if (lastAtom.parent is FunctionAtom)
					((FunctionAtom)lastAtom.parent).value = atom;
			} else {
				RootAtom = atom;
			}
			//Fill the new Atom
			if (atom is BinaryOperatorAtom) {
				((BinaryOperatorAtom)atom).left = lastAtom;
			}
			lastAtom.parent = atom;
		}

        /// <summary>
        /// Input lastAtom to left side of atom, so ...
        /// (3*2)+.. => ((3*2)+..)
        /// -(5)*.. => ((-5)*..)
        /// </summary>
        void replaceOpAtomL (Atom atom, Atom lastAtom)
		{
			//Replace last Atom
			if (lastAtom.parent != null) {
				Atom mParent = lastAtom.parent.parent;
				if (mParent != null) {
					atom.parent = mParent;
					if (mParent is BinaryOperatorAtom)
						((BinaryOperatorAtom)mParent).right = atom;
					else if (mParent is FunctionAtom)
						((FunctionAtom)mParent).value = atom;
				} else {
					RootAtom = atom;
				}
				//Fill the new Atom
				if (atom is BinaryOperatorAtom) {
					((BinaryOperatorAtom)atom).left = lastAtom.parent;
				}
				lastAtom.parent.parent = atom;
			} else {
				RootAtom = atom;
                if (atom is BinaryOperatorAtom)
				((BinaryOperatorAtom)atom).left = lastAtom;
                else if (atom is UnaryOperatorAtom)
                    ((UnaryOperatorAtom)atom).value = lastAtom;
            }
        }
	
		internal void Add (Atom atom)
		{
			Add (atom, true);
		}
	
	
	
		internal void Add (Atom atom, bool Mark)
		{
			if (RootAtom == null) {
                if (atom is BinaryOperatorAtom)
                    ((BinaryOperatorAtom)atom).TrySwitchToUnary(ref atom);
                RootAtom = atom;
				//Check if the operator is negative prefix, if yes, add zero 
				/*if (atom is BinaryOperatorAtom && ((BinaryOperatorAtom)atom).func == "Subtract") {
					((BinaryOperatorAtom)atom).left = DigitAtom.Get (0);
					((BinaryOperatorAtom)atom).func = "Negate";
				}*/
               
				_last = atom;
			} else {
				Atom lastAtom = _last;
				if (atom is OperatorAtom) {
                    var opAtom = ((OperatorAtom)atom);
                    if (lastAtom is OperatorAtom) {
                        //// Operator <-> Operator
                        if (opAtom is UnaryOperatorAtom || ((BinaryOperatorAtom)opAtom).TrySwitchToUnary(ref atom))
                        {
//                            var newOpAtom = (UnaryOperatorAtom)atom;
                           // if (ComHelper.compareOpLevel(newOpAtom.func, ((OperatorAtom)lastAtom).func))
                            {
                                if (lastAtom is BinaryOperatorAtom)
                                {
                                    ((BinaryOperatorAtom)lastAtom).right = atom;
                                    atom.parent = lastAtom;
                                }
                                else if (lastAtom is UnaryOperatorAtom)
                                {
                                    ((UnaryOperatorAtom)lastAtom).value = atom;
                                    atom.parent = lastAtom;
                                }
                            }
                           // else
                             //   replaceOpAtomL(atom, lastAtom);
                        }
                        else
                            throw ComCalcException.p_OpByOp;
					} else {
                        //// Non Operator <-> Operator
                        bool hasParented = false;
                        while (lastAtom.parent is OperatorAtom)
                        {
                            if (ComHelper.compareOpLevel(opAtom.func, ((OperatorAtom)lastAtom.parent).func))
                            {
                                replaceOpAtomR(atom, lastAtom);
                                hasParented = true;
                                break;
                            }
                            else
                                lastAtom = lastAtom.parent;
                        }
                       if (!hasParented)
                            replaceOpAtomL(atom, lastAtom);
                    }

                } else {
					if (lastAtom is OperatorAtom) {
						if (lastAtom is BinaryOperatorAtom) {
							((BinaryOperatorAtom)lastAtom).right = atom;
							atom.parent = lastAtom;
						} else if (lastAtom is UnaryOperatorAtom)
                        {
                            ((UnaryOperatorAtom)lastAtom).value = atom;
                            atom.parent = lastAtom;
                        }
                        else
                        replaceDigitAtom (atom, lastAtom, BinaryOperatorAtom.Get (lastAtom, atom, "Multiply"));
					//	if (((BinaryOperatorAtom)lastAtom).func == "Negate")
						//	atom = _last;
					} else
						replaceDigitAtom (atom, lastAtom, BinaryOperatorAtom.Get (lastAtom, atom, "Multiply"));
				}
			}
			if (Mark)
				_last = atom;
		}

        public override void DebugHierarchy(int indent)
        {
            base.DebugHierarchy(indent++);
            _hierarchySingleton.Append("Formula");
            _hierarchySingleton.AppendLine();
            RootAtom.DebugHelper(indent);
        }

    }
}
