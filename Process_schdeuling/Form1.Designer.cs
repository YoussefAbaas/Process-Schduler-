namespace Process_schdeuling
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
            this.algorithm = new System.Windows.Forms.ComboBox();
            this.process_num = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // algorithm
            // 
            this.algorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithm.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.algorithm.FormattingEnabled = true;
            this.algorithm.Items.AddRange(new object[] {
            "First come first served",
            "Sjf preemptive",
            "Sjf non preemptive",
            "Priority preemptive",
            "Priority non preemptive",
            "Round robin"});
            this.algorithm.Location = new System.Drawing.Point(418, 205);
            this.algorithm.Name = "algorithm";
            this.algorithm.Size = new System.Drawing.Size(259, 30);
            this.algorithm.TabIndex = 0;
            this.algorithm.SelectedIndexChanged += new System.EventHandler(this.algorithm_SelectedIndexChanged);
            // 
            // process_num
            // 
            this.process_num.Location = new System.Drawing.Point(119, 194);
            this.process_num.Multiline = true;
            this.process_num.Name = "process_num";
            this.process_num.Size = new System.Drawing.Size(119, 41);
            this.process_num.TabIndex = 1;
            this.process_num.TextChanged += new System.EventHandler(this.process_num_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cooper Black", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Coral;
            this.label1.Location = new System.Drawing.Point(412, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(341, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choose your algorithm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cooper Black", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Coral;
            this.label2.Location = new System.Drawing.Point(34, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(317, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enter no of processes";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Cooper Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(319, 313);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 66);
            this.button1.TabIndex = 6;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.process_num);
            this.Controls.Add(this.algorithm);
            this.Name = "Form1";
            this.Text = "Process_Schdeuling";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox algorithm;
        private System.Windows.Forms.TextBox process_num;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

