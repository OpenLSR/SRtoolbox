using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRtoolbox
{
    public partial class RTBViewForm : Form
    {
        RTB rtb = new RTB();

        public RTBViewForm()
        {
            InitializeComponent();
        }

        private void rtbLoadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open RTB File";
            openFileDialog.Filter = "MOTO.rtb|MOTO.rtb";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rtb.GenTable(openFileDialog.FileName);
                rtbDataGrid.Columns.Clear();
                rtbDataGrid.DataSource = rtb.bs;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            rtb.bs.Filter = string.Format("ID LIKE '%{0}%' OR Path LIKE '%{0}%'", textBox1.Text);
        }
    }
}
