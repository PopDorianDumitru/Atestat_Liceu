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
        TabPage imag = new TabPage(), aut = new TabPage() , log = new TabPage(), profil = new TabPage();
        string utilizator_sesiune, parola_sesiune;
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Insert(tabControl1.TabPages.Count, log);
        }

        private void utilizatoriBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.utilizatoriBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Utilizatori' table. You can move, or remove it, as needed.
            //this.utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);
            // TODO: This line of code loads data into the 'database1DataSet.Utilizatori' table. You can move, or remove it, as needed.
            //this.utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);
            // TODO: This line of code loads data into the 'database1DataSet.Utilizatori' table. You can move, or remove it, as needed.
            this.utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string util = textBox1.Text, prl = textBox2.Text, prlc = textBox3.Text, loc = textBox4.Text, sco = textBox5.Text, adm = textBox6.Text;
            if (util == "" || prl == "" || adm == "")
                MessageBox.Show("Introduceti datele corespunzatoare!");
            else
            {
                if (prl == prlc)
                {
                    int ok = 1;
                    for (int i = 0; i < database1DataSet.Utilizatori.Count && ok == 1; i++)
                        if (util == database1DataSet.Utilizatori[i].Nume_Utilizator || adm == database1DataSet.Utilizatori[i].Adresa_Mail)
                            ok = 0;
                    if (ok == 1)
                    {
                        utilizatoriTableAdapter.Inserare_Utilizator_Nou(util, prl, loc, sco, adm, "def_prof.jpg", "def_fundal.jpg");
                        utilizatoriTableAdapter.Fill(database1DataSet.Utilizatori);
                        tabControl1.TabPages.Remove(tabPage3);
                        tabControl1.TabPages.Insert(tabControl1.TabPages.Count, profil);
                        utilizator_sesiune = util;
                        parola_sesiune = prl;
                    }
                    else
                        MessageBox.Show("Numele de utilizator sau adresa de email sunt folosite de un alt utilizator");
                    
                    
                }
                else
                    MessageBox.Show("Parolele nu sunt identice!");
                
            }
        }

        private void utilizatoriBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.utilizatoriBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void utilizatoriBindingNavigatorSaveItem_Click_2(object sender, EventArgs e)
        {
            this.Validate();
            this.utilizatoriBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string num = textBox7.Text, prl = textBox8.Text;
            int ok = 0;
            for (int i = 0; i < database1DataSet.Utilizatori.Count && ok == 0; i++)
                if (database1DataSet.Utilizatori[i].Nume_Utilizator == num && database1DataSet.Utilizatori[i].Parola == prl)
                    ok = 1;
            if (ok == 1)
            {
                utilizator_sesiune = num;
                parola_sesiune = prl;
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Insert(tabControl1.TabPages.Count, profil);
            }
            else
            {
                MessageBox.Show("Numele de utilizator sau parola sunt gresite!");
            }
        }

        public Form1()
        {
            InitializeComponent();
            imag = tabPage4;
            aut = tabPage2;
            log = tabPage3;
            profil = tabPage5;
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
            tabControl1.TabPages.Remove(tabPage5);
            tabControl1.TabPages.Remove(tabPage4);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Insert(tabControl1.TabPages.Count ,aut);
            pictureBox1.Image = Image.FromFile("Imagini_profil\\def_avatar.jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
