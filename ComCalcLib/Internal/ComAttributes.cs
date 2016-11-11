using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComCalcLib
{
    [System.AttributeUsage(AttributeTargets.Method)]
    public class ComOperatorAttribute : Attribute
    {
        public char symbol;
        public int level;
        public bool isUnary;
       
        public ComOperatorAttribute(char Symbol, int Level, bool IsUnary = false)
        {
            symbol = Symbol;
            level = Level;
            isUnary = IsUnary;
        }

    }

    [System.AttributeUsage(AttributeTargets.Method)]
    public class ComFunctionAttribute : Attribute
    {
        public string altName;

        public ComFunctionAttribute(string AltName = "")
        {
            altName = AltName;
        }

   }

    [System.AttributeUsage(AttributeTargets.Method)]
    public class ComMultiFunctionAttribute : Attribute
    {
        public string altName;

        public ComMultiFunctionAttribute(string AltName = "")
        {
            altName = AltName;
        }

    }

    [System.AttributeUsage(AttributeTargets.Method)]
    public class ComConstantAttribute : Attribute
    {
        public string altName;

        public ComConstantAttribute(string AltName = "")
        {
            altName = AltName;
        }

    }
}
