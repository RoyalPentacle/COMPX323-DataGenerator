namespace COMPX323_Generator
{
    partial class Form_DataGenerator
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_RandomSeed = new System.Windows.Forms.TextBox();
            this.button_Generate = new System.Windows.Forms.Button();
            this.radioButton_SmallDataset = new System.Windows.Forms.RadioButton();
            this.radioButton_LargeDataset = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_DataSource = new System.Windows.Forms.TextBox();
            this.textBox_Username = new System.Windows.Forms.TextBox();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton_MongoDB = new System.Windows.Forms.RadioButton();
            this.radioButton_Oracle = new System.Windows.Forms.RadioButton();
            this.progressBar_Generation = new System.Windows.Forms.ProgressBar();
            this.formDataGeneratorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label_CurrentStep = new System.Windows.Forms.Label();
            this.timer_progressUpdate = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.formDataGeneratorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Random Seed:";
            // 
            // textBox_RandomSeed
            // 
            this.textBox_RandomSeed.Location = new System.Drawing.Point(134, 144);
            this.textBox_RandomSeed.Name = "textBox_RandomSeed";
            this.textBox_RandomSeed.Size = new System.Drawing.Size(280, 26);
            this.textBox_RandomSeed.TabIndex = 2;
            // 
            // button_Generate
            // 
            this.button_Generate.Location = new System.Drawing.Point(322, 176);
            this.button_Generate.Name = "button_Generate";
            this.button_Generate.Size = new System.Drawing.Size(92, 38);
            this.button_Generate.TabIndex = 3;
            this.button_Generate.Text = "Generate";
            this.button_Generate.UseVisualStyleBackColor = true;
            this.button_Generate.Click += new System.EventHandler(this.button_Generate_Click);
            // 
            // radioButton_SmallDataset
            // 
            this.radioButton_SmallDataset.AutoSize = true;
            this.radioButton_SmallDataset.Checked = true;
            this.radioButton_SmallDataset.Location = new System.Drawing.Point(13, 183);
            this.radioButton_SmallDataset.Name = "radioButton_SmallDataset";
            this.radioButton_SmallDataset.Size = new System.Drawing.Size(134, 24);
            this.radioButton_SmallDataset.TabIndex = 5;
            this.radioButton_SmallDataset.TabStop = true;
            this.radioButton_SmallDataset.Text = "Small Dataset";
            this.radioButton_SmallDataset.UseVisualStyleBackColor = true;
            // 
            // radioButton_LargeDataset
            // 
            this.radioButton_LargeDataset.AutoSize = true;
            this.radioButton_LargeDataset.Location = new System.Drawing.Point(153, 183);
            this.radioButton_LargeDataset.Name = "radioButton_LargeDataset";
            this.radioButton_LargeDataset.Size = new System.Drawing.Size(136, 24);
            this.radioButton_LargeDataset.TabIndex = 6;
            this.radioButton_LargeDataset.Text = "Large Dataset";
            this.radioButton_LargeDataset.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Data Source:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password:";
            // 
            // textBox_DataSource
            // 
            this.textBox_DataSource.Location = new System.Drawing.Point(122, 12);
            this.textBox_DataSource.Name = "textBox_DataSource";
            this.textBox_DataSource.Size = new System.Drawing.Size(292, 26);
            this.textBox_DataSource.TabIndex = 10;
            // 
            // textBox_Username
            // 
            this.textBox_Username.Location = new System.Drawing.Point(122, 44);
            this.textBox_Username.Name = "textBox_Username";
            this.textBox_Username.Size = new System.Drawing.Size(292, 26);
            this.textBox_Username.TabIndex = 11;
            // 
            // textBox_Password
            // 
            this.textBox_Password.Location = new System.Drawing.Point(122, 76);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.PasswordChar = '*';
            this.textBox_Password.Size = new System.Drawing.Size(292, 26);
            this.textBox_Password.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton_MongoDB);
            this.panel1.Controls.Add(this.radioButton_Oracle);
            this.panel1.Location = new System.Drawing.Point(0, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 30);
            this.panel1.TabIndex = 13;
            // 
            // radioButton_MongoDB
            // 
            this.radioButton_MongoDB.AutoSize = true;
            this.radioButton_MongoDB.Location = new System.Drawing.Point(99, 3);
            this.radioButton_MongoDB.Name = "radioButton_MongoDB";
            this.radioButton_MongoDB.Size = new System.Drawing.Size(106, 24);
            this.radioButton_MongoDB.TabIndex = 1;
            this.radioButton_MongoDB.Text = "MongoDB";
            this.radioButton_MongoDB.UseVisualStyleBackColor = true;
            this.radioButton_MongoDB.CheckedChanged += new System.EventHandler(this.radioButton_MongoDB_CheckedChanged);
            // 
            // radioButton_Oracle
            // 
            this.radioButton_Oracle.AutoSize = true;
            this.radioButton_Oracle.Checked = true;
            this.radioButton_Oracle.Location = new System.Drawing.Point(13, 3);
            this.radioButton_Oracle.Name = "radioButton_Oracle";
            this.radioButton_Oracle.Size = new System.Drawing.Size(80, 24);
            this.radioButton_Oracle.TabIndex = 0;
            this.radioButton_Oracle.TabStop = true;
            this.radioButton_Oracle.Text = "Oracle";
            this.radioButton_Oracle.UseVisualStyleBackColor = true;
            // 
            // progressBar_Generation
            // 
            this.progressBar_Generation.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.formDataGeneratorBindingSource, "ProgressValue", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
            this.progressBar_Generation.Location = new System.Drawing.Point(12, 220);
            this.progressBar_Generation.Name = "progressBar_Generation";
            this.progressBar_Generation.Size = new System.Drawing.Size(402, 44);
            this.progressBar_Generation.TabIndex = 14;
            // 
            // formDataGeneratorBindingSource
            // 
            this.formDataGeneratorBindingSource.DataSource = typeof(COMPX323_Generator.Form_DataGenerator);
            // 
            // label_CurrentStep
            // 
            this.label_CurrentStep.AutoSize = true;
            this.label_CurrentStep.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.formDataGeneratorBindingSource, "ProgressText", true));
            this.label_CurrentStep.Location = new System.Drawing.Point(12, 275);
            this.label_CurrentStep.Name = "label_CurrentStep";
            this.label_CurrentStep.Size = new System.Drawing.Size(0, 20);
            this.label_CurrentStep.TabIndex = 15;
            // 
            // timer_progressUpdate
            // 
            this.timer_progressUpdate.Tick += new System.EventHandler(this.timerProgressUpdate_Tick);
            // 
            // Form_DataGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 304);
            this.Controls.Add(this.label_CurrentStep);
            this.Controls.Add(this.progressBar_Generation);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox_Password);
            this.Controls.Add(this.textBox_Username);
            this.Controls.Add(this.textBox_DataSource);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton_LargeDataset);
            this.Controls.Add(this.radioButton_SmallDataset);
            this.Controls.Add(this.button_Generate);
            this.Controls.Add(this.textBox_RandomSeed);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_DataGenerator";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "COMPX323 Database Generator";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.formDataGeneratorBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_RandomSeed;
        private System.Windows.Forms.Button button_Generate;
        private System.Windows.Forms.RadioButton radioButton_SmallDataset;
        private System.Windows.Forms.RadioButton radioButton_LargeDataset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_DataSource;
        private System.Windows.Forms.TextBox textBox_Username;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton_MongoDB;
        private System.Windows.Forms.RadioButton radioButton_Oracle;
        private System.Windows.Forms.ProgressBar progressBar_Generation;
        private System.Windows.Forms.Label label_CurrentStep;
        private System.Windows.Forms.BindingSource formDataGeneratorBindingSource;
        private System.Windows.Forms.Timer timer_progressUpdate;
    }
}

