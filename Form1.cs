using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intrebari_Bac
{
    public partial class Form1 : Form
    {
        TabPage imag = new TabPage(), aut = new TabPage() , log = new TabPage();

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Insert(tabControl1.TabPages.Count, log);
        }

        public Form1()
        {
            InitializeComponent();
            imag = tabPage4;
            aut = tabPage2;
            log = tabPage3;
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
            tabControl1.TabPages.Remove(tabPage4);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Insert(tabControl1.TabPages.Count ,aut);
        }
    }
}
