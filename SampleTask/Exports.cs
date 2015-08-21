using System.ComponentModel.Composition;
using Autofac;
using Autofac.Extras.NLog;
using Panteon.SampleTask.Configuration;
using Panteon.Sdk;
using Panteon.Sdk.Utils;

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

                builder.RegisterType<EnvironmentWrapper>().As<IEnvironmentWrapper>().SingleInstance();

                builder.RegisterType<JsonNetSerializer>().As<IJsonSerializer>().SingleInstance();

                builder.Register(context => new SampleTaskConfigProvider(context.Resolve<ILogger>()).ParseSettings())
                    .AsImplementedInterfaces().SingleInstance();

                builder.RegisterType<SampleTask>().As<IPanteonTask>();

                return builder;
            }
        }
    }
}