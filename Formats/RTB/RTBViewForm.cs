using LSRutil.RTB;
using Microsoft.WindowsAPICodePack.Dialogs;
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
        public RTBViewForm()
        {
            InitializeComponent();
        }

        private void rtbLoadBtn_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.Filters.Add(new CommonFileDialogFilter("Resource table file", "*.rtb"));
            dialog.Title = "Open resource table file";
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                GenTable(dialog.FileName);
                rtbDataGrid.Columns.Clear();
                rtbDataGrid.DataSource = bs;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bs.Filter = string.Format("ID LIKE '%{0}%' OR Path LIKE '%{0}%'", textBox1.Text);
        }

        DataTable table = new DataTable();
        public BindingSource bs = new BindingSource();
        /// <summary>
        /// This example method generates a DataTable.
        /// </summary>
        public void GenTable(string filename)
        {
            // Here we create a DataTable with four columns.
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Path", typeof(string));

            RtbReader reader = new RtbReader();
            var resTable = reader.ReadTable(filename);
            foreach (var item in resTable.contents)
            {
                table.Rows.Add(new object[] { string.Format("0x{0:X4}",item.Key), item.Value });
            }

            this.bs.DataSource = table;
        }
    }
}
