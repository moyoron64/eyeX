namespace ActivatableButtonsForms
{
    partial class ActivatableButtonsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivatableButtonsForm));
            this.buttonHueDown = new System.Windows.Forms.Button();
            this.buttonHueUp = new System.Windows.Forms.Button();
            this.panelColorSample = new System.Windows.Forms.Panel();
            this.buttonBrightnessUp = new System.Windows.Forms.Button();
            this.buttonBrightnessDown = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.behaviorMap1 = new EyeXFramework.Forms.BehaviorMap(this.components);
            this.SuspendLayout();
            // 
            // buttonHueDown
            // 
            this.buttonHueDown.Location = new System.Drawing.Point(12, 136);
            this.buttonHueDown.Name = "buttonHueDown";
            this.buttonHueDown.Size = new System.Drawing.Size(167, 103);
            this.buttonHueDown.TabIndex = 0;
            this.buttonHueDown.Text = "< Hue";
            this.buttonHueDown.UseVisualStyleBackColor = true;
            this.buttonHueDown.Click += new System.EventHandler(this.buttonHueDown_Click);
            // 
            // buttonHueUp
            // 
            this.buttonHueUp.Location = new System.Drawing.Point(406, 136);
            this.buttonHueUp.Name = "buttonHueUp";
            this.buttonHueUp.Size = new System.Drawing.Size(167, 103);
            this.buttonHueUp.TabIndex = 2;
            this.buttonHueUp.Text = "Hue >";
            this.buttonHueUp.UseVisualStyleBackColor = true;
            this.buttonHueUp.Click += new System.EventHandler(this.buttonHueUp_Click);
            // 
            // panelColorSample
            // 
            this.panelColorSample.BackColor = System.Drawing.Color.SeaGreen;
            this.panelColorSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColorSample.Location = new System.Drawing.Point(185, 121);
            this.panelColorSample.Name = "panelColorSample";
            this.panelColorSample.Size = new System.Drawing.Size(215, 133);
            this.panelColorSample.TabIndex = 4;
            // 
            // buttonBrightnessUp
            // 
            this.buttonBrightnessUp.Location = new System.Drawing.Point(209, 12);
            this.buttonBrightnessUp.Name = "buttonBrightnessUp";
            this.buttonBrightnessUp.Size = new System.Drawing.Size(167, 103);
            this.buttonBrightnessUp.TabIndex = 1;
            this.buttonBrightnessUp.Text = "Lighter";
            this.buttonBrightnessUp.UseVisualStyleBackColor = true;
            this.buttonBrightnessUp.Click += new System.EventHandler(this.buttonBrightnessUp_Click);
            // 
            // buttonBrightnessDown
            // 
            this.buttonBrightnessDown.Location = new System.Drawing.Point(209, 260);
            this.buttonBrightnessDown.Name = "buttonBrightnessDown";
            this.buttonBrightnessDown.Size = new System.Drawing.Size(167, 103);
            this.buttonBrightnessDown.TabIndex = 3;
            this.buttonBrightnessDown.Text = "Darker";
            this.buttonBrightnessDown.UseVisualStyleBackColor = true;
            this.buttonBrightnessDown.Click += new System.EventHandler(this.buttonBrightnessDown_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 383);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(561, 44);
            this.textBox1.TabIndex = 5;
            this.textBox1.TabStop = false;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // ActivatableButtonsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 431);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonBrightnessDown);
            this.Controls.Add(this.buttonBrightnessUp);
            this.Controls.Add(this.panelColorSample);
            this.Controls.Add(this.buttonHueUp);
            this.Controls.Add(this.buttonHueDown);
            this.Name = "ActivatableButtonsForm";
            this.Text = "Activatable Buttons Windows Forms Sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonHueDown;
        private System.Windows.Forms.Button buttonHueUp;
        private System.Windows.Forms.Panel panelColorSample;
        private System.Windows.Forms.Button buttonBrightnessUp;
        private System.Windows.Forms.Button buttonBrightnessDown;
        private System.Windows.Forms.TextBox textBox1;
        private EyeXFramework.Forms.BehaviorMap behaviorMap1;
    }
}

