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
            dbB703CB358AEntities1 d = new dbB703CB358AEntities1();
            var dai = from dairelerSet in d.dairelerSet select dairelerSet;
            dataGridView1.DataSource = dai.ToList();

        }
    }
}
