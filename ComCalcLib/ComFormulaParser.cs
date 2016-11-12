
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ComCalcLib
{
    public class ComFormulaParser
    {
        private const char groupLeft = '(';
        private const char groupRight = ')';

        static List<Atom> s_pooledParam = new List<Atom>();
        internal static FormulaAtom Parse(string value)
        {
            var cache = ComHelper.PopFromCache(value);
            if (cache != null)
                return cache;
            var formula = FormulaAtom.Get();
            var position = 0;
            while (position < value.Length)
            {
                char ch = value[position];
                if (IsWhiteSpace(ch))
                {
                    position++;
                }
                else if (char.IsDigit(ch) || ch == '.')
                {
                    formula.Add(DigitAtom.Get(ProcessDigit(formula, value, ref position)));
                }
                else if (char.IsLetter(ch))
                {
                    string n = ProcessLetter(formula, value, ref position);
                    SkipWhiteSpace(value, ref position);
                    if (ComHelper.Functions.ContainsKey(n) && value[position] == groupLeft)
                    {
                        formula.Add(FunctionAtom.Get(Parse(ReadGroup( value, ref position)).RootAtom, n));
                    }
                    else if (ComHelper.MultiFunctions.ContainsKey(n) && value[position] == groupLeft)
                    {
                        s_pooledParam.Clear();
                        string lastPool;
                        do
                        {
                            lastPool = StepOverParam(value, ref position);
                            if (lastPool != null)
                                s_pooledParam.Add(Parse(lastPool));
                            else
                                break;
                        } while (true);
                        if (s_pooledParam.Count > 0)
                            formula.Add(MultiFunctionAtom.Get(s_pooledParam, n));
                    }
                    else if (ComHelper.Constant.ContainsKey(n))
                    {
                        formula.Add(ConstantAtom.Get(n));
                    }
                    else if (ComHelper.UseOneCharForVariableNames)
                    {
                        for (int i = 0; i < n.Length; i++)
                        {
                            formula.Add(VariableAtom.Get(new string(n[i], 1)));
                        }
                    }
                    else
                    {
                        formula.Add(VariableAtom.Get(n));
                    }
                }
                else if (ComHelper.OperatorBinarys.ContainsKey(ch))
                {
                    formula.Add(BinaryOperatorAtom.Get(null, null, ComHelper.OperatorBinarys[ch]));
                    position++;
                }
                else if (ComHelper.OperatorUnarys.ContainsKey(ch))
                {
                    formula.Add(UnaryOperatorAtom.Get(null, ComHelper.OperatorUnarys[ch]));
                    position++;
                }
                else if (ch == groupLeft)
                {
                    formula.Add(Parse(ReadGroup(value, ref position)));
                }
                else if (ch == groupRight)
                {
                    throw ComCalcException.p_PrematureClose;
                }
                else
    				throw ComCalcException.p_NoCharDefined;
            }
            ComHelper.RegisterToCache(value, formula);
            return formula;
        }

        private static string ReadGroup(string value, ref int position)
        {
            var start = position + 1;

            StepOverGroup(value, ref position);

            return value.Substring(start, position - 1 - start);
        }

        private static bool StepOverGroup(string value, ref int position, char openChar = groupLeft, char closeChar = groupRight)
        {
            if (position == value.Length || value[position] != openChar)
                return false;

            //var result = new StringBuilder ();
            var group = 0;
            position++;
//            var start = position;
            while (position < value.Length && !(value[position] == closeChar && group == 0))
            {
                if (value[position] == openChar)
                    group++;
                else if (value[position] == closeChar)
                    group--;
                //		result.Append (value [position]);
                position++;
            }

            if (position == value.Length)
            {
                // Reached end of formula but group has not been closed.
                return false;
            }
            position++;
            return true;
        }

        private static string StepOverParam(string value, ref int position)
        {
        	if (position >= value.Length || value[position] == groupRight) {
        		position++;
        		return null;
        	}
            int start = position;
                position++;
            while (position < value.Length && value[position] != ',')
            {
                if (value[position] == groupLeft)
                    StepOverGroup(value, ref position);
                else if (value[position] == groupRight)
                {
                	if (position - 1 == start)
	                	return null;
                    return value.Substring(start + 1, position - start - 1);
                }
                position++;
            }
            if (position < value.Length)
                return value.Substring(start + 1, position - start - 1);

            return null;
        }

        private static double ProcessDigit(FormulaAtom formula, string value, ref int position)
        {
             var start = position;
            while (position < value.Length)
            {
                var ch = value[position];
                position++;
                if (position == value.Length)
                    break;
                ch = value[position];
                if (!(char.IsDigit(ch) || ch == '.'))
                {
                    break;
                }
            }

            return double.Parse(value.Substring(start, position - start), invariant);
        }


        static CultureInfo invariant = CultureInfo.InvariantCulture;

        private static string ProcessLetter(FormulaAtom formula, string value, ref int position)
        {
            var result = new StringBuilder();
            while (position < value.Length)
            {
                var ch = value[position];
                result.Append(ch);
                position++;
                if (position == value.Length)
                    break;
                ch = value[position];
                if (!char.IsLetter(ch))
                {
                    break;
                }
            }

            return result.ToString();
        }

        private static bool IsWhiteSpace(char ch)
        {
            return ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r';
        }

        private static bool IsSymbol(char c)
        {
            return !((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));
        }

        private static void SkipWhiteSpace(string value, ref int position)
        {
            while (position < value.Length && IsWhiteSpace(value[position]))
                position++;
        }
    }
}