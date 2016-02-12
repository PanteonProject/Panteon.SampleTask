using Panteon.Sdk.Configuration;
using Panteon.Sdk.History;

namespace Panteon.SampleTask.Configuration
{
    public class SampleTaskSettings : WorkerSettingsBase, ISampleTaskSettings, IHistoryStorageSettings
    {
        public string ConnectionString { get; set; }
    }
}