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
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Diagnostics;

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
            comboBox2.DataSource = kisiler.ToList();
            comboBox2.DisplayMember = "isim";
            comboBox2.ValueMember = "kisiId";

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

        private void button7_Click(object sender, EventArgs e)
        {
            dbB703CB358AEntities1 ent = new dbB703CB358AEntities1();
            int daireid = Convert.ToInt32(dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[0].Value);
            dairelerSet daire = ent.dairelerSet.FirstOrDefault(x => x.daireId == daireid);
            daire.kisi_id = Convert.ToInt32(comboBox1.SelectedValue);
            if (!checkBox1.Checked)
                daire.ev_sahibi_id = Convert.ToInt32(comboBox2.SelectedValue);
            else
                daire.ev_sahibi_id = Convert.ToInt32(comboBox1.SelectedValue);
            ent.SaveChanges();
            reloadDaire();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.SelectedValue = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[2].Value;
            comboBox2.SelectedValue = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[3].Value;
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
            string kullanici_adi = textBox9.Text;
            string s1 = textBox8.Text;
            string s2 = textBox10.Text;
            string binaadi = textBox11.Text;
            if (textBox12.Text!="" && kullanici_adi != "" && s1 == s2 && s1 != "" && binaadi != "")
            {
                int dairesayisi = Convert.ToInt32(textBox12.Text);
                int i = 1;
                for (i = 1; i <= dairesayisi; i++)
                {
                    DataGridViewRow d = new DataGridViewRow();

                    dataGridView3.Rows.Add(i.ToString());

                }
                button10.Visible = true;
            }
            else
            {
                MessageBox.Show("Formda hatalar var!");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile1 = new OpenFileDialog();
            if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openfile1.FileName;
            }
            {
                Excel.Application app = new Excel.Application();
                Excel.Workbook wb = app.Workbooks.Open(textBox1.Text, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Excel.Worksheet ws = wb.Sheets[1];
                Excel.Range r = ws.UsedRange;
                ws.Select();
                int i = 3;
                string tel;
                for (i = 3; i < 45; i++)
                {
                    dataGridView3.Rows[i-3].Cells[1].Value= (string)ws.Cells[i, 2].Value2;
                    if (Convert.ToString(ws.Cells[i, 3].Value2) == null)
                    {
                        if (Convert.ToString(ws.Cells[i, 5].Value2) == null)
                        {
                            tel = "-";
                        }
                        else
                            tel = Convert.ToString(ws.Cells[i, 5].Value2);
                    }
                    else
                        tel = Convert.ToString(ws.Cells[i, 3].Value2);

                    dataGridView3.Rows[i - 3].Cells[2].Value = tel;
                    string durum = Convert.ToString(ws.Cells[i, 4].Value2);
                    if ((string)ws.Cells[i, 4].Value2 != "Kirada" && Convert.ToString(ws.Cells[i, 4].Value2) != null)
                    {
                        dataGridView3.Rows[i - 3].Cells[3].Value = (string)ws.Cells[i, 2].Value2;
                        dataGridView3.Rows[i - 3].Cells[4].Value = tel;
                        dataGridView3.Rows[i - 3].Cells[5].Value = checked(true);
                    }
                    else
                    {
                        dataGridView3.Rows[i - 3].Cells[3].Value = (string)ws.Cells[i, 6].Value2;
                        dataGridView3.Rows[i - 3].Cells[4].Value = "-";
                    }
                }
                string sa = (string)ws.Cells[5, 2].Value2;
                wb.Close(false, Missing.Value, Missing.Value);
                app.DisplayAlerts = false;
                app.Quit();
                foreach (Process clsProcess in Process.GetProcesses())
                    if (clsProcess.ProcessName.Equals("EXCEL"))  //Process Excel?
                        clsProcess.Kill();
            }
        
 
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            bool control = true;
            for (i = 0; i < dataGridView3.Rows.Count; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    if (dataGridView3.Rows[i].Cells[j].Value == null)
                    {
                        control = false;
                        MessageBox.Show("Tabloda boş alanları tamamlayın");
                        break;
                       
                    }
                    if (!control) break;
                }
            }

            if (control)
            {
                dbB703CB358AEntities1 ent = new dbB703CB358AEntities1();
                Users user = new Users();
                user.kullanici_adi = textBox9.Text;
                user.sifre = textBox8.Text;
                user.onay = false;
                ent.UsersSet.Add(user);
                ent.SaveChanges();

                Binalar bn = new Binalar();
                bn.binaAdi = textBox11.Text;
                bn.daireSayisi = Convert.ToInt32(textBox12.Text);
                bn.userId = user.userId.ToString();
                ent.BinalarSet.Add(bn);
                ent.SaveChanges();
              
                for (i = 1; i <= bn.daireSayisi; i++)
                {
                    dairelerSet daire = new dairelerSet();
                    daire.kisi_id = 0;
                    daire.daireno = i;
                    daire.binaId = bn.BinaId;
                    daire.ev_sahibi_id = 0;
                    ent.dairelerSet.Add(daire);
                    ent.SaveChanges();
                    kisilerSet evsahibi = new kisilerSet();
                    evsahibi.bina_id = bn.BinaId;
                    evsahibi.daire_id = daire.daireId;
                    evsahibi.isim = dataGridView3.Rows[i - 1].Cells[1].Value.ToString();
                    evsahibi.telefon= dataGridView3.Rows[i - 1].Cells[2].Value.ToString();
                    ent.kisilerSet.Add(evsahibi);
                    ent.SaveChanges();
                    daire.ev_sahibi_id = evsahibi.kisiId;
                    if(! Convert.ToBoolean(dataGridView3.Rows[i - 1].Cells[5].Value)) {
                        kisilerSet kiracı = new kisilerSet();
                        kiracı.bina_id = bn.BinaId;
                        kiracı.daire_id = daire.daireId;
                        kiracı.isim = dataGridView3.Rows[i - 1].Cells[3].Value.ToString();
                        kiracı.telefon = dataGridView3.Rows[i - 1].Cells[4].Value.ToString();
                        ent.kisilerSet.Add(kiracı);
                        ent.SaveChanges();
                        daire.kisi_id = kiracı.kisiId;
                    }
                    else
                    {
                        daire.kisi_id = evsahibi.kisiId;
                    }
                    ent.SaveChanges();
                }
                panel4.Visible = false;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string k_a = textBox3.Text;
            string s = textBox4.Text;
            if (k_a !="" && s != "")
            {
                dbB703CB358AEntities1 ent = new dbB703CB358AEntities1();
                Users user=ent.UsersSet.FirstOrDefault(x => x.kullanici_adi == k_a);
                if (user.sifre == s)
                {
                    panel4.Visible = false;
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı!");
                }
            }
        }
    }
}
