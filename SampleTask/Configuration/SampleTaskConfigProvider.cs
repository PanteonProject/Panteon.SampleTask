using System.IO;
using System.Reflection;
using Autofac.Extras.NLog;
using Panteon.Sdk.Configuration;
using Panteon.Sdk.IO;
using Panteon.Sdk.Utils;

namespace Panteon.SampleTask.Configuration
{
    public class SampleTaskConfigProvider : TaskConfigProviderBase<SampleTaskSettings>
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IFileReader _fileReader;

        public SampleTaskConfigProvider(ILogger logger, IJsonSerializer jsonSerializer, IFileReader fileReader)
            : base(logger)
        {
            _jsonSerializer = jsonSerializer;
            _fileReader = fileReader;
        }

        public override SampleTaskSettings ParseSettings(string configFilePath = null)
        {
            string asmLocation = Assembly.GetExecutingAssembly().Location;

            string directoryName = Path.GetDirectoryName(asmLocation);

            if (!string.IsNullOrEmpty(directoryName))
            {
                configFilePath = configFilePath ?? Path.Combine(directoryName, "config.json");
            }

            FileContentResult result = _fileReader.ReadFileContent(configFilePath);

            SampleTaskSettings settings = _jsonSerializer.Deserialize<SampleTaskSettings>(result.Content);

            return settings;
        }
    }
}