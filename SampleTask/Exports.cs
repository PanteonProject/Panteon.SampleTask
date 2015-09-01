using System.ComponentModel.Composition;
using Autofac;
using Autofac.Extras.NLog;
using Panteon.SampleTask.Configuration;
using Panteon.Sdk;
using Panteon.Sdk.IO;
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

                builder.RegisterModule<WorkerModule>();

                builder.RegisterType<JsonNetSerializer>().As<IJsonSerializer>().SingleInstance();

                builder.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance();
                builder.RegisterType<FileReader>().As<IFileReader>().SingleInstance();


                builder.Register(context => new SampleTaskConfigProvider(
                    context.Resolve<ILogger>(),
                    context.Resolve<IJsonSerializer>(),
                    context.Resolve<IFileReader>()
                    )
                    .ParseSettings()
                    ).AsImplementedInterfaces()
                    .SingleInstance();

                builder.RegisterType<SampleTask>().As<IPanteonWorker>();

                return builder;
            }
        }
    }
}