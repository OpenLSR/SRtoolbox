using LSRutil;
using LSRutil.MVD;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRtoolbox
{
    public partial class MVDEditorForm : Form
    {
        public MVDEditorForm()
        {
            InitializeComponent();
        }

        // The frame images.
        private List<Bitmap> bmpFrames;

        // The index of the current frame.
        private int frameNum = 0;

        private MotoVideo video;

        // Load the images.
        private void MVDEditorForm_Load(object sender, EventArgs e)
        {
            
            
            // Load the frames.
            bmpFrames = new List<Bitmap> { new Bitmap(100,100) };
            

            // Display the first frame.
            //setFrame(frameNum);
            frameTimer.Interval = 1000 / 20;
        }

        private bool setPlay(bool state)
        {
            frameTimer.Enabled = state;
            btnStartStop.Text = frameTimer.Enabled ? "Stop" : "Start";

            return frameTimer.Enabled;
        }

        public void setFrame(int num)
        {
            if(num<0) num = bmpFrames.Count-1;
            frameNum = num % bmpFrames.Count;
            seekBar.Value = frameNum;
            videoFrame.Image = bmpFrames[frameNum];
            videoFrameReal.Image = bmpFrames[frameNum];
            videoFrameCounter.Text = string.Format("Frame: {0}/{1}", frameNum, video.frames.Count - 1);
        }

        private void frameTimer_Tick(object sender, EventArgs e)
        {
            frameNum = ++frameNum % bmpFrames.Count;
            setFrame(frameNum);
        }

        // Start or stop the animation.
        private void btnStartStop_Click(object sender, EventArgs e)
        {
            setPlay(!frameTimer.Enabled);
        }

        private void btnSeekForward_Click(object sender, EventArgs e)
        {
            setPlay(false);
            setFrame(++frameNum);
        }

        private void btnSeekBack_Click(object sender, EventArgs e)
        {
            setPlay(false);
            setFrame(--frameNum);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            bmpFrames[frameNum].Save("export.png",ImageFormat.Png);
            MessageBox.Show("Exported to export.png!", "MotoVideo Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void seekBar_Scroll(object sender, ScrollEventArgs e)
        {
            setPlay(false);
            setFrame(seekBar.Value);
        }

        private void btnExportAll_Click(object sender, EventArgs e)
        {
            var dir = Directory.CreateDirectory($"export-{DateTime.Now.Ticks}");
            for (int i = 0; i < bmpFrames.Count; i++)
            {
                bmpFrames[i].Save($"{dir}/frame{i:d4}.png",ImageFormat.Png);
            }
            MessageBox.Show($"Exported {bmpFrames.Count} frames to {dir.Name}!", "MotoVideo Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog { IsFolderPicker = true };
            dialog.Title = "Select folder to load frames from";
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            var frameRoot = dialog.FileName;

            var loadedPaths = Directory.GetFiles(frameRoot, "frame*.png");

            if (loadedPaths.Length < 1) return;

            bmpFrames.Clear();

            foreach (var item in loadedPaths)
            {
                var img = new Bitmap(item);

                bmpFrames.Add(img.Clone(new Rectangle(new Point(0,0), img.Size), PixelFormat.Format16bppRgb565));
            }

            seekBar.Maximum = bmpFrames.Count;

            video = new MotoVideo(bmpFrames.Count, bmpFrames[0].Width, bmpFrames[0].Height);
            video.bitDepth = 16;

            setFrame(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dumpBitmap = (Bitmap)bmpFrames[frameNum].Clone();

            var bmpData = dumpBitmap.LockBits(new Rectangle(new Point(0, 0), dumpBitmap.Size), ImageLockMode.ReadOnly, PixelFormat.Format16bppRgb565);

            int numbytes = bmpData.Stride * dumpBitmap.Height;
            byte[] bytedata = new byte[numbytes];
            IntPtr ptr = bmpData.Scan0;

            Marshal.Copy(ptr, bytedata, 0, numbytes);

            var file = new FileStream("new.raw", FileMode.Create);
            using (var writer = new BinaryWriter(file))
            {
                writer.Write(bytedata);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var saveDialog = new CommonSaveFileDialog();
            saveDialog.Title = "Save video file as";
            saveDialog.Filters.Add(new CommonFileDialogFilter("MotoVideo File", "*.mvd"));
            if (saveDialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            foreach (var fr in bmpFrames)
            {
                var motoFrame = new MotoVideoFrame(video.width, video.height, 16);

                var dumpBitmap = (Bitmap)fr.Clone();

                var bmpData = dumpBitmap.LockBits(new Rectangle(new Point(0, 0), dumpBitmap.Size), ImageLockMode.ReadOnly, PixelFormat.Format16bppRgb565);

                int numbytes = bmpData.Stride * dumpBitmap.Height;
                motoFrame.bytes = new byte[numbytes];
                IntPtr ptr = bmpData.Scan0;

                Marshal.Copy(ptr, motoFrame.bytes, 0, numbytes);

                video.frames.Add(motoFrame);
            }

            var writer = new MvdWriter();
            writer.WriteVideo(video, saveDialog.FileName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.Title = "Open new frame";
            dialog.Filters.Add(new CommonFileDialogFilter("Image file", "*.png"));
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }
            var img = new Bitmap(dialog.FileName);

            if (img.Size != bmpFrames[frameNum].Size) MessageBox.Show(string.Format("Frame you are trying to replace is different size! (frame: {0}, image: {1})", img.Size.ToString(), bmpFrames[frameNum].Size.ToString()), "error");
            else bmpFrames[frameNum] = img.Clone(new Rectangle(new Point(0, 0), img.Size), PixelFormat.Format16bppRgb565);

            setFrame(frameNum);
        }

        private void loadMvdBtn_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.Title = "Open video file";
            dialog.Filters.Add(new CommonFileDialogFilter("MotoVideo file", "*.mvd"));
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) { return; }

            var reader = new MvdReader();
            video = reader.ReadVideo(dialog.FileName);

            seekBar.Maximum = video.frames.Count - 1;

            videoFrameReal.Size = new Size(video.width, video.height);

            bmpFrames.Clear();

            for (int i = 0; i < video.frames.Count; i++)
            {
                // Create empty Bitmep and inject byte arrays data into bitmap's data area
                Bitmap bmp = new Bitmap(video.width, video.height, PixelFormat.Format16bppRgb565);
                // Lock the bitmap's bits.  
                Rectangle rect = new Rectangle(0, 0, video.width, video.height);
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format16bppRgb565);
                IntPtr ptrToFirstPixel = bmpData.Scan0;

                Marshal.Copy(video.frames[i].bytes, 0, ptrToFirstPixel, video.frames[0].bytes.Length);  // *** Use padded buffer2 instead of buffer1
                bmp.UnlockBits(bmpData);
                bmpFrames.Add(bmp);
            }

            setFrame(0);
        }
    }
}
