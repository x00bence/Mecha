using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using Wrapper.Util;

namespace Wrapper.Hooks
{
    class Unhook
    {
        public static void Do()
        {
            Environment.SetEnvironmentVariable("MECHA_CONFIG", null, EnvironmentVariableTarget.User);

            try
            {
                Registry.LocalMachine.DeleteSubKey(Constants.subkey);
            }
            catch 
            { 
            }

            if (League.IsLeagueOpen())
            {
                string leaguePath = League.GetLeaguePath();

                DialogResult dialogResult = MessageBox.Show("Remove injection files? This will close your client.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    League.KillLeagueProcesses();

                    try
                    {
                        File.Delete(leaguePath + "mecha_injector.exe");
                        File.Delete(leaguePath + "mecha_payload.dll");
                    }
                    catch
                    {
                        MessageBox.Show("Failed removing injection files. You may remove them manually.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                        return;
                    }
                }

                MessageBox.Show("Successfully unhooked.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return;
            } 
            else 
            {
                MessageBox.Show("Successfully unhooked. You may remove injection files manually.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return;
            }
        }
    }
}
