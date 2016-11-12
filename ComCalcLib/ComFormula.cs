#if (UNITY_5 || UNITY_4) && !UNITY
#define UNITY
using UnityEngine;
#endif
using System.Collections.Generic;
using System;


namespace ComCalcLib {
	[Serializable]
public class ComFormula
{

	CompEnvironment c_Environ;

	[NonSerialized]
	internal FormulaAtom rootFormula;

	public VariableDictionary variables;

#if UNITY
        [SerializeField]
#endif
        string c_Expression;
#if UNITY
        [SerializeField]
#endif
        double c_Value;
#if UNITY
        [SerializeField]
#endif
        string c_ErrorMessage;

	public string cachedExpression { get { return c_Expression; } }
	public double cachedValue { get { return c_Value; } }
	public string cachedErrorMessage { get { return c_ErrorMessage; } }
	const double errorValue = double.NaN;

	public ComFormula ()
	{
		variables = new VariableDictionary();
		ComHelper.checkAndReload ();
	}

	protected virtual void Reparse ()
	{ 
		if(rootFormula != null)
			rootFormula.Flush();
		try {
			rootFormula = ComFormulaParser.Parse (c_Expression);
			c_ErrorMessage = null;
		} catch (Exception ex) {
			c_ErrorMessage = ex.Message;
		}
		c_Value = double.NaN;
	}

	public virtual void Parse (string expression)
	{
		c_Expression = expression;
		Reparse();
	}

	public virtual double Compute ()
	{
		if(c_Environ == null)
			c_Environ = new CompEnvironment(variables);
		if(rootFormula == null)
			Reparse();
            if (c_ErrorMessage != null)
                return errorValue;
		try {
			c_Value = rootFormula.Compute (c_Environ);
			c_ErrorMessage = null;
			return c_Value;
		} catch (Exception ex) {
			c_ErrorMessage = ex.Message;
			return errorValue;
		}
	}

        public string TracedPath 
        {
            get
            {
                Atom._hierarchySingleton.Length = 0;
                rootFormula.DebugHelper(0);
                return Atom._hierarchySingleton.ToString();
            }
        }
}

}