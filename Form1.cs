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
     
        TableLayoutPanel pan_imag = new TableLayoutPanel();
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
            // TODO: This line of code loads data into the 'database1DataSet.Cumparari_Imagini_Profil' table. You can move, or remove it, as needed.
            this.cumparari_Imagini_ProfilTableAdapter.Fill(this.database1DataSet.Cumparari_Imagini_Profil);
            // TODO: This line of code loads data into the 'database1DataSet.Imagini_profil' table. You can move, or remove it, as needed.
            this.imagini_profilTableAdapter.Fill(this.database1DataSet.Imagini_profil);
            this.utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);
        }
    
        private void deschide_imagini_profil()
        {
           
            if(tabControl1.TabPages.Contains(imag) == false)
            {
                for(int i = 0; i < database1DataSet.Imagini_profil.Count; i++)
                {
                    listBox1.Items.Add($"{database1DataSet.Imagini_profil[i].Nume_imagine}");
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
                utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            deschide_imagini_profil();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index_imagine = -1;
            for(int i = 0; i < database1DataSet.Imagini_profil.Count && index_imagine == -1; i++)
            {
                if (database1DataSet.Imagini_profil[i].Nume_imagine == listBox1.SelectedItem.ToString())
                    index_imagine = i;
            }
            pictureBox3.Image = Image.FromFile($"Imagini_profil\\{listBox1.SelectedItem.ToString()}.jpg");
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            int ok = 0;
            for (int i = 0; i < database1DataSet.Cumparari_Imagini_Profil.Count && ok == 0; i++)
            {
                if (database1DataSet.Cumparari_Imagini_Profil[i].Id_Utilizator == id_sesiune && database1DataSet.Cumparari_Imagini_Profil[i].Id_Imagine_Profil == database1DataSet.Imagini_profil[index_imagine].Id_imagine)
                    ok = 1;
            }
            if(ok == 0)
            {
                DialogResult set = MessageBox.Show($"Doriti sa cumparati imaginea pentru {database1DataSet.Imagini_profil[index_imagine].Valoare} de monede?","Cumparare", MessageBoxButtons.YesNo);
                if(set == DialogResult.Yes)
                {
                    if (database1DataSet.Utilizatori[index_sesiune].Monede < database1DataSet.Imagini_profil[index_imagine].Valoare)
                        MessageBox.Show("Nu ai destule monede!");
                    else
                    {
                        int monede_sesiune = Convert.ToInt32(textBox9.Text) - database1DataSet.Imagini_profil[index_imagine].Valoare;
                        textBox9.Text = monede_sesiune.ToString();
                        utilizatoriTableAdapter.UpdateMonedeUtilizator(monede_sesiune, id_sesiune);
                        utilizatoriTableAdapter.Fill(this.database1DataSet.Utilizatori);
                        cumparari_Imagini_ProfilTableAdapter.InsertQuery_Cumparare_Noua(id_sesiune, database1DataSet.Imagini_profil[index_imagine].Id_imagine, database1DataSet.Imagini_profil[index_imagine].Valoare);
                        cumparari_Imagini_ProfilTableAdapter.Fill(this.database1DataSet.Cumparari_Imagini_Profil);
                    }
                }
                
            }
            else
            {
                DialogResult set = MessageBox.Show("Selectezi drept imagine de profil?", "Selectare", MessageBoxButtons.YesNo);
                if(set == DialogResult.Yes)
                {
                    pictureBox2.Image = Image.FromFile($"Imagini_profil\\{listBox1.SelectedItem.ToString()}.jpg");
                    utilizatoriTableAdapter.Update_Imagine_Profil_Utilizator(listBox1.SelectedItem.ToString()+".jpg", id_sesiune);
                }
            }
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
