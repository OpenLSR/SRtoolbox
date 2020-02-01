using Pfim;
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
        public string tagline = "It's all in the timing...";
        public string version = "0.1.1";

        public TRKFile trackFile;

        public void checkFile(string file)
        {
            string ext = Path.GetExtension(file);

            if (ext == ".trk")
            {
                tabControl.SelectedIndex = 1; // Track tab
                trkLoad(file);
            }
            else if (ext == ".bin") aiLoad(file);
            else if (ext == ".dat") cornLoad(file);
            else if (ext == ".rfh") rfxUnpack(file);
        }

        public MainForm()
        {
            InitializeComponent();

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";

            if(Program.arguments.Length > 0)
            {
                foreach (var arg in Program.arguments)
                {
                    checkFile(arg);
                } 
            }
        }

        public void rfxUnpack(string filename)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            string text = Path.GetDirectoryName(filename) + "\\" + Path.GetFileNameWithoutExtension(filename) + ".rfd";
            if (File.Exists(text))
            {
                RFH rfh = new RFH();
                rfh.load(filename, text);
                rfh.extractAllFiles(Path.GetFileNameWithoutExtension(filename));
                MessageBox.Show("Successfully unpacked " + Path.GetFileName(filename) + ".", "RFH/RFD Unpacker");
            }
            else
            {
                MessageBox.Show("Error: Could not locate the data file for this RFH file.", "RFH/RFD Unpacker", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void RFx_Unpack_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open RFH File";
            openFileDialog.Filter = "Resource File Header (*.rfh)|*.rfh";
            if (openFileDialog.ShowDialog(this) != DialogResult.OK) { return; }

            rfxUnpack(openFileDialog.FileName);
        }

        private void RFx_Unpack_Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Unpacks a Resource File Data file using the associated Resource File Header file into a directory.\n\nWritten by:\nCyrem, Yellowberry", "RFH/RFD Unpacker",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void trkxtkHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Manages the loading and conversion of TRK and XTK files.\n\nWritten by:\nYellowberry", "TRK/XTK Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            MessageBox.Show("Converts LSR binary models into OBJ format (very experimental and buggy).\n\nWritten by:\nSluicer, Yellowberry", "XBF to OBJ Converter", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RFHPacker_Click(object sender, EventArgs e)
        {
            RFH rfh = new RFH();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open TRK File";
            openFileDialog.Filter = "Resource File Header (*.trk)|*.trk";
            openFileDialog.ShowDialog();
            byte[] packed = rfh.packFile(openFileDialog.FileName,string.Empty, 0,false);
            FileStream fileStream = new FileStream(openFileDialog.FileName+".zl", FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(packed);
            binaryWriter.Close();
            fileStream.Close();
        }

        public void xbfobjConvert(string filename, string location)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";
            try
            {
                XBFtool xbf = new XBFtool();

                xbf.Setup(filename);
                xbf.Extract(location);
                MessageBox.Show("Successfully converted " + Path.GetFileName(filename) + ".", "XBF to OBJ Converter");
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show("Not yet surported: " + ex.Message, "XBF to OBJ Converter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.ToString(), "XBF to OBJ Converter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open XBF File";
            ofd.Filter = "XBF file (*.xbf)|*.xbf";
            if (ofd.ShowDialog(this) != DialogResult.OK) { return; }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Please select a folder for the converted files.";
            fbd.SelectedPath = Path.GetDirectoryName(ofd.FileName);
            if (fbd.ShowDialog(this) != DialogResult.OK) { return; }

            xbfobjConvert(ofd.FileName, fbd.SelectedPath);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RTBViewForm rvf = new RTBViewForm();
            rvf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Allows you to view the contents of the MOTO.rtb file, which contains the asset mapping for the game.\n\nWritten by:\nYellowberry", "RTB Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Allows you to edit the parameters of the AI characters.\n\nWritten by:\nYellowberry", "AI Data Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void corneringHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Allows you to edit the AI cornering data of each theme.\n\nWritten by:\nYellowberry", "Cornering Data Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void trkLoad(string filename)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            TRK trk = new TRK();
            trackFile = trk.load(filename);
            trackLabel.Text = "Track: " + Path.GetFileName(filename);
            trackImage.Image = trackFile.image;
            trackMode.Text = "Mode: " + trackFile.type.ToString();
            trackSize.Text = "Size: " + trackFile.size.ToString(true);
            trackTheme.Text = "Theme: " + trackFile.theme.ToString();
            trackCompat.Text = "Compatibility: " + trackFile.compat.ToString();

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void trkxtkLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open track file";
            ofd.Filter = "Track files (*.trk, *.xtk)|*.trk;*.xtk";
            if (ofd.ShowDialog(this) != DialogResult.OK) { return; }

            trkLoad(ofd.FileName);
        }

        public void cornLoad(string filename)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            CorneringFile cor;
            Cornering cornering = new Cornering();

            cor = cornering.load(filename);

            corDataGroup.Text = "Cornering data editor ( " + Path.GetFileName(filename) + " )";
            corSmall.Value = (decimal)cor.Small;
            corBig.Value = (decimal)cor.Big;
            corSmallSmall_Same.Value = (decimal)cor.SmallSmall_Same;
            corSmallSmall_Same.Value = (decimal)cor.SmallSmall_Same;
            corSmallSmall_Opp.Value = (decimal)cor.SmallSmall_Opp;
            corBigBig_Same.Value = (decimal)cor.BigBig_Same;
            corBigBig_Opp.Value = (decimal)cor.BigBig_Opp;
            corBigSmall_Same.Value = (decimal)cor.BigSmall_Same;
            corSmallBig_Same.Value = (decimal)cor.SmallBig_Same;
            corBigSmall_Opp.Value = (decimal)cor.BigSmall_Opp;
            corSmallBig_Opp.Value = (decimal)cor.SmallBig_Opp;

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void cornLoadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open cornering data file";
            ofd.Filter = "Cornering data (*.dat)|*.dat";
            if (ofd.ShowDialog(this) != DialogResult.OK) { return; }

            cornLoad(ofd.FileName);
        }

        public void cornSave(string filename)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            CorneringFile cor = new CorneringFile();
            Cornering cornering = new Cornering();

            corDataGroup.Text = "Cornering data editor ( " + Path.GetFileName(filename) + " )";
            cor.Small = (float)corSmall.Value;
            cor.Big = (float)corBig.Value;
            cor.SmallSmall_Same = (float)corSmallSmall_Same.Value;
            cor.SmallSmall_Same = (float)corSmallSmall_Same.Value;
            cor.SmallSmall_Opp = (float)corSmallSmall_Opp.Value;
            cor.BigBig_Same = (float)corBigBig_Same.Value;
            cor.BigBig_Opp = (float)corBigBig_Opp.Value;
            cor.BigSmall_Same = (float)corBigSmall_Same.Value;
            cor.SmallBig_Same = (float)corSmallBig_Same.Value;
            cor.BigSmall_Opp = (float)corBigSmall_Opp.Value;
            cor.SmallBig_Opp = (float)corSmallBig_Opp.Value;

            cornering.save(filename, cor);

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void cornSaveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save cornering data file as";
            sfd.Filter = "Cornering data (*.dat)|*.dat";
            if (sfd.ShowDialog(this) != DialogResult.OK) { return; }

            cornSave(sfd.FileName);
        }

        public void aiLoad(string filename)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            AIFile ai;
            AIData aidata = new AIData();

            ai = aidata.load(filename);

            aiDataGroup.Text = "Car AI data editor ( " + Path.GetFileName(filename) + " )";

            aiRacingLine.Value = ai.RacingLine;
            aiBraking.Value = ai.Braking;
            aiOvertaking.Value = ai.Overtaking;
            aiSpeed.Value = ai.Speed;
            aiReflex.Value = ai.Reflex;
            aiBlocking.Value = ai.Blocking;
            aiCutsCorners.Value = ai.CutsCorners;
            aiIntelligence.Value = ai.Intelligence;
            aiCraziness.Value = ai.Craziness;

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void aiLoadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open AI data file";
            ofd.Filter = "AI data (*.bin)|*.bin";
            if (ofd.ShowDialog(this) != DialogResult.OK) { return; }

            aiLoad(ofd.FileName);
        }

        public void aiSave(string filename)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            AIFile ai = new AIFile();
            AIData aidata = new AIData();

            aiDataGroup.Text = "Car AI data editor ( " + Path.GetFileName(filename) + " )";

            ai.RacingLine = (ushort)aiRacingLine.Value;
            ai.Braking = (ushort)aiBraking.Value;
            ai.Overtaking = (ushort)aiOvertaking.Value;
            ai.Speed = (ushort)aiSpeed.Value;
            ai.Reflex = (ushort)aiReflex.Value;
            ai.Blocking = (byte)aiBlocking.Value;
            ai.CutsCorners = (byte)aiCutsCorners.Value;
            ai.Intelligence = (byte)aiIntelligence.Value;
            ai.Craziness = (byte)aiCraziness.Value;

            aidata.save(filename, ai);

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void aiSaveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save AI data file as";
            sfd.Filter = "AI data (*.bin)|*.bin";
            if (sfd.ShowDialog(this) != DialogResult.OK) { return; }

            aiSave(sfd.FileName);
        }
    }
}
