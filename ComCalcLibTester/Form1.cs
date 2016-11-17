using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComCalcLib;

namespace ComCalcLibTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public ComFormula formula;
        public double xx  = 0;
        private void Input_TextChanged(object sender, EventArgs e)
        {
            formula = Input.Text.ParseExpression();
            formula.variables["x"] = xx;
            Output.Text = formula.Compute().ToString("R");
            Traced.Text = formula.TracedPath.Replace("\t", "  ");
            VarProp.SelectedObject = formula;
        }

        private void X_ValueChanged(object sender, EventArgs e)
        {
            xx = (double)(XMin.Value + (XMax.Value - XMin.Value) * X.Value / 100.0m);
            Input_TextChanged(sender, e);
        }

        static double HelloWorld ()
        {
            // return 5 (double)
            return "x^2+8x+sin(pi/6)".Evaluate(0.5);
        }

    }
}
