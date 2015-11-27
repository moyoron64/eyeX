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
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

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
        private double dgree;
        private double distance;
        private int preBlinkCount;

        Label workLabel = null;
 
        private GazePointDataStream stream;
        private EyePositionDataStream stream2;

        private TranslatorApi translatorApi;


        private double firstDistance;
        private double firstDgree;
        private int firstBlink;

        private bool blinkFirstFlag = false;

        private int sameWord = 0;
        private String preWord = "";

       


        
	    [DllImport("oleacc.dll", CharSet = CharSet.Auto)]
	    private static extern int AccessibleObjectFromPoint(int x, int y, ref Accessibility.IAccessible ppoleAcc, ref object pvarElement);


        public Form1()
        {
            //初期化
            InitializeComponent();
            eyeCloseCount = 0;
            dgree = 0;
            BlinkCount = 0;
            distance = 0;

            firstBlink = 20;
 
            eyeClose = false;

            //初期設定
            firstDistance =600 ;
            firstDgree= 0;
            firstBlink = 21;

            listBox3.Items.Clear();
            listBox3.Items.Add("瞬き  " + firstBlink  + "　回/分");
            listBox3.Items.Add("首の傾き　" + firstDgree + "　度");
            listBox3.Items.Add("画面距離　" + firstDistance + "　mm");

            
            _eyeXHost = new FormsEyeXHost();

            //フルスクリーンにする
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            
            
            translatorApi = new TranslatorApi();

            //Form_Load呼び出し
            Load += Form1_Load;


        }
        
        
        
        private void Form1_Load(object sender, System.EventArgs e)
	    {
		    timer1.Interval = 25;
		    timer1.Enabled = true;
            timer2.Interval = 20000;
            timer2.Enabled = true;
            webBrowser1.Navigate("http://com.center.wakayama-u.ac.jp/~s175022/englishTest.html");
            _eyeXHost.Start();
            stream = _eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            stream2 = _eyeXHost.CreateEyePositionDataStream();
            stream.Next += OutputGazePoint;
            stream2.Next += OutputEyePosition;

            


        }


        private void timer1_Tick_1(object sender, System.EventArgs e)
	    {

            listUpdate();
            
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

            //listBox1.Items.Clear();
            // ERROR: Not supported in C#: OnErrorStatement

            int[] ltwh = new int[4];
            //objAcc.accLocation((int)ltwh[0], ltwh[1], ltwh[2], ltwh[3], child);

            try
            {
                //listBox1.Items.Add("Name=" + objAcc.get_accName(child));
            }catch{

            }


            try
            {
                Console.WriteLine(objAcc.get_accName(child));


                if (!(preWord.Equals(objAcc.get_accName(child))))
                {
                    if (!(objAcc.get_accName(child).Equals("Form1")))
                    {
                        preWord = objAcc.get_accName(child);
                        sameWord = 0;
                    }

                }
                else
                {
                    sameWord++;
                    if (sameWord == 5)
                    {
                        backgroundWorker1.RunWorkerAsync(objAcc.get_accName(child));
                    }
                }
            }
            catch
            {

            }

            
	    }

        //見ている座標の更新
        private void OutputGazePoint(object sender, GazePointEventArgs e)
        {
            gazeX = e.X;
            gazeY = e.Y;
            
            double x3 = listBox1.Location.X;
            double y3 = listBox1.Location.Y;

            if (System.Math.Abs(listBox1.Location.X - gazeX) > 20  )
            {
                x3 += (e.X * 0.08) - (0.08 * x3);

            }
            if(System.Math.Abs(listBox1.Location.Y - gazeY) > 20){
                y3 += (e.Y * 0.08) - (0.08 * y3) + 5;

            }
                listBox1.Location = new Point((int)(x3), (int)(y3));


        }

        //アイポジションの座標の更新
        private void OutputEyePosition(object sender, EyePositionEventArgs e)
        {
            //両方の目の位置が把握できているときに傾きをだす
            if ((int)e.LeftEye.X != 0 && (int)e.LeftEye.Y != 0 && (int)e.RightEye.X != 0 && (int)e.RightEye.Y != 0)
            {
                getDgree(e.LeftEye.X, e.LeftEye.Y, e.RightEye.X, e.RightEye.Y);
            }
            
            //瞬きの回数を確認する
            checkBlink(e.LeftEye.X, e.LeftEye.Y);
            //Console.WriteLine(BlinkCount);

            if(e.LeftEye.Z != 0)distance = (double)e.LeftEye.Z;

            
        }


        //瞬きをしているかどうか判定
        private void checkBlink(double x, double y){

            if (x == 0 && y == 0)
            {
                eyeCloseCount += 1;
                if (eyeCloseCount == 8)
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

            //Console.WriteLine(BlinkCount);

        }
        
        
        //フォームが閉じるとき
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //アイホストを終了する
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
            //msgTextBox.Text += msg + "\r\n";
        }



        //二点間から傾きを出す
        private void getDgree(double x , double y , double x2 ,double y2){
            
            double dx = x2-x;
            double dy = y2 - y;
            double radian = Math.Atan2(dy, dx);
            dgree = (double)(radian * 180 / Math.PI);
            double dgreeAbs = System.Math.Abs(dgree);
            //Console.WriteLine(dgree);
            
        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine("               s");

            if (backgroundWorker1.IsBusy)
            {
                return;
            }

            //button2.Enabled = false;
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
                //outTextBox.Text = (string)e.Result;
                listBox1.Items.Clear();
                listBox1.Items.Add((string)e.Result);
            }
            else
            {
                //outTextBox.Text = string.Empty;
                AddMsg(e.Error.Message);
            }
            AddMsg("翻訳終了");
            button1.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            BlinkUpdate();



        }

        private void translate()
        {


        }


        //瞬きの値表示更新
        public void BlinkUpdate()
        {
            listBox2.Items.Clear();
            listBox2.Items.Add("瞬き  " + BlinkCount * 3 + "　回/分");
            listBox2.Items.Add("首の傾き" + (int)dgree + "　度");
            listBox2.Items.Add("画面距離" + (int)distance + "　mm");
            preBlinkCount = BlinkCount;
            blinkFirstFlag = true;
            BlinkCount = 0;
        }

        //リストの更新
        public void listUpdate()
        {
            
            listBox2.Items.Clear();

            listBox2.Items.Add("瞬き  " + preBlinkCount * 3 + "　回/分");
            if (blinkFirstFlag == false)
            {
                listBox2.Items.Clear();
                listBox2.Items.Add("瞬き  " + "＊" + "　回/分");
            }

            listBox2.Items.Add("首の傾き　" + (int)dgree + "　度");
            listBox2.Items.Add("画面距離　" + (int)distance + "　mm");

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

       
    }
}
