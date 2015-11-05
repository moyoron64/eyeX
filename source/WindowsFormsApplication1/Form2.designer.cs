namespace Translator
{
    partial class Form2
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.inLabel = new System.Windows.Forms.Label();
            this.inTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.outTextBox = new System.Windows.Forms.TextBox();
            this.outLabel = new System.Windows.Forms.Label();
            this.msgTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // inLabel
            // 
            this.inLabel.AutoSize = true;
            this.inLabel.Location = new System.Drawing.Point(12, 9);
            this.inLabel.Name = "inLabel";
            this.inLabel.Size = new System.Drawing.Size(41, 12);
            this.inLabel.TabIndex = 0;
            this.inLabel.Text = "日本語";
            // 
            // inTextBox
            // 
            this.inTextBox.Location = new System.Drawing.Point(12, 24);
            this.inTextBox.Multiline = true;
            this.inTextBox.Name = "inTextBox";
            this.inTextBox.Size = new System.Drawing.Size(200, 100);
            this.inTextBox.TabIndex = 1;
            this.inTextBox.Text = "こんにちは、マイクロソフト翻訳API！";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(218, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "翻訳";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // outTextBox
            // 
            this.outTextBox.Location = new System.Drawing.Point(299, 26);
            this.outTextBox.Multiline = true;
            this.outTextBox.Name = "outTextBox";
            this.outTextBox.ReadOnly = true;
            this.outTextBox.Size = new System.Drawing.Size(200, 100);
            this.outTextBox.TabIndex = 3;
            // 
            // outLabel
            // 
            this.outLabel.AutoSize = true;
            this.outLabel.Location = new System.Drawing.Point(297, 9);
            this.outLabel.Name = "outLabel";
            this.outLabel.Size = new System.Drawing.Size(29, 12);
            this.outLabel.TabIndex = 4;
            this.outLabel.Text = "英語";
            // 
            // msgTextBox
            // 
            this.msgTextBox.Location = new System.Drawing.Point(12, 151);
            this.msgTextBox.Multiline = true;
            this.msgTextBox.Name = "msgTextBox";
            this.msgTextBox.ReadOnly = true;
            this.msgTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.msgTextBox.Size = new System.Drawing.Size(487, 99);
            this.msgTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "処理メッセージ";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.msgTextBox);
            this.Controls.Add(this.outLabel);
            this.Controls.Add(this.outTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.inTextBox);
            this.Controls.Add(this.inLabel);
            this.Name = "Form1";
            this.Text = "Microsoft Translator API を使ったサンプルソフトウェア";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label inLabel;
        private System.Windows.Forms.TextBox inTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox outTextBox;
        private System.Windows.Forms.Label outLabel;
        private System.Windows.Forms.TextBox msgTextBox;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

