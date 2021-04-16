using System.IO;
using Wrapper.Data;

namespace Wrapper.Util
{
    class Constants
    {
        public static readonly string currPath = Directory.GetCurrentDirectory();
        public static readonly string defaultPluginsPath = Path.Combine(currPath, "plugins");
        public static readonly string configPath = Path.Combine(currPath, "config.json");
        public static readonly string injectorPath = Path.Combine(currPath, "mecha_injector.exe");
        public static readonly string payloadPath = Path.Combine(currPath, "mecha_payload.dll");
        public static readonly string subkey = "Software\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\LeagueClientUx.exe";
        public static readonly ConfigManager configManager = ConfigManager.Instance;
    }
}
