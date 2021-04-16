using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Wrapper.Data;
using Wrapper.Hooks;
using Wrapper.Util;

namespace Wrapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Migrate away from old version and create a config file.
            if (!File.Exists(Constants.configPath))
            {
                Environment.SetEnvironmentVariable("MECHA_PORT", null, EnvironmentVariableTarget.User);
                Environment.SetEnvironmentVariable("MECHA_PLUGINS_PATH", null, EnvironmentVariableTarget.User);

                Constants.configManager.WriteDefault();
            }

            // We set MECHA_CONFIG on every launch, this allows us to persist whenever the directory containing Mecha changes.
            Environment.SetEnvironmentVariable("MECHA_CONFIG", Constants.configPath, EnvironmentVariableTarget.User);

            Constants.configManager.Read();

            UpdateGui();
        }

        // +===+===+ GUI +===+===+ 

        public void UpdateGui()
        {
            this.portTextBox.Text = Constants.configManager.GetDebuggingPort();
            this.pluginsTextBox.Text = Constants.configManager.GetPluginsPath();
            this.hookedLabel.Text = Registry.LocalMachine.OpenSubKey(Constants.subkey) != null ? "Hooked" : "Not Hooked";

            AddListViewItems();
        }

        private void AddListViewItems()
        {
            this.pluginsList.Items.Clear();

            if (!Directory.Exists(Constants.configManager.GetPluginsPath()))
            {
                return;
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(Constants.configManager.GetPluginsPath());

            FileInfo[] files = directoryInfo.GetFiles("*.js");

            foreach (FileInfo file in files)
            {
                string[] row = {
                    file.Name, file.FullName
                };

                this.pluginsList.Items.Add(new ListViewItem(row));
            }
        }

        // +===+===+ Misc +===+===+ 

        private void tabControl1_Selected(object sender, TabControlEventArgs e) => UpdateGui();

        private void githubLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            => Process.Start("https://github.com/x00bence/Mecha");

        // +===+===+ Main +===+===+ 

        private void hookBtn_Click(object sender, EventArgs e)
        {
            Hook.Do();
            UpdateGui();
        }

        private void unhookBtn_Click(object sender, EventArgs e)
        {
            Unhook.Do();
            UpdateGui();
        }

        private void devtoolsBtn_Click(object sender, EventArgs e)
        {
            if (!League.IsLeagueOpen())
            {
                MessageBox.Show("You must open your (hooked) League of Legends client!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            League.OpenDevTools();
        }

        // +===+===+ Settings +===+===+ 

        private void portTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void changePathBtn_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog()
            {
                SelectedPath = Directory.GetCurrentDirectory()
            })
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    this.pluginsTextBox.Text = fbd.SelectedPath;
                }
            }
        }

        private void saveSettingsBtn_Click(object sender, EventArgs e)
        {
            int port = Convert.ToInt32(portTextBox.Text);

            if (port > 0 && port < 65535)
            {
                Constants.configManager.SetDebuggingPort(portTextBox.Text);
            }
            else
            {
                MessageBox.Show("Port must be between 1-65535!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!Directory.Exists(pluginsTextBox.Text))
            {
                Directory.CreateDirectory(pluginsTextBox.Text);
            }

            Constants.configManager.SetPluginsPath(pluginsTextBox.Text);

            Constants.configManager.Write();

            UpdateGui();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            Constants.configManager.WriteDefault();
            this.UpdateGui();
        }

        // +===+===+ Plugins +===+===+ 

        private void refreshBtn_Click(object sender, EventArgs e)
            => this.UpdateGui();

        private void openPluginsBtn_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Constants.configManager.GetPluginsPath()))
            {
                MessageBox.Show("Plugins directory does not exist or is not found. Please update the directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            Process.Start(Constants.configManager.GetPluginsPath());
        }

    }
}
