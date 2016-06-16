using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace binaTakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reload();

        }
        public void reload()
        {
            dbB703CB358AEntities1 d = new dbB703CB358AEntities1();
            var dai = from Binalar in d.BinalarSet select Binalar;
            dataGridView1.DataSource = dai.ToList();
            dataGridView1.Columns[0].Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Binalar bn = new Binalar();
            bn.binaAdi = textBox1.Text;
            bn.daireSayisi = Convert.ToInt32(textBox2.Text);
            bn.userId = "1";
            dbB703CB358AEntities1 ent = new dbB703CB358AEntities1();
            
            ent.BinalarSet.Add(bn);
            ent.SaveChanges();
            int i;
            for (i = 1; i <= bn.daireSayisi; i++)
            {
                dairelerSet daire = new dairelerSet();
                daire.kisi_id = 0;
                daire.daireno = i;
                daire.binaId = bn.BinaId;
                daire.ev_sahibi_id = 0;
                
                ent.dairelerSet.Add(daire);
                ent.SaveChanges();
            }
            
            reload();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            textBox5.Text = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            dbB703CB358AEntities1 ent = new dbB703CB358AEntities1();
            int id = Convert.ToInt32(textBox5.Text);
            Binalar bn = ent.BinalarSet.FirstOrDefault(x => x.BinaId == id);
            ent.BinalarSet.Remove(bn);
            ent.SaveChanges();
            reload();
        }
        private void reloadDaire()
        {
            dbB703CB358AEntities1 d = new dbB703CB358AEntities1();
            int id = Convert.ToInt32(textBox5.Text);
            var dai = from dairelerSet in d.dairelerSet where dairelerSet.binaId == id select dairelerSet;
            dataGridView2.DataSource = dai.ToList();
            dataGridView2.Columns[0].Visible = false;
            var kisiler = from kisilerSet in d.kisilerSet where kisilerSet.bina_id == id select kisilerSet;
            comboBox1.DataSource = kisiler.ToList();
            comboBox1.DisplayMember = "isim";
            comboBox1.ValueMember = "kisiId";
           
        }
        private void button4_Click(object sender, EventArgs e)
        {
            reloadDaire();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dbB703CB358AEntities1 ent = new dbB703CB358AEntities1();
            kisilerSet kisi = new kisilerSet();
            int id = Convert.ToInt32(textBox5.Text);
            kisi.bina_id = id;
            kisi.isim = textBox7.Text;
            kisi.telefon = textBox6.Text;
            ent.kisilerSet.Add(kisi);
            ent.SaveChanges();
            reloadDaire();
        }
    }
}
