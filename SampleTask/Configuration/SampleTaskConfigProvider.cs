using Autofac.Extras.NLog;
using Panteon.Sdk.Configuration;
using Panteon.Sdk.IO;
using Panteon.Sdk.Utils;

namespace Panteon.SampleTask.Configuration
{
    public class SampleTaskConfigProvider : TaskConfigProviderBase<SampleTaskSettings>
    {
        public SampleTaskConfigProvider(ILogger logger, IJsonSerializer jsonSerializer, IFileReader fileReader)
            : base(logger, jsonSerializer, fileReader)
        {
        }
    }
}