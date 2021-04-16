using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using Wrapper.Util;

namespace Wrapper.Hooks
{
    class Hook
    {
        public static void Do()
        {
            Environment.SetEnvironmentVariable("MECHA_CONFIG", Constants.configPath, EnvironmentVariableTarget.User);

            Constants.configManager.WriteDefault();

            if (!File.Exists(Constants.injectorPath) || !File.Exists(Constants.payloadPath))
            {
                MessageBox.Show("You are missing needed files for hooking. Please download a fresh version of Mecha or recompile your project.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                return;
            }

            if (!League.IsLeagueOpen())
            {
                MessageBox.Show("You must launch your League of Legends client before hooking!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            string leaguePath = League.GetLeaguePath();

            DialogResult dialogResult = MessageBox.Show("Mecha must terminate your League of Legends client process before hooking. Continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                League.KillLeagueProcesses();
            }
            else 
            {
                return;
            }

            if (!Directory.Exists(Constants.defaultPluginsPath))
            {
                Directory.CreateDirectory(Constants.defaultPluginsPath);
            }

            try
            {
                File.Copy(Constants.injectorPath, leaguePath + "mecha_injector.exe", true);
                File.Copy(Constants.payloadPath, leaguePath + "mecha_payload.dll", true);
            } 
            catch 
            {
                MessageBox.Show("An error occurred while copying files needed for hooking. Unhook and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(Constants.subkey);
            registryKey.SetValue("debugger", Path.Combine(leaguePath, "mecha_injector.exe"));

            MessageBox.Show("Successfully hooked. You may restart your League of Legends client to see the changes.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
