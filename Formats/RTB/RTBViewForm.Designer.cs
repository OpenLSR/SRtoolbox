namespace SRtoolbox
{
    partial class RTBViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RTBViewForm));
            this.rtbDataGrid = new System.Windows.Forms.DataGridView();
            this.rtbLoadBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.assetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.assetPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.rtbDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbDataGrid
            // 
            this.rtbDataGrid.AllowUserToAddRows = false;
            this.rtbDataGrid.AllowUserToDeleteRows = false;
            this.rtbDataGrid.AllowUserToResizeRows = false;
            this.rtbDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.rtbDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.rtbDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.assetId,
            this.assetPath});
            this.rtbDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.rtbDataGrid.Location = new System.Drawing.Point(12, 41);
            this.rtbDataGrid.MultiSelect = false;
            this.rtbDataGrid.Name = "rtbDataGrid";
            this.rtbDataGrid.ReadOnly = true;
            this.rtbDataGrid.RowHeadersVisible = false;
            this.rtbDataGrid.RowHeadersWidth = 21;
            this.rtbDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.rtbDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rtbDataGrid.ShowEditingIcon = false;
            this.rtbDataGrid.Size = new System.Drawing.Size(572, 371);
            this.rtbDataGrid.TabIndex = 0;
            // 
            // rtbLoadBtn
            // 
            this.rtbLoadBtn.Location = new System.Drawing.Point(12, 12);
            this.rtbLoadBtn.Name = "rtbLoadBtn";
            this.rtbLoadBtn.Size = new System.Drawing.Size(572, 23);
            this.rtbLoadBtn.TabIndex = 1;
            this.rtbLoadBtn.Text = "Load RTB";
            this.rtbLoadBtn.UseVisualStyleBackColor = true;
            this.rtbLoadBtn.Click += new System.EventHandler(this.rtbLoadBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 418);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(572, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // assetId
            // 
            this.assetId.FillWeight = 20.19841F;
            this.assetId.HeaderText = "ID";
            this.assetId.MinimumWidth = 40;
            this.assetId.Name = "assetId";
            this.assetId.ReadOnly = true;
            // 
            // assetPath
            // 
            this.assetPath.FillWeight = 79.27875F;
            this.assetPath.HeaderText = "Path";
            this.assetPath.Name = "assetPath";
            this.assetPath.ReadOnly = true;
            // 
            // RTBViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.rtbLoadBtn);
            this.Controls.Add(this.rtbDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RTBViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stunt Rally Toolbox - RTB Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.rtbDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView rtbDataGrid;
        private System.Windows.Forms.Button rtbLoadBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn assetId;
        private System.Windows.Forms.DataGridViewTextBoxColumn assetPath;
    }
}