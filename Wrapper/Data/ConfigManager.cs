using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrapper.Util;

namespace Wrapper.Data
{
    class ConfigManager
    {
        public static ConfigManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigManager();
                    }
                    return _instance;
                }
            }
        }

        private static ConfigManager _instance = null;
        private static readonly object _lock = new object();
        private MechaConfigModel data;

        private ConfigManager() 
        { 
        }

        public void Read()
            => this.data = JsonConvert.DeserializeObject<MechaConfigModel>(File.ReadAllText(Constants.configPath));

        public void Write()
            => File.WriteAllText(Constants.configPath, JsonConvert.SerializeObject(this.data, Formatting.Indented));

        public void WriteDefault()
        {
            File.WriteAllText(Constants.configPath, JsonConvert.SerializeObject(new MechaConfigModel() 
            {
                Migrated = true,
                DebuggingPort = "8888",
                PluginsPath = Constants.defaultPluginsPath
            }, Formatting.Indented));

            this.Read();
        }

        public string GetDebuggingPort() 
            => this.data.DebuggingPort;

        public string SetDebuggingPort(string port) 
            => this.data.DebuggingPort = port;

        public string GetPluginsPath() 
            => this.data.PluginsPath;

        public string SetPluginsPath(string pluginspath) 
            => this.data.PluginsPath = pluginspath;
    }
}
