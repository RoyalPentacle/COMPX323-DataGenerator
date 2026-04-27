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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_RandomSeed = new System.Windows.Forms.TextBox();
            this.button_Generate = new System.Windows.Forms.Button();
            this.radioButton_SmallDataset = new System.Windows.Forms.RadioButton();
            this.radioButton_LargeDataset = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Random Seed:";
            // 
            // textBox_RandomSeed
            // 
            this.textBox_RandomSeed.Location = new System.Drawing.Point(134, 12);
            this.textBox_RandomSeed.Name = "textBox_RandomSeed";
            this.textBox_RandomSeed.Size = new System.Drawing.Size(280, 26);
            this.textBox_RandomSeed.TabIndex = 2;
            // 
            // button_Generate
            // 
            this.button_Generate.Location = new System.Drawing.Point(322, 74);
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
            this.radioButton_SmallDataset.Location = new System.Drawing.Point(16, 44);
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
            this.radioButton_LargeDataset.Location = new System.Drawing.Point(16, 81);
            this.radioButton_LargeDataset.Name = "radioButton_LargeDataset";
            this.radioButton_LargeDataset.Size = new System.Drawing.Size(136, 24);
            this.radioButton_LargeDataset.TabIndex = 6;
            this.radioButton_LargeDataset.Text = "Large Dataset";
            this.radioButton_LargeDataset.UseVisualStyleBackColor = true;
            // 
            // Form_DataGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 122);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_RandomSeed;
        private System.Windows.Forms.Button button_Generate;
        private System.Windows.Forms.RadioButton radioButton_SmallDataset;
        private System.Windows.Forms.RadioButton radioButton_LargeDataset;
    }
}

