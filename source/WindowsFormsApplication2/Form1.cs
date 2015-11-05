using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Translator
{
    /// <summary>
    /// Microsoft Translator APIを利用して日本語→英語翻訳を行うサンプルソフトウェア
    /// 
    /// 参考ページ
    /// ・Microsoft Translator | Windows Azure Marketplace
    /// 　https://datamarket.azure.com/dataset/1899a118-d202-492c-aa16-ba21c33c06cb
    /// ・開発者
    /// 　http://msdn.microsoft.com/en-us/library/hh454950
    /// ・Translate Method
    /// 　http://msdn.microsoft.com/en-us/library/ff512421
    /// </summary>
    public partial class Form1 : Form
    {

        private TranslatorApi translatorApi;

        public Form1()
        {
            InitializeComponent();

            translatorApi = new TranslatorApi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                return;
            }

            button1.Enabled = false;
            AddMsg("翻訳開始");
            backgroundWorker1.RunWorkerAsync(inTextBox.Text);
        }

        private void AddMsg(string msg)
        {
            msgTextBox.Text += msg + "\r\n";
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string inText = (string)e.Argument;
            e.Result = translatorApi.Translate(inText);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                outTextBox.Text = (string)e.Result;
            }
            else
            {
                outTextBox.Text = string.Empty;
                AddMsg(e.Error.Message);
            }
            AddMsg("翻訳終了");
            button1.Enabled = true;
        }
    }
}
