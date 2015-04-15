namespace FolderSearcher
{
    partial class frmOptions
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnDismiss = new System.Windows.Forms.Button();
            this.chkExcludeFilesLargerThan = new System.Windows.Forms.CheckBox();
            this.numKB = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.chkExcludeFilesOfTheseTypes = new System.Windows.Forms.CheckBox();
            this.txtExcludeFileTypes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDefaultType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDefaultSearchString = new System.Windows.Forms.TextBox();
            this.chkShowProgress = new System.Windows.Forms.CheckBox();
            this.chkSearchRecursively = new System.Windows.Forms.CheckBox();
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numKB)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 252);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnDismiss
            // 
            this.btnDismiss.Location = new System.Drawing.Point(93, 252);
            this.btnDismiss.Name = "btnDismiss";
            this.btnDismiss.Size = new System.Drawing.Size(75, 23);
            this.btnDismiss.TabIndex = 5;
            this.btnDismiss.Text = "Cancel";
            this.btnDismiss.UseVisualStyleBackColor = true;
            this.btnDismiss.Click += new System.EventHandler(this.btnDismiss_Click);
            // 
            // chkExcludeFilesLargerThan
            // 
            this.chkExcludeFilesLargerThan.AutoSize = true;
            this.chkExcludeFilesLargerThan.Location = new System.Drawing.Point(12, 13);
            this.chkExcludeFilesLargerThan.Name = "chkExcludeFilesLargerThan";
            this.chkExcludeFilesLargerThan.Size = new System.Drawing.Size(149, 17);
            this.chkExcludeFilesLargerThan.TabIndex = 6;
            this.chkExcludeFilesLargerThan.Text = "Exclude Files Larger Than";
            this.chkExcludeFilesLargerThan.UseVisualStyleBackColor = true;
            // 
            // numKB
            // 
            this.numKB.Location = new System.Drawing.Point(167, 10);
            this.numKB.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numKB.Name = "numKB";
            this.numKB.Size = new System.Drawing.Size(51, 20);
            this.numKB.TabIndex = 7;
            this.numKB.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "KB";
            // 
            // chkExcludeFilesOfTheseTypes
            // 
            this.chkExcludeFilesOfTheseTypes.AutoSize = true;
            this.chkExcludeFilesOfTheseTypes.Location = new System.Drawing.Point(12, 117);
            this.chkExcludeFilesOfTheseTypes.Name = "chkExcludeFilesOfTheseTypes";
            this.chkExcludeFilesOfTheseTypes.Size = new System.Drawing.Size(165, 17);
            this.chkExcludeFilesOfTheseTypes.TabIndex = 8;
            this.chkExcludeFilesOfTheseTypes.Text = "Exclude Files of These Types";
            this.chkExcludeFilesOfTheseTypes.UseVisualStyleBackColor = true;
            // 
            // txtExcludeFileTypes
            // 
            this.txtExcludeFileTypes.Location = new System.Drawing.Point(32, 140);
            this.txtExcludeFileTypes.Multiline = true;
            this.txtExcludeFileTypes.Name = "txtExcludeFileTypes";
            this.txtExcludeFileTypes.Size = new System.Drawing.Size(213, 40);
            this.txtExcludeFileTypes.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Default File Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtDefaultType
            // 
            this.txtDefaultType.Location = new System.Drawing.Point(102, 189);
            this.txtDefaultType.Name = "txtDefaultType";
            this.txtDefaultType.Size = new System.Drawing.Size(32, 20);
            this.txtDefaultType.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Default Search String";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtDefaultSearchString
            // 
            this.txtDefaultSearchString.Location = new System.Drawing.Point(123, 218);
            this.txtDefaultSearchString.Name = "txtDefaultSearchString";
            this.txtDefaultSearchString.Size = new System.Drawing.Size(85, 20);
            this.txtDefaultSearchString.TabIndex = 10;
            // 
            // chkShowProgress
            // 
            this.chkShowProgress.AutoSize = true;
            this.chkShowProgress.Location = new System.Drawing.Point(12, 39);
            this.chkShowProgress.Name = "chkShowProgress";
            this.chkShowProgress.Size = new System.Drawing.Size(197, 17);
            this.chkShowProgress.TabIndex = 8;
            this.chkShowProgress.Text = "Show Progress (faster if unchecked)";
            this.chkShowProgress.UseVisualStyleBackColor = true;
            // 
            // chkSearchRecursively
            // 
            this.chkSearchRecursively.AutoSize = true;
            this.chkSearchRecursively.Location = new System.Drawing.Point(12, 65);
            this.chkSearchRecursively.Name = "chkSearchRecursively";
            this.chkSearchRecursively.Size = new System.Drawing.Size(113, 17);
            this.chkSearchRecursively.TabIndex = 8;
            this.chkSearchRecursively.Text = "Search Subfolders";
            this.chkSearchRecursively.UseVisualStyleBackColor = true;
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Location = new System.Drawing.Point(12, 91);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(162, 17);
            this.chkCaseSensitive.TabIndex = 8;
            this.chkCaseSensitive.Text = "Searches are Case-Sensitive";
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 288);
            this.Controls.Add(this.txtDefaultSearchString);
            this.Controls.Add(this.txtDefaultType);
            this.Controls.Add(this.txtExcludeFileTypes);
            this.Controls.Add(this.chkCaseSensitive);
            this.Controls.Add(this.chkSearchRecursively);
            this.Controls.Add(this.chkShowProgress);
            this.Controls.Add(this.chkExcludeFilesOfTheseTypes);
            this.Controls.Add(this.numKB);
            this.Controls.Add(this.chkExcludeFilesLargerThan);
            this.Controls.Add(this.btnDismiss);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOK);
            this.Name = "frmOptions";
            this.Text = "Search Options";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numKB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnDismiss;
        private System.Windows.Forms.CheckBox chkExcludeFilesLargerThan;
        private System.Windows.Forms.NumericUpDown numKB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkExcludeFilesOfTheseTypes;
        private System.Windows.Forms.TextBox txtExcludeFileTypes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDefaultType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDefaultSearchString;
        private System.Windows.Forms.CheckBox chkShowProgress;
        private System.Windows.Forms.CheckBox chkSearchRecursively;
        private System.Windows.Forms.CheckBox chkCaseSensitive;
    }
}