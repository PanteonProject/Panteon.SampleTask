using System;
using Autofac.Extras.NLog;
using Panteon.SampleTask.Configuration;
using Panteon.Sdk;

namespace Panteon.SampleTask
{
    public class SampleTask : PanteonTask, IDisposable
    {
        public SampleTask(ILogger logger, ISampleTaskSettings taskSettings) : base(logger, taskSettings)
        {
        }
        public override string Name => "My-Dummy-Task";

        public override bool Bootstrap()
        {
            return Run((task, offset) => Console.WriteLine($"Dummy Hello {DateTime.Now}"));
        }
    }
}