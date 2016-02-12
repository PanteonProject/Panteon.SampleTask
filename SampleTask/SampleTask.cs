using System;
using System.Data.Entity.SqlServer;
using Autofac.Extras.NLog;
using Panteon.SampleTask.Configuration;
using Panteon.Sdk;
using Panteon.Sdk.History;

namespace Panteon.SampleTask
{
    public class SampleTask : PanteonWorker, IDisposable
    {
        public SampleTask(ILogger logger, ISampleTaskSettings taskSettings, IHistoryStorage historyStorage) : base(logger, taskSettings, historyStorage)
        {
        }
        public override string Name => "My-Dummy-Task";

        public override bool Init(bool autoRun)
        {
            SqlProviderServices providerServices;
            return Run((task, offset) => Console.WriteLine($"Dummy Hello {DateTime.Now}"));
        }
    }
}