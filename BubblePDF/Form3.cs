using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAG_TeknikResimDamgalama
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string nl = System.Environment.NewLine;
            string text = @"Ekinoks-AG Teknik Resim Damgalama Programı" + nl + nl;
            text = text + @"Version: 0.9" + nl;
            text = text + @"Date: 27.07.2020" + nl;
            text = text + @"Revised By: Utku TÜRER" + nl;
            text = text + @"Description: First version" + nl;
            richTextBox1.Text = text;
        }
    }
}
