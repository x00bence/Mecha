
namespace Wrapper
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.hookedLabel = new System.Windows.Forms.Label();
            this.devtoolsBtn = new System.Windows.Forms.Button();
            this.unhookBtn = new System.Windows.Forms.Button();
            this.hookBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.resetBtn = new System.Windows.Forms.Button();
            this.saveSettingsBtn = new System.Windows.Forms.Button();
            this.changePathBtn = new System.Windows.Forms.Button();
            this.pluginsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.openPluginsBtn = new System.Windows.Forms.Button();
            this.pluginsList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.githubLabel = new System.Windows.Forms.LinkLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(441, 301);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.hookedLabel);
            this.tabPage1.Controls.Add(this.devtoolsBtn);
            this.tabPage1.Controls.Add(this.unhookBtn);
            this.tabPage1.Controls.Add(this.hookBtn);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(433, 275);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // hookedLabel
            // 
            this.hookedLabel.AutoSize = true;
            this.hookedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hookedLabel.Location = new System.Drawing.Point(15, 14);
            this.hookedLabel.Name = "hookedLabel";
            this.hookedLabel.Size = new System.Drawing.Size(77, 24);
            this.hookedLabel.TabIndex = 3;
            this.hookedLabel.Text = "Hooked";
            // 
            // devtoolsBtn
            // 
            this.devtoolsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.devtoolsBtn.Location = new System.Drawing.Point(6, 194);
            this.devtoolsBtn.Name = "devtoolsBtn";
            this.devtoolsBtn.Size = new System.Drawing.Size(421, 41);
            this.devtoolsBtn.TabIndex = 2;
            this.devtoolsBtn.Text = "Open DevTools URL";
            this.devtoolsBtn.UseVisualStyleBackColor = true;
            this.devtoolsBtn.Click += new System.EventHandler(this.devtoolsBtn_Click);
            // 
            // unhookBtn
            // 
            this.unhookBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.unhookBtn.Location = new System.Drawing.Point(218, 147);
            this.unhookBtn.Name = "unhookBtn";
            this.unhookBtn.Size = new System.Drawing.Size(209, 41);
            this.unhookBtn.TabIndex = 1;
            this.unhookBtn.Text = "Unhook";
            this.unhookBtn.UseVisualStyleBackColor = true;
            this.unhookBtn.Click += new System.EventHandler(this.unhookBtn_Click);
            // 
            // hookBtn
            // 
            this.hookBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hookBtn.Location = new System.Drawing.Point(6, 147);
            this.hookBtn.Name = "hookBtn";
            this.hookBtn.Size = new System.Drawing.Size(209, 41);
            this.hookBtn.TabIndex = 0;
            this.hookBtn.Text = "Hook";
            this.hookBtn.UseVisualStyleBackColor = true;
            this.hookBtn.Click += new System.EventHandler(this.hookBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.resetBtn);
            this.tabPage2.Controls.Add(this.saveSettingsBtn);
            this.tabPage2.Controls.Add(this.changePathBtn);
            this.tabPage2.Controls.Add(this.pluginsTextBox);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.portTextBox);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(433, 275);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // resetBtn
            // 
            this.resetBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.resetBtn.Location = new System.Drawing.Point(3, 246);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(427, 26);
            this.resetBtn.TabIndex = 6;
            this.resetBtn.Text = "Reset Settings";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // saveSettingsBtn
            // 
            this.saveSettingsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveSettingsBtn.Location = new System.Drawing.Point(3, 214);
            this.saveSettingsBtn.Name = "saveSettingsBtn";
            this.saveSettingsBtn.Size = new System.Drawing.Size(427, 26);
            this.saveSettingsBtn.TabIndex = 5;
            this.saveSettingsBtn.Text = "Save Settings";
            this.saveSettingsBtn.UseVisualStyleBackColor = true;
            this.saveSettingsBtn.Click += new System.EventHandler(this.saveSettingsBtn_Click);
            // 
            // changePathBtn
            // 
            this.changePathBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.changePathBtn.Location = new System.Drawing.Point(350, 86);
            this.changePathBtn.Name = "changePathBtn";
            this.changePathBtn.Size = new System.Drawing.Size(77, 22);
            this.changePathBtn.TabIndex = 4;
            this.changePathBtn.Text = "Change";
            this.changePathBtn.UseVisualStyleBackColor = true;
            this.changePathBtn.Click += new System.EventHandler(this.changePathBtn_Click);
            // 
            // pluginsTextBox
            // 
            this.pluginsTextBox.Location = new System.Drawing.Point(9, 87);
            this.pluginsTextBox.Name = "pluginsTextBox";
            this.pluginsTextBox.ReadOnly = true;
            this.pluginsTextBox.Size = new System.Drawing.Size(337, 20);
            this.pluginsTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Plugins Path";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(9, 29);
            this.portTextBox.MaxLength = 5;
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(418, 20);
            this.portTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Remote Debugging Port (default: 8888)";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.refreshBtn);
            this.tabPage3.Controls.Add(this.openPluginsBtn);
            this.tabPage3.Controls.Add(this.pluginsList);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(433, 275);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Plugins";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // refreshBtn
            // 
            this.refreshBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshBtn.Location = new System.Drawing.Point(3, 217);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(427, 26);
            this.refreshBtn.TabIndex = 7;
            this.refreshBtn.Text = "Refresh";
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // openPluginsBtn
            // 
            this.openPluginsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openPluginsBtn.Location = new System.Drawing.Point(3, 246);
            this.openPluginsBtn.Name = "openPluginsBtn";
            this.openPluginsBtn.Size = new System.Drawing.Size(427, 26);
            this.openPluginsBtn.TabIndex = 6;
            this.openPluginsBtn.Text = "Open Plugins Folder";
            this.openPluginsBtn.UseVisualStyleBackColor = true;
            this.openPluginsBtn.Click += new System.EventHandler(this.openPluginsBtn_Click);
            // 
            // pluginsList
            // 
            this.pluginsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.pluginsList.HideSelection = false;
            this.pluginsList.Location = new System.Drawing.Point(3, 3);
            this.pluginsList.Name = "pluginsList";
            this.pluginsList.Size = new System.Drawing.Size(427, 208);
            this.pluginsList.TabIndex = 0;
            this.pluginsList.UseCompatibleStateImageBehavior = false;
            this.pluginsList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 113;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Path";
            this.columnHeader2.Width = 310;
            // 
            // githubLabel
            // 
            this.githubLabel.AutoSize = true;
            this.githubLabel.Location = new System.Drawing.Point(12, 326);
            this.githubLabel.Name = "githubLabel";
            this.githubLabel.Size = new System.Drawing.Size(40, 13);
            this.githubLabel.TabIndex = 1;
            this.githubLabel.TabStop = true;
            this.githubLabel.Text = "GitHub";
            this.githubLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.githubLabel_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(465, 348);
            this.Controls.Add(this.githubLabel);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Mecha GUI";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button devtoolsBtn;
        private System.Windows.Forms.Button unhookBtn;
        private System.Windows.Forms.Button hookBtn;
        private System.Windows.Forms.ListView pluginsList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Button changePathBtn;
        private System.Windows.Forms.TextBox pluginsTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveSettingsBtn;
        private System.Windows.Forms.Button openPluginsBtn;
        private System.Windows.Forms.Label hookedLabel;
        private System.Windows.Forms.LinkLabel githubLabel;
        private System.Windows.Forms.Button refreshBtn;
        private System.Windows.Forms.Button resetBtn;
    }
}

