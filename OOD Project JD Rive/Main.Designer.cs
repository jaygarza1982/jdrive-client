namespace OOD_Project_JD_Rive
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lboxDirectories = new System.Windows.Forms.ListBox();
            this.folderBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.btnAddDir = new System.Windows.Forms.Button();
            this.lboxUpdates = new System.Windows.Forms.ListBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "teststset";
            this.notifyIcon.BalloonTipTitle = "tttttttttttttttttt";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "JD Rive";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // lboxDirectories
            // 
            this.lboxDirectories.FormattingEnabled = true;
            this.lboxDirectories.Location = new System.Drawing.Point(12, 28);
            this.lboxDirectories.Name = "lboxDirectories";
            this.lboxDirectories.Size = new System.Drawing.Size(381, 82);
            this.lboxDirectories.TabIndex = 0;
            this.lboxDirectories.DoubleClick += new System.EventHandler(this.lboxDirectories_DoubleClick);
            // 
            // btnAddDir
            // 
            this.btnAddDir.Location = new System.Drawing.Point(13, 117);
            this.btnAddDir.Name = "btnAddDir";
            this.btnAddDir.Size = new System.Drawing.Size(105, 23);
            this.btnAddDir.TabIndex = 1;
            this.btnAddDir.Text = "Add directory";
            this.btnAddDir.UseVisualStyleBackColor = true;
            this.btnAddDir.Click += new System.EventHandler(this.btnAddDir_Click);
            // 
            // lboxUpdates
            // 
            this.lboxUpdates.FormattingEnabled = true;
            this.lboxUpdates.Location = new System.Drawing.Point(399, 28);
            this.lboxUpdates.Name = "lboxUpdates";
            this.lboxUpdates.Size = new System.Drawing.Size(389, 186);
            this.lboxUpdates.TabIndex = 2;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(13, 415);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lboxUpdates);
            this.Controls.Add(this.btnAddDir);
            this.Controls.Add(this.lboxDirectories);
            this.Name = "Main";
            this.Text = "JD Rive";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ListBox lboxDirectories;
        private System.Windows.Forms.FolderBrowserDialog folderBrowse;
        private System.Windows.Forms.Button btnAddDir;
        private System.Windows.Forms.ListBox lboxUpdates;
        private System.Windows.Forms.Button btnTest;
    }
}

