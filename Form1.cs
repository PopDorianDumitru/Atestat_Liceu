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
        int id_sesiune, index_sesiune;
        
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
            // TODO: This line of code loads data into the 'database1DataSet.Imagini_profil' table. You can move, or remove it, as needed.
            this.imagini_profilTableAdapter.Fill(this.database1DataSet.Imagini_profil);
            // TODO: This line of code loads data into the 'database1DataSet.Utilizatori' table. You can move, or remove it, as needed.
            //this.utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);
            // TODO: This line of code loads data into the 'database1DataSet.Utilizatori' table. You can move, or remove it, as needed.
            //this.utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);
            // TODO: This line of code loads data into the 'database1DataSet.Utilizatori' table. You can move, or remove it, as needed.
            this.utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);

        }
        private void deschide_imagini_profil()
        {
            if(tabControl1.TabPages.Contains(imag) == false)
            {
                TableLayoutPanel pan_imag = new TableLayoutPanel();
                int i = 0, j = 0;
                pan_imag.ColumnCount = 5;
                pan_imag.RowCount = database1DataSet.Imagini_profil.Count / 5;
                if (database1DataSet.Imagini_profil.Count % 5 != 0)
                    pan_imag.RowCount++;
                tabPage4.Controls.Add(pan_imag);
                pan_imag.Dock = DockStyle.Fill;
                for (int q = 0; q < database1DataSet.Imagini_profil.Count; q++)
                {
                    PictureBox avat = new PictureBox();
                    avat.Image = Image.FromFile($"Imagini_profil\\ava{q + 1}.jpg");
                    avat.SizeMode = PictureBoxSizeMode.StretchImage;
                    avat.Margin = Padding.Empty;
                    avat.Padding = Padding.Empty;
                    avat.Height = pan_imag.Size.Height / pan_imag.RowCount;
                    avat.Width = pan_imag.Size.Width / pan_imag.ColumnCount;
                    pan_imag.Controls.Add(avat, i, j);
                    avat.Dock = DockStyle.Fill;
                    i++;
                    if (i > 5)
                    {
                        i = 0;
                        j++;
                    }
                }

                tabControl1.TabPages.Insert(tabControl1.TabPages.Count, imag);
            }
        }
        private void gaseste_cont(string util, string par)
        {
            int ok = 0;
            for(int i = 0; i < database1DataSet.Utilizatori.Count && ok == 0; i++)
            {
                if(util == database1DataSet.Utilizatori[i].Nume_Utilizator && par == database1DataSet.Utilizatori[i].Parola)
                {
                    ok = 1;
                    index_sesiune = i;
                    id_sesiune = database1DataSet.Utilizatori[i].Id;
                }
            }    
        }
        private void deschide_profil()
        {
            tabControl1.TabPages.Insert(tabControl1.TabPages.Count, profil);
            pictureBox2.Image = Image.FromFile($"Imagini_profil\\{database1DataSet.Utilizatori[index_sesiune].Imag_prof}");
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            try
            {
                textBox9.Text = database1DataSet.Utilizatori[index_sesiune].Monede.ToString();
            
            }
            catch
            {
                textBox9.Text = "0";
            }
               
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
                        {
                            ok = 0;
                        }
                    if (ok == 1)
                    {
                        utilizatoriTableAdapter.Inserare_Utilizator_Nou(util, prl, loc, sco, adm, "def_avatar.jpg", "def_fundal.jpg");
                        utilizatoriTableAdapter.Fill(database1DataSet.Utilizatori);
                        tabControl1.TabPages.Remove(tabPage3);
                        utilizator_sesiune = util;
                        parola_sesiune = prl;
                        gaseste_cont(util, prl);
                        id_sesiune = database1DataSet.Utilizatori[index_sesiune].Id; 
                        deschide_profil();
                    }
                    else
                        MessageBox.Show("Numele de utilizator sau adresa de email sunt folosite de un alt utilizator");
                    
                    
                }
                else
                    MessageBox.Show("Parolele nu sunt identice!");
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 x = new Form2();
            x.ShowDialog();
            if (x.DialogResult == DialogResult.Cancel)
            {
                MessageBox.Show($"Ai castigat {x.punctaj} monede");
                int monede_nou = Convert.ToInt32(textBox9.Text) + x.punctaj;
                utilizatoriTableAdapter.UpdateMonedeUtilizator(monede_nou, id_sesiune);
                textBox9.Text = monede_nou.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            deschide_imagini_profil();
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
                gaseste_cont(num, prl);
                tabControl1.TabPages.Remove(tabPage2);
                deschide_profil();
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
