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
    public partial class Form2 : Form
    {
        Random intrebare = new Random();
        private void Seteaza_Raspunsuri(int x, int y, int z, int w, int q)
        {
            radioButton1.Text = database1DataSet.Intrebari[x].Raspuns_1;
            radioButton2.Text = database1DataSet.Intrebari[x].Raspuns_2;
            radioButton3.Text = database1DataSet.Intrebari[x].Raspuns_3;
            radioButton4.Text = database1DataSet.Intrebari[x].Raspuns_4;

            radioButton5.Text = database1DataSet.Intrebari[y].Raspuns_1;
            radioButton6.Text = database1DataSet.Intrebari[y].Raspuns_2;
            radioButton7.Text = database1DataSet.Intrebari[y].Raspuns_3;
            radioButton8.Text = database1DataSet.Intrebari[y].Raspuns_4;

            radioButton9.Text = database1DataSet.Intrebari[z].Raspuns_1;
            radioButton10.Text = database1DataSet.Intrebari[z].Raspuns_2;
            radioButton11.Text = database1DataSet.Intrebari[z].Raspuns_3;
            radioButton12.Text = database1DataSet.Intrebari[z].Raspuns_4;

            radioButton13.Text = database1DataSet.Intrebari[w].Raspuns_1;
            radioButton14.Text = database1DataSet.Intrebari[w].Raspuns_2;
            radioButton15.Text = database1DataSet.Intrebari[w].Raspuns_3;
            radioButton16.Text = database1DataSet.Intrebari[w].Raspuns_4;

            radioButton17.Text = database1DataSet.Intrebari[q].Raspuns_1;
            radioButton18.Text = database1DataSet.Intrebari[q].Raspuns_2;
            radioButton19.Text = database1DataSet.Intrebari[q].Raspuns_3;
            radioButton20.Text = database1DataSet.Intrebari[q].Raspuns_4;
        }
        private void Creeaza_intrebari(int x, int y, int z, int w, int q)
        {
            richTextBox1.Text = database1DataSet.Intrebari[x].Enunt.ToString();
            richTextBox2.Text = database1DataSet.Intrebari[y].Enunt.ToString();
            richTextBox3.Text = database1DataSet.Intrebari[z].Enunt.ToString();
            richTextBox4.Text = database1DataSet.Intrebari[w].Enunt.ToString();
            richTextBox5.Text = database1DataSet.Intrebari[q].Enunt.ToString();
        }
        private void Seteaza_Imagini(int x, int y, int z, int w, int q)
        {
            try { pictureBox1.Image = Image.FromFile($"Imagini_intrebare\\{database1DataSet.Intrebari[x].Imagine}");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;}
            catch { }
            try {  pictureBox2.Image = Image.FromFile($"Imagini_intrebare\\{database1DataSet.Intrebari[y].Imagine}");
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;}
            catch { }
            try { pictureBox3.Image = Image.FromFile($"Imagini_intrebare\\{database1DataSet.Intrebari[z].Imagine}");
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;}
            catch { }
            try { pictureBox4.Image = Image.FromFile($"Imagini_intrebare\\{database1DataSet.Intrebari[w].Imagine}");
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;}
            catch { }
            try { pictureBox5.Image = Image.FromFile($"Imagini_intrebare\\{database1DataSet.Intrebari[q].Imagine}");
                pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage; }
            catch { }
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void intrebariBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.intrebariBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Intrebari' table. You can move, or remove it, as needed.
      
            // TODO: This line of code loads data into the 'database1DataSet.Intrebari' table. You can move, or remove it, as needed.
            this.intrebariTableAdapter.Fillintrebare(this.database1DataSet.Intrebari);
            int x = intrebare.Next(0, database1DataSet.Intrebari.Count - 1), y, z, w, q;
            y = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            while (y == x) y = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            z = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            while (z == y || z == x) z = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            w = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            while (w == x || w == y || w == z) w = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            q = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            while (q == x || q == y || q == z || q == w) q = intrebare.Next(0, database1DataSet.Intrebari.Count - 1);
            Creeaza_intrebari(x, y, z, w, q);
            Seteaza_Raspunsuri(x, y, z, w, q);
            Seteaza_Imagini(x, y, z, w, q);
        }




    }
}
