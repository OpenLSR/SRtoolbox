using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace SRtoolbox
{
    public partial class MainForm : Form
    {
        public string tagline = "The first release!";
        public string version = "0.1.0";

        public MainForm()
        {
            InitializeComponent();

            

            statusState.Text = "Idle";
        }

        private void RFx_Unpack_Click(object sender, EventArgs e)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open RFH File";
            openFileDialog.Filter = "Resource File Header (*.rfh)|*.rfh";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string text = Path.GetDirectoryName(openFileDialog.FileName) + "\\" + Path.GetFileNameWithoutExtension(openFileDialog.FileName) + ".rfd";
                if (File.Exists(text))
                {
                    RFH rfh = new RFH();
                    rfh.load(openFileDialog.FileName, text);
                    rfh.extractAllFiles(Path.GetFileNameWithoutExtension(openFileDialog.FileName));
                    statusBar.Style = ProgressBarStyle.Continuous;
                    statusState.Text = "Idle";
                    MessageBox.Show("Successfully unpacked "+Path.GetFileName(openFileDialog.FileName)+".", "RFH/RFD Unpacker");
                }
                else
                {
                    statusBar.Style = ProgressBarStyle.Continuous;
                    MessageBox.Show("Error: Could not locate the data file for this RFH file.", "RFH/RFD Unpacker", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            statusBar.Style = ProgressBarStyle.Continuous;
        }

        private void RFx_Unpack_Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("About: \nUnpacks a Resource File Data file using the associated Resource File Header file into a directory.\n\nWritten by:\nCyrem, Yellowberry", "RFH/RFD Unpacker",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TRK_to_XTK_Click(object sender, EventArgs e)
        {

        }

        private void trkxtkHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("About: \nManages the loading and conversion of TRK and XTK files.\n\nWritten by:\nYellowberry", "TRK/XTK Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void compressionLevel_Click(object sender, EventArgs e)
        {
            compressionBar.Value = 0;
        }

        private void compressionBar_ValueChanged(object sender, EventArgs e)
        {
            compressionLevel.Text = "Compression: " + compressionBar.Value;
        }

        private void xbfobjHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("About: \nConverts LSR binary models into OBJ format (very experimental and buggy).\n\nWritten by:\nSluicer, Yellowberry", "XBF to OBJ Converter", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RFHPacker_Click(object sender, EventArgs e)
        {
            RFH rfh = new RFH();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open TRK File";
            openFileDialog.Filter = "Resource File Header (*.trk)|*.trk";
            openFileDialog.ShowDialog();
            byte[] packed = rfh.packFile(openFileDialog.FileName,"", 0,false);
            FileStream fileStream = new FileStream(openFileDialog.FileName+".zl", FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(packed);
            binaryWriter.Close();
            fileStream.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                statusBar.Style = ProgressBarStyle.Marquee;
                statusState.Text = "Working...";
                XBFtool xBFtool = new XBFtool();
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Open XBF File";
                ofd.Filter = "XBF file (*.xbf)|*.xbf";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "Please select a folder for the converted files.";
                    fbd.SelectedPath = Path.GetDirectoryName(ofd.FileName);
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        xBFtool.Setup(ofd.FileName);
                        xBFtool.Extract(fbd.SelectedPath);
                        statusBar.Style = ProgressBarStyle.Continuous;
                        statusState.Text = "Idle";
                        MessageBox.Show("Successfully converted " + Path.GetFileName(ofd.FileName) + ".", "XBF to OBJ Converter");
                    }
                }
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show("Not yet surported: " + ex.Message, "XBF to OBJ Converter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.ToString(), "XBF to OBJ Converter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RTBViewForm rvf = new RTBViewForm();
            rvf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("About: \nAllows you to view the contents of the MOTO.rtb file, which contains the asset mapping for the game.\n\nWritten by:\nYellowberry", "RTB Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            MessageBox.Show("About: \nAllows you to edit the parameters of the AI characters.\n\nWritten by:\nYellowberry", "AI Data Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void corneringHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("About: \nAllows you to edit the AI cornering data of each theme.\n\nWritten by:\nYellowberry", "Cornering Data Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mdfeditBtn_Click(object sender, EventArgs e)
        {
            MDFEditForm mef = new MDFEditForm();
            mef.Show();
        }

        private void githubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/YellowberryHN/SRtoolbox");
        }

        private void versionTagline_Layout(object sender, LayoutEventArgs e)
        {
            versionTagline.Text = "Version " + version + " - " + tagline;
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("ok boomer", "a message from yellowberry");
        }
    }
}
