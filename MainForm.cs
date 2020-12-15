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
using LSRutil;
using LSRutil.TRK;
using LSRutil.RF;
using Microsoft.WindowsAPICodePack.Dialogs;
using LSRutil.AI;
using System.Runtime.InteropServices;
using Pfim;
using System.Drawing.Imaging;

namespace SRtoolbox
{
    public partial class MainForm : Form
    {
        private string githubLinkURL = "https://github.com/YellowberryHN/SRtoolbox";

        public string tagline = "What if we used a library?";
        public string version = "2.0.0";

        public Track track;

        private Stopwatch timer = new Stopwatch();

        public void checkFile(string file)
        {
            string ext = Path.GetExtension(file);

            if (ext == ".trk")
            {
                tabControl.SelectedIndex = 1; // Track tab
                trkLoad(file);
            }
            else if (ext == ".mvd")
            {
                tabControl.SelectedIndex = 2; // Moto Video tab
                //mvdLoad(file);
            }
            else if (ext == ".bin") aiLoad(file);
            else if (ext == ".dat") cornLoad(file);
            else if (ext == ".rfh" || ext == ".rfd") rfxUnpack(file);
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

        protected void timerResult()
        {
            timer.Stop();
            if (debugOptionsTiming.Checked) MessageBox.Show(string.Format("Operation took {0}ms.", timer.ElapsedMilliseconds), "(DEBUG) Internal Timer");
        }

        public void rfxUnpack(string filename)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            timer.Restart();

            string resourcePath = Path.GetDirectoryName(filename) + "/" + Path.GetFileNameWithoutExtension(filename) + ".rfd";
            if (File.Exists(resourcePath))
            {
                var reader = new RfReader();
                var resArchive = reader.ReadArchive(filename);
                resArchive.ExtractAllFiles(Path.GetFileNameWithoutExtension(filename));
                MessageBox.Show("Successfully unpacked " + Path.GetFileName(filename) + ".", "RFH/RFD Unpacker");
            }
            else
            {
                MessageBox.Show("Could not locate the data file for this RFH file.", "RFH/RFD Unpacker", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            timerResult();

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        public void rfxPack(string directory, string location, bool compression)
        {
            timer.Restart();

            var resArchive = new ResourceArchive();

            resArchive.GetRelativeFiles(directory, true, compression);

            try
            {
                var writer = new RfWriter();
                writer.WriteArchive(resArchive, location);
            }
            catch (IOException e)
            {
                MessageBox.Show("There was an error saving the archive. Does it already exist?\n\nException: "+e.Message, "RFH/RFD Unpacker", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            timerResult();
        }

        private void RFx_Unpack_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.Title = "Open RFH File";
            dialog.Filters.Add(new CommonFileDialogFilter("Resource File Header", "*.rfh"));
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            rfxUnpack(dialog.FileName);
        }

        private void RFx_Pack_Click(object sender, EventArgs e)
        {

            var dialog = new CommonOpenFileDialog { IsFolderPicker = true };
            dialog.Title = "Select folder to pack";
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            var saveDialog = new CommonSaveFileDialog { InitialDirectory = dialog.FileName };
            saveDialog.DefaultFileName = Path.GetFileName(dialog.FileName)+".rfh";
            saveDialog.Title = "Save resource archive as";
            saveDialog.Filters.Add(new CommonFileDialogFilter("Resource File Header", "*.rfh"));
            if (saveDialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            rfxPack(dialog.FileName, saveDialog.FileName, rfxPackerCompression.Checked);
        }

        private void RFx_Unpack_Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Unpacks a resource archive into a directory.\n\nWritten by:\nCyrem, Yellowberry", "RFH/RFD Unpacker",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RFx_Pack_Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Packs a resource archive from the contents of a directory.\n\nWritten by:\nYellowberry", "RFH/RFD Packer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void trkxtkHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Manages the loading and conversion of TRK and XTK files.\n\nWritten by:\nYellowberry", "TRK/XTK Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void xbfobjHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Converts LSR binary models into OBJ format (very experimental and buggy).\n\nWritten by:\nSluicer, Yellowberry", "XBF to OBJ Converter", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void xbfobjConvert(string filename, string location)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            timer.Restart();

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

            timerResult();

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.Title = "Open XBF file";
            dialog.Filters.Add(new CommonFileDialogFilter("Xanadu Binary File", "*.xbf"));
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            var folderDialog = new CommonOpenFileDialog { IsFolderPicker = true };
            folderDialog.Title = "Select folder to extract files";
            if (folderDialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            xbfobjConvert(dialog.FileName, folderDialog.FileName);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RTBViewForm rvf = new RTBViewForm();
            rvf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Allows you to view the contents of the MOTO.rtb file, which contain the table of resources for the game.\n\nWritten by:\nYellowberry", "RTB Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Process.Start(githubLinkURL);
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

            GCHandle handle;

            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            timer.Restart();

            var reader = new TrkReader();
            track = reader.ReadTrack(filename);
            trackLabel.Text = "Track: " + Path.GetFileName(filename);
            try
            {
                var image = Pfim.Pfim.FromFile(Path.Combine(Path.GetDirectoryName(filename), "Images", Path.GetFileNameWithoutExtension(filename) + ".tga"));
                handle = GCHandle.Alloc(image.Data, GCHandleType.Pinned);
                var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
                trackImage.Image = new Bitmap(image.Width, image.Height, image.Stride, PixelFormat.Format24bppRgb, ptr);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("DEBUG: No image found for " + Path.GetFileName(filename));
            }
            trackMode.Text = "Mode: " + track.size;
            trackSize.Text = "Size: " + (int)Math.Sqrt(track.GetMaxElements());
            trackTheme.Text = "Theme: " + track.theme;
            //trackCompat.Text = "Compatibility: " + trackFile.compat.ToString();

            timerResult();

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void trkxtkLoad_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.Filters.Add(new CommonFileDialogFilter("Tracks", "*.trk;*.xtk"));
            dialog.Filters.Add(new CommonFileDialogFilter("Legacy track file", "*.trk"));
            dialog.Filters.Add(new CommonFileDialogFilter("eXtensible track file", "*.xtk"));
            dialog.Title = "Open track file";
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            trkLoad(dialog.FileName);
        }

        public void cornLoad(string filename)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            timer.Restart();

            var cor = new CorneringData();
            cor.Load(filename);

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

            timerResult();

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void cornLoadBtn_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.Filters.Add(new CommonFileDialogFilter("Cornering data", "*.dat"));
            dialog.Title = "Open cornering data file";
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            cornLoad(dialog.FileName);
        }

        public void cornSave(string filename)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            timer.Restart();

            var cor = new CorneringData();

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

            cor.Save(filename);

            timerResult();

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void cornSaveBtn_Click(object sender, EventArgs e)
        {
            var dialog = new CommonSaveFileDialog();
            dialog.Title = "Save cornering data file as";
            dialog.Filters.Add(new CommonFileDialogFilter("Cornering data", "*.dat"));
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            cornSave(dialog.FileName);
        }

        public void aiLoad(string filename)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            timer.Restart();

            var ai = new AIData();
            ai.Load(filename);

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

            timerResult();

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void aiLoadBtn_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.Filters.Add(new CommonFileDialogFilter("AI data", "*.bin"));
            dialog.Title = "Open AI data file";
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            aiLoad(dialog.FileName);
        }

        public void aiSave(string filename)
        {
            statusBar.Style = ProgressBarStyle.Marquee;
            statusState.Text = "Working...";

            timer.Restart();

            var ai = new AIData();

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

            ai.Save(filename);

            timerResult();

            statusBar.Style = ProgressBarStyle.Continuous;
            statusState.Text = "Idle";
        }

        private void aiSaveBtn_Click(object sender, EventArgs e)
        {
            var dialog = new CommonSaveFileDialog();
            dialog.Title = "Save AI data file as";
            dialog.Filters.Add(new CommonFileDialogFilter("AI data", "*.bin"));
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            aiSave(dialog.FileName);
        }

        private void githubLink_Layout(object sender, LayoutEventArgs e)
        {
            githubLink.Text = githubLinkURL;
        }

        private void mvdeditBtn_Click(object sender, EventArgs e)
        {
            MVDEditorForm mvdef = new MVDEditorForm();
            mvdef.Show();
        }
    }
}
