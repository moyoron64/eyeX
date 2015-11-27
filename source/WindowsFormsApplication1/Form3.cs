using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Translator
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public void Update()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("瞬目回数　= " + BlinkCount +"");
        }
    }
}

