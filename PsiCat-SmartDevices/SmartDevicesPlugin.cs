namespace PsiCat.SmartDevices
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using PsiCat.Plugins;


    public class SmartDevicesPlugin : PsiCatPlugin
    {
        
        public override string Author
        {
            get
            {
                return "Wren White (Primitive Concept)";
            }
        }


        public override Config Config { get; set; } // TODO


        public override string Description
        {
            get
            {
                return "Manages access to various smart devices.";
            }
        }


        public override string Name
        {
            get { return "PsiCAT Smart Devices"; }
        }


        public override PluginHost PluginHost { get; set; }


        public override string Version
        {
            get { return "1.0.0"; }
        }
        
        public SmartLights SmartLights { get; private set; }

        public override async void OnStart()
        {
            LoadConfig();

            this.SmartLights = new SmartLights();
            this.SmartLights.Logger = this.Logger;
            
            await this.SmartLights.LocateAll(this.Config as SmartDevicesConfig);
            
            this.Config.Save(SmartDevicesConfig.DefaultFilePath);
        }


        public override void OnUpdate()
        {
            // TODO
        }
        
        
        /// <summary>
        /// Load in main config file.
        /// If no config file found, a new one will be created.
        /// </summary>
        public void LoadConfig(string path = null)
        {
            if (string.IsNullOrEmpty(path))
                path = SmartDevicesConfig.DefaultFilePath;
            
            if (File.Exists(path))
            {
                this.Config = PsiCat.Config.LoadFromJson<SmartDevicesConfig>(path);
            }
            else
            {
                this.Logger.LogWarning($"Creating new config at: {path}");
                this.Config = new SmartDevicesConfig();
                this.Config.Save(path);
            }
        }
    }
}