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
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using MathNet.Numerics.Statistics;

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

        private System.Windows.Forms.Label workLabel = null;
 
        private GazePointDataStream stream;
        private EyePositionDataStream stream2;

        private TranslatorApi translatorApi;


        private double firstDistance;
        private double firstDgree;
        private int firstBlink;

        private bool blinkFirstFlag = false;

        private int sameWord = 0;
        private String preWord = "";
        private int excelCount = 1;
        private int blinkCheckInt;
        private double[] dgreeStock =new double[600];
        private int dgreeStockCount = 0;
        private double dangerDgree = 0;
        private double[] distanceStock = new double[600];
        private int distanceStockCount = 0;
        private double dangerDistance = 0;

        private int blinkMydDanger = 0 ;
        private int dgreeMydDanger = 0 ;
        private int distanceMydDanger = 0 ;

        private int allDanger = 0;

        private String[] wordBox ={"Vitamin","C","plays","an","important","role","in","keeping","us","healthy.","Most","mammals","produce","it","in","their","livers,","so","they","never","suffer","from","a","lack","of","it.","Curiously,","however,","some","mammals,","such","as","humans","and","apes,","cannot","do","so.","What","happens","when","you","lack","this","important","vitamin?","You","might","see","blac-and-blue","marks","on","your","skin.","You","teeth","could","suffer,","too:","the","pink","area","around","them","might","become","soft","and","bleed","easily.","These","are","just","a","couple","of","good","reasons","to","eat","plenty","of","fresh","fruit."};
        private String[] wordChangeBox = { "ビタミン", "C", "働く", "ひとつの", "重要な", "役割", "の中で", "保つ", "私達", "健康", "ほとんどの", "哺乳類", "作る", "それ", "のなかで", "それらの", "肝臓", "だから", "それらは", "決してない", "引き起こす", "から", "ひとつの", "不足", "～の", "それ", "奇妙なことに", "しかし", "いくらかの", "哺乳類は", "その", "ような", "人間", "そして", "類人猿", "できない", "する", "そのように", "何が", "起きる", "～とき", "あなたが", "不足する", "この", "重要な", "ビタミン", "あなたは", "かもしれない", "みる", "黒と青", "あざ", "～で", "あなたの", "肌", "あなたの", "歯", "かもしれない", "害をうける", "～も", "（冠詞）", "ピンク", "場所", "周辺", "それら", "かもしれない", "＝になる", "やわらかく", "そして", "出血する", "簡単に", "それらが", "～です", "ただ", "一組の", "二つ", "～の", "正当な", "理由", "～のため", "食べる", "たくさん", "～の", "新鮮な", "果実" };

        Worksheet ws1;
        Workbook wb;
        Microsoft.Office.Interop.Excel.Application ExcelApp;
        

       


        
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


            string ExcelBookFileName = "test2";

            ExcelApp
              = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Visible = false;
            wb = ExcelApp.Workbooks.Add();

            ws1 = wb.Sheets[1];
            ws1.Select(Type.Missing);

            Range rgn = ws1.Cells[excelCount, 1];
            rgn.Value2 = "瞬目率";
            rgn = ws1.Cells[excelCount, 2];
            rgn.Value2 = "首の傾き";
            rgn = ws1.Cells[excelCount, 3];
            rgn.Value2 = "画面距離";


            this.listBox2.Visible = false;
            this.listBox3.Visible = false;
            this.label3.Visible = false;
            this.label1.Visible = false;
            this.listBox1.Visible = false;                

            


            //Form_Load呼び出し
            Load += Form1_Load;


        }
        
        
        
        private void Form1_Load(object sender, System.EventArgs e)
	    {
		    timer1.Interval = 25;
		    timer1.Enabled = true;
            timer2.Interval = 10000;
            timer2.Enabled = true;
            webBrowser1.Navigate("http://com.center.wakayama-u.ac.jp/~s175022/englishTest11.html");
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

            //listBox1.Items.Clear();
            // ERROR: Not supported in C#: OnErrorStatement

            int[] ltwh = new int[4];
            //objAcc.accLocation((int)ltwh[0], ltwh[1], ltwh[2], ltwh[3], child);

            try
            {
                //listBox1.Items.Add("Name=" + objAcc.get_accName(child));
            }catch{

            }

           


            //label貼り付け
            try
            {
                //Console.WriteLine(objAcc.get_accName(child));

                listUpdate(objAcc.get_accName(child));



                if (!(preWord.Equals(objAcc.get_accName(child))))
                {
                    if (!((objAcc.get_accName(child).Equals("Form1"))||(objAcc.get_accName(child).Equals(" "))))
                    {
                        preWord = objAcc.get_accName(child);
                        sameWord = 0;
                    }

                }
                else
                {
                    if (!((objAcc.get_accName(child).Equals("Form1")) && (objAcc.get_accName(child).Equals(" ")&& (objAcc.get_accName(child).Equals("")))))
                    {
                        sameWord++;
                        if (sameWord == 16 - (allDanger))
                        {

                            //ラベルの生成
                            List<System.Windows.Forms.TextBox> clist = new List<System.Windows.Forms.TextBox>();

                            System.Windows.Forms.TextBox tb = new System.Windows.Forms.TextBox();
                            tb.Top = (int)gazeY+21;
                            tb.Left = (int)gazeX;
                            tb.Height = 10;
                            tb.Width = 60;
                            this.Controls.Add(tb);

                            


                            for (int i = 0; i <= wordBox.Length; i++)
                            {
                                if (objAcc.get_accName(child).Equals(wordBox[i]))
                                {
                                    //最前面へ
                                    tb.BringToFront();
                                    clist.Add(tb);
                                    
                                    tb.Text = wordChangeBox[i];
                                    wordBox[i] = "blank";


                                }
                            }

                            //if (tb.Text.Equals("")) return;
                            tb.BackColor = Color.Red;

                            //翻訳開始
                            //backgroundWorker1.RunWorkerAsync(objAcc.get_accName(child));

                        }
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
            /*
            double x3 = listBox1.Location.X;
            double y3 = listBox1.Location.Y;

            if (System.Math.Abs(listBox1.Location.X - gazeX) > 20  )
            {
                x3 += (e.X * 0.08) - (0.08 * x3);

            }
            if(System.Math.Abs(listBox1.Location.Y - gazeY) > 20){
                y3 += (e.Y * 0.08) - (0.08 * y3) + 5;

            }
                listBox1.Location = new System.Drawing.Point((int)(x3), (int)(y3));
            */

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

            if (e.LeftEye.Z != 0)
            {
                distance = (double)e.LeftEye.Z;
                distanceStock[distanceStockCount] = distance;
                distanceStockCount++;

                if (distanceStockCount >= 600)
                {
                    distanceStockCount = 0;
                    dangerDistance = distanceStock.PopulationVariance();
                }

            }
            
        }


        //瞬きをしているかどうか判定
        private void checkBlink(double x, double y){

            if (x == 0 && y == 0)
            {
                blinkCheckInt = 1;

                eyeCloseCount += 1;
                if (eyeCloseCount == 8)
                {
                    eyeClose = true;
                }

            }
            else
            {
                blinkCheckInt = 0;

                if (eyeClose == true)
                {
                    if (eyeCloseCount <= 14)
                    {
                        BlinkCount += 1;
                        blinkCheckInt = 2;
                    }
                    eyeClose = false;
                }
                eyeCloseCount = 0;
            }

            //Console.WriteLine(BlinkCount);

        }
        
        
        //フォームが閉じるとき
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
            dgree = radian;
            dgreeStock[dgreeStockCount] = radian;
            dgreeStockCount++;

            if (dgreeStockCount >= 600)
            {
                dgreeStockCount = 0;
                dangerDgree = dgreeStock.PopulationVariance();
            }

            //dgree = (double)(radian * 180 / Math.PI);

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
        public void listUpdate(String word)
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



            distanceMydDanger = 0;
            if(dangerDistance > 100)  distanceMydDanger = 1;
            if (dangerDistance > 250) distanceMydDanger = 2;

            dgreeMydDanger = 0;
            if (dangerDgree > 0.0005) dgreeMydDanger = 1;
            if (dangerDgree > 0.001) dgreeMydDanger = 2;

            blinkMydDanger = 0;
            if (preBlinkCount > 3) blinkMydDanger = 1;
            if (preBlinkCount > 4) blinkMydDanger = 2;

            allDanger = distanceMydDanger + dgreeMydDanger + blinkMydDanger;


            //excel書き出し
            try
            {
                excelCount++;
                Range rgn = ws1.Cells[excelCount, 1];
                rgn.Value2 = preBlinkCount * 3;
                rgn = ws1.Cells[excelCount, 2];
                rgn.Value2 = dgree;
                rgn = ws1.Cells[excelCount, 3];
                rgn.Value2 = distance;
                rgn = ws1.Cells[excelCount, 4];
                rgn.Value2 = word;
                rgn = ws1.Cells[excelCount, 5];
                rgn.Value2 = blinkCheckInt;
                rgn = ws1.Cells[excelCount, 6];
                rgn.Value2 = GetUnixTime(DateTime.Now);
                rgn = ws1.Cells[excelCount, 7];
                rgn.Value2 = gazeX;
                rgn = ws1.Cells[excelCount, 8];
                rgn.Value2 = gazeY;
                rgn = ws1.Cells[excelCount, 9];
                rgn.Value2 = allDanger;
                rgn = ws1.Cells[excelCount, 10];
                rgn.Value2 = blinkMydDanger;
                rgn = ws1.Cells[excelCount, 11];
                rgn.Value2 = dgreeMydDanger;
                rgn = ws1.Cells[excelCount, 12];
                rgn.Value2 = distanceMydDanger;
                rgn = ws1.Cells[excelCount, 13];
                rgn.Value2 = dangerDgree;
                rgn = ws1.Cells[excelCount, 14];
                rgn.Value2 = dangerDistance;
            }
            catch
            {

            }

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

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {

            wb.SaveAs("test2");
            wb.Close(false);
            ExcelApp.Quit();

            Console.WriteLine("保存しました");
        }

        // UNIXエポックを表すDateTimeオブジェクトを取得
        private static DateTime UNIX_EPOCH =   DateTime.Now;

        public static long GetUnixTime(DateTime targetTime)
        {
            // UTC時間に変換
            targetTime = targetTime.ToUniversalTime();
            UNIX_EPOCH = UNIX_EPOCH.ToUniversalTime();
         
            // UNIXエポックからの経過時間を取得
            TimeSpan elapsedTime = targetTime - UNIX_EPOCH;

            // 経過秒数に変換
            return (long)elapsedTime.TotalMilliseconds;
        }

       
    }
}
