
using System.Collections;
using System;


public class ComCalcException : Exception
{
	internal ComCalcException(string message)
		: base(message)
	{
	}
	
	internal ComCalcException(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	static public ComCalcException e_DivByZero = new ComCalcException ("Division by zero occurs!");
	static public ComCalcException e_ModByZero = new ComCalcException ("Modulo Divisor can't be zero!");
	static public ComCalcException e_PowImaginer = new ComCalcException ("Exponential base can't be negative while degree are double number!");
	static public ComCalcException e_FactByMinus = new ComCalcException ("Factorial base can't be negative!");
	static public ComCalcException e_NoMatchVariable = new ComCalcException ("One or more variable can't be found");


	static public ComCalcException p_OpByOp = new ComCalcException ("Operator meets with Operator!");
	static public ComCalcException p_PrematureClose = new ComCalcException ("closing group without an open group character. '(' expected");
	static public ComCalcException p_NoCharDefined = new ComCalcException ("Character is not defined : ??");
	static public ComCalcException p_MissOpenBrack = new ComCalcException ("missing '('!");
	static public ComCalcException p_MissCloseBrack = new ComCalcException ("Illegal end,  missing ')'!");
	static public ComCalcException d_KeyNotFound = new ComCalcException ("Key doesn't found");
}

