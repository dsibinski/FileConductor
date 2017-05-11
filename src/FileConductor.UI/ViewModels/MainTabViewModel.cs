using System.Collections.Generic;
using FileConductor.Configuration;
using FileConductor.Configuration.XmlData;
using FileConductor.ConfigurationTool.Entities;
using FileConductorUI.UI;

namespace FileConductor.ConfigurationTool.ViewModels
{
    public class MainTabViewModel : Tab
    {
        public MainTabViewModel()
        {
            Name = "Main";
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            var deserializer = new XmlFileDeserializer<ConfigurationData>("Configuration\\Config.xml");
            deserializer.Deserialize();

            Watchers = new List<Watcher>();
            foreach (var watcherData in deserializer.XmlData.Watchers)
            {
                Watchers.Add(new Watcher() {Code = watcherData.Code});
            }
        }

        public List<Watcher> Watchers { get; set; }
        public Watcher SelectedWatcher { get; set; }
        public CommandHandler EditWatcher { get; set; }
        public CommandHandler RemoveWatcher { get; set; }
        public CommandHandler TestWatcher { get; set; }
        public ConfigurationData Configuration { get; set; }

    }
}