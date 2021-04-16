using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Wrapper.Util
{
    class League
    {
        public static bool IsLeagueOpen() 
            => Process.GetProcessesByName("LeagueClientUx")?.Length > 0;

        public static string GetLeaguePath() 
            => Process.GetProcessesByName("LeagueClientUx")[0].MainModule.FileName.Replace("LeagueClientUx.exe", "");

        public static void KillLeagueProcesses()
        {
            Process[] procs = Process
                .GetProcessesByName("LeagueClientUx")
                .Concat(Process.GetProcessesByName("LeagueClient"))
                .Concat(Process.GetProcessesByName("LeagueClientUxRender"))
                .ToArray();

            foreach(Process proc in procs)
            {
                if (!proc.HasExited)
                {
                    proc.Kill();
                    proc.WaitForExit();
                }
            }
        }

        public static void OpenDevTools()
        {
            string port = Constants.configManager.GetDebuggingPort();
            string resp;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://localhost:{port}/json");
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader sr = new StreamReader(stream))
                {
                    resp = sr.ReadToEnd();

                    if (string.IsNullOrEmpty(resp))
                    {
                        return;
                    }

                    dynamic json = JsonConvert.DeserializeObject(resp);

                    Process.Start($"http://localhost:{port}{json[0].devtoolsFrontendUrl}");

                    return;
                }
            }
            catch
            {
                MessageBox.Show("There was an error while opening the DevTools URL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }
    }
}
