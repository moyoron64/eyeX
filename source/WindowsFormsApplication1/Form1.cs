using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Runtime.InteropServices;
using EyeXFramework;
using Tobii.EyeX.Framework;
using EyeXFramework.Forms;

namespace Translator
{
    public partial class Form1 : Form
    {

        private readonly FormsEyeXHost _eyeXHost;
        private double gazeX;
        private double gazeY;
        private int eyeCloseCount;
        private int BlinkCount;
        private bool eyeClose;
        
        private GazePointDataStream stream;
        private EyePositionDataStream stream2;

        private TranslatorApi translatorApi;


        
	    [DllImport("oleacc.dll", CharSet = CharSet.Auto)]
	    private static extern int AccessibleObjectFromPoint(int x, int y, ref Accessibility.IAccessible ppoleAcc, ref object pvarElement);


        public Form1()
        {
            InitializeComponent();
            _eyeXHost = new FormsEyeXHost();
            eyeCloseCount = 0;
            BlinkCount = 0;
            eyeClose = false;
            //フルスクリーンにする
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            translatorApi = new TranslatorApi();

            Load += Form1_Load;


        }
        
        
        
        private void Form1_Load(object sender, System.EventArgs e)
	    {
		    timer1.Interval = 25;
		    timer1.Enabled = true;
            webBrowser1.Navigate("http://com.center.wakayama-u.ac.jp/~s175022/englishTest.html");
            _eyeXHost.Start();
            stream = _eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            stream2 = _eyeXHost.CreateEyePositionDataStream();
            stream.Next += OutputGazePoint;
            stream2.Next += OutputEyePosition;
        }


        private void timer1_Tick_1(object sender, System.EventArgs e)
	    {
		    //int[] xy = new int[2];

		    //GetCursorPos(ref xy(0));

            //int x = System.Windows.Forms.Cursor.Position.X;
            //Y座標を取得する
            //int y = System.Windows.Forms.Cursor.Position.Y;


            // Create a data stream object and listen to events.
           
                     

            //Console.WriteLine(x + "  " + y);



            

		    //listBox1.Items.Add("Value=" + objAcc.get_accValue(child));

            Accessibility.IAccessible objAcc = default(Accessibility.IAccessible);

            object child = null;
            AccessibleObjectFromPoint((int)gazeX,(int)gazeY, ref objAcc, ref child);

            listBox1.Items.Clear();
            // ERROR: Not supported in C#: OnErrorStatement

            int[] ltwh = new int[4];
            //objAcc.accLocation((int)ltwh[0], ltwh[1], ltwh[2], ltwh[3], child);

            try
            {
                listBox1.Items.Add("Name=" + objAcc.get_accName(child));
                
            }catch{

            }

            //backgroundWorker1.RunWorkerAsync(objAcc.get_accName(child));

	    }

        private void OutputGazePoint(object sender, GazePointEventArgs e)
        {
            gazeX = e.X;
            gazeY = e.Y;
        }

        private void OutputEyePosition(object sender, EyePositionEventArgs e)
        {
            checkBlink(e.LeftEye.X, e.LeftEye.Y);
            
        }

        private void checkBlink(double x, double y){

            if (x == 0 && y == 0)
            {
                eyeCloseCount += 1;
                if (eyeCloseCount == 7)
                {
                    eyeClose = true;
                }

            }
            else
            {
                if (eyeClose == true)
                {
                    eyeClose = false;
                    BlinkCount += 1;
                }
                eyeCloseCount = 0;
            }

            Console.WriteLine(BlinkCount);

        }
        
        

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _eyeXHost.Dispose();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //通常サイズに戻す
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
        }


        private void AddMsg(string msg)
        {
            msgTextBox.Text += msg + "\r\n";
        }




        private void button2_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine("               s");

            if (backgroundWorker1.IsBusy)
            {
                return;
            }

            button2.Enabled = false;
            AddMsg("翻訳開始");
        }

        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
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






	

	
	



