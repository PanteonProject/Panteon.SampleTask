using System.ComponentModel.Composition;
using Autofac;
using Panteon.Sdk;

namespace Panteon.SampleTask
{
    [Export(typeof(ITaskExports))]
    public class Exports : ITaskExports
    {
        public ContainerBuilder Builder
        {
            get
            {
                var builder = new ContainerBuilder();

                builder.RegisterModule<CoreModule>();
                builder.RegisterModule<TaskModule>();

                return builder;
            }
        }
    }
}