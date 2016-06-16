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
            dbB703CB358AEntities1 ent = new dbB703CB358AEntities1();
            ent.BinalarSet.Add(bn);
            ent.SaveChanges();
            reload();
        }
    }
}
