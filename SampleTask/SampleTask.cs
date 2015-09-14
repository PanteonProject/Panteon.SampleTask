using System;
using Autofac.Extras.NLog;
using Panteon.SampleTask.Configuration;
using Panteon.Sdk;

namespace Panteon.SampleTask
{
    public class SampleTask : PanteonWorker, IDisposable
    {
        public SampleTask(ILogger logger, ISampleTaskSettings taskSettings) : base(logger, taskSettings)
        {
        }
        public override string Name
        {
            get { return "My-Dummy-Task"; }
        }

        public override bool Init(bool autoRun)
        {
            return Run((task, offset) => Console.WriteLine("Dummy Hello {0}", DateTime.Now));
        }
    }
}