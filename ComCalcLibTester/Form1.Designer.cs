namespace ComCalcLibTester
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Input = new System.Windows.Forms.TextBox();
            this.Traced = new System.Windows.Forms.TextBox();
            this.Output = new System.Windows.Forms.Label();
            this.VarProp = new System.Windows.Forms.PropertyGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.XMax = new System.Windows.Forms.NumericUpDown();
            this.XMin = new System.Windows.Forms.NumericUpDown();
            this.X = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.X)).BeginInit();
            this.SuspendLayout();
            // 
            // Input
            // 
            this.Input.Dock = System.Windows.Forms.DockStyle.Top;
            this.Input.Font = new System.Drawing.Font("Consolas", 13F);
            this.Input.Location = new System.Drawing.Point(0, 0);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(618, 28);
            this.Input.TabIndex = 0;
            this.Input.TextChanged += new System.EventHandler(this.Input_TextChanged);
            // 
            // Traced
            // 
            this.Traced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Traced.Location = new System.Drawing.Point(0, 0);
            this.Traced.Multiline = true;
            this.Traced.Name = "Traced";
            this.Traced.ReadOnly = true;
            this.Traced.Size = new System.Drawing.Size(285, 336);
            this.Traced.TabIndex = 1;
            this.Traced.WordWrap = false;
            // 
            // Output
            // 
            this.Output.Dock = System.Windows.Forms.DockStyle.Top;
            this.Output.Font = new System.Drawing.Font("Consolas", 11F);
            this.Output.Location = new System.Drawing.Point(0, 0);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(329, 28);
            this.Output.TabIndex = 2;
            this.Output.Text = "label1";
            this.Output.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VarProp
            // 
            this.VarProp.CommandsVisibleIfAvailable = false;
            this.VarProp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VarProp.HelpVisible = false;
            this.VarProp.Location = new System.Drawing.Point(0, 28);
            this.VarProp.Name = "VarProp";
            this.VarProp.Size = new System.Drawing.Size(329, 249);
            this.VarProp.TabIndex = 3;
            this.VarProp.ToolbarVisible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Traced);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.VarProp);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.Output);
            this.splitContainer1.Size = new System.Drawing.Size(618, 336);
            this.splitContainer1.SplitterDistance = 285;
            this.splitContainer1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.XMax);
            this.panel1.Controls.Add(this.XMin);
            this.panel1.Controls.Add(this.X);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 277);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(329, 59);
            this.panel1.TabIndex = 5;
            // 
            // XMax
            // 
            this.XMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.XMax.DecimalPlaces = 1;
            this.XMax.Location = new System.Drawing.Point(216, 0);
            this.XMax.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.XMax.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.XMax.Name = "XMax";
            this.XMax.Size = new System.Drawing.Size(113, 20);
            this.XMax.TabIndex = 6;
            this.XMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.XMax.ValueChanged += new System.EventHandler(this.X_ValueChanged);
            // 
            // XMin
            // 
            this.XMin.DecimalPlaces = 1;
            this.XMin.Location = new System.Drawing.Point(0, 0);
            this.XMin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.XMin.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.XMin.Name = "XMin";
            this.XMin.Size = new System.Drawing.Size(120, 20);
            this.XMin.TabIndex = 5;
            this.XMin.ValueChanged += new System.EventHandler(this.X_ValueChanged);
            // 
            // X
            // 
            this.X.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.X.Location = new System.Drawing.Point(1, 22);
            this.X.Maximum = 100;
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(326, 45);
            this.X.TabIndex = 4;
            this.X.ValueChanged += new System.EventHandler(this.X_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(118, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "\'X\' Variable";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 364);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.Input);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.X)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.TextBox Traced;
        private System.Windows.Forms.Label Output;
        private System.Windows.Forms.PropertyGrid VarProp;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown XMax;
        private System.Windows.Forms.NumericUpDown XMin;
        private System.Windows.Forms.TrackBar X;
        private System.Windows.Forms.Label label1;
    }
}

