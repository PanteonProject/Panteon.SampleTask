using Autofac.Extras.NLog;

namespace Panteon.SampleTask.Configuration
{
    public class SampleTaskConfigProvider : TaskConfigProviderBase<SampleTaskSettings>
    {
        public SampleTaskConfigProvider(ILogger logger)
            : base(logger)
        {
        }

        public override SampleTaskSettings ParseSettings(string configFilePath = null)
        {
            return new SampleTaskSettings
            {
                RedisConnectionString = "localhost:6379",
                SchedulePattern = "min(*)",
                DbNo = -1
            };
        }
    }
}