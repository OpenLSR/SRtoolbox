namespace SRtoolbox
{
    partial class MVDEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MVDEditorForm));
            this.videoFrame = new System.Windows.Forms.PictureBox();
            this.frameTimer = new System.Windows.Forms.Timer(this.components);
            this.btnStartStop = new System.Windows.Forms.Button();
            this.videoFrameCounter = new System.Windows.Forms.Label();
            this.btnSeekForward = new System.Windows.Forms.Button();
            this.btnSeekBack = new System.Windows.Forms.Button();
            this.videoFrameReal = new System.Windows.Forms.PictureBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnExportAll = new System.Windows.Forms.Button();
            this.seekBar = new System.Windows.Forms.HScrollBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.loadMvdBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.videoFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.videoFrameReal)).BeginInit();
            this.SuspendLayout();
            // 
            // videoFrame
            // 
            this.videoFrame.Location = new System.Drawing.Point(12, 12);
            this.videoFrame.Name = "videoFrame";
            this.videoFrame.Size = new System.Drawing.Size(300, 300);
            this.videoFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.videoFrame.TabIndex = 0;
            this.videoFrame.TabStop = false;
            // 
            // frameTimer
            // 
            this.frameTimer.Tick += new System.EventHandler(this.frameTimer_Tick);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(343, 12);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(50, 23);
            this.btnStartStop.TabIndex = 1;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // videoFrameCounter
            // 
            this.videoFrameCounter.AutoSize = true;
            this.videoFrameCounter.Location = new System.Drawing.Point(318, 196);
            this.videoFrameCounter.Name = "videoFrameCounter";
            this.videoFrameCounter.Size = new System.Drawing.Size(83, 13);
            this.videoFrameCounter.TabIndex = 2;
            this.videoFrameCounter.Text = "Frame: 999/999";
            // 
            // btnSeekForward
            // 
            this.btnSeekForward.Location = new System.Drawing.Point(399, 12);
            this.btnSeekForward.Name = "btnSeekForward";
            this.btnSeekForward.Size = new System.Drawing.Size(19, 23);
            this.btnSeekForward.TabIndex = 3;
            this.btnSeekForward.Text = ">";
            this.btnSeekForward.UseVisualStyleBackColor = true;
            this.btnSeekForward.Click += new System.EventHandler(this.btnSeekForward_Click);
            // 
            // btnSeekBack
            // 
            this.btnSeekBack.Location = new System.Drawing.Point(318, 12);
            this.btnSeekBack.Name = "btnSeekBack";
            this.btnSeekBack.Size = new System.Drawing.Size(19, 23);
            this.btnSeekBack.TabIndex = 4;
            this.btnSeekBack.Text = "<";
            this.btnSeekBack.UseVisualStyleBackColor = true;
            this.btnSeekBack.Click += new System.EventHandler(this.btnSeekBack_Click);
            // 
            // videoFrameReal
            // 
            this.videoFrameReal.Location = new System.Drawing.Point(318, 212);
            this.videoFrameReal.Name = "videoFrameReal";
            this.videoFrameReal.Size = new System.Drawing.Size(100, 100);
            this.videoFrameReal.TabIndex = 5;
            this.videoFrameReal.TabStop = false;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(424, 260);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(48, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExportAll
            // 
            this.btnExportAll.Location = new System.Drawing.Point(424, 289);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.Size = new System.Drawing.Size(63, 23);
            this.btnExportAll.TabIndex = 7;
            this.btnExportAll.Text = "Export All";
            this.btnExportAll.UseVisualStyleBackColor = true;
            this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // seekBar
            // 
            this.seekBar.Location = new System.Drawing.Point(12, 325);
            this.seekBar.Name = "seekBar";
            this.seekBar.Size = new System.Drawing.Size(779, 17);
            this.seekBar.TabIndex = 8;
            this.seekBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.seekBar_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Load frames";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(713, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "dump frame";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(713, 70);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "save .mvd";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(424, 219);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 35);
            this.button4.TabIndex = 12;
            this.button4.Text = "replace frame";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // loadMvdBtn
            // 
            this.loadMvdBtn.Location = new System.Drawing.Point(318, 170);
            this.loadMvdBtn.Name = "loadMvdBtn";
            this.loadMvdBtn.Size = new System.Drawing.Size(75, 23);
            this.loadMvdBtn.TabIndex = 13;
            this.loadMvdBtn.Text = "Load .mvd";
            this.loadMvdBtn.UseVisualStyleBackColor = true;
            this.loadMvdBtn.Click += new System.EventHandler(this.loadMvdBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(321, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "this UI is awful. i will fix it later.";
            // 
            // MVDEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 352);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadMvdBtn);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.seekBar);
            this.Controls.Add(this.btnExportAll);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.videoFrameReal);
            this.Controls.Add(this.btnSeekBack);
            this.Controls.Add(this.btnSeekForward);
            this.Controls.Add(this.videoFrameCounter);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.videoFrame);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MVDEditorForm";
            this.Text = "Stunt Rally Toolbox - MotoVideo Editor";
            this.Load += new System.EventHandler(this.MVDEditorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.videoFrameReal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox videoFrame;
        private System.Windows.Forms.Timer frameTimer;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label videoFrameCounter;
        private System.Windows.Forms.Button btnSeekForward;
        private System.Windows.Forms.Button btnSeekBack;
        private System.Windows.Forms.PictureBox videoFrameReal;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnExportAll;
        private System.Windows.Forms.HScrollBar seekBar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button loadMvdBtn;
        private System.Windows.Forms.Label label1;
    }
}