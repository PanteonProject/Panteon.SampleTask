using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extras.NLog;
using NLog;
using Panteon.HistoryStorage.SqlServer;
using Panteon.SampleTask.Configuration;
using Panteon.Sdk;
using Panteon.Sdk.History;
using Panteon.Sdk.IO;
using Panteon.Sdk.Utils;
using ILogger = Autofac.Extras.NLog.ILogger;

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

                RegisterCustomNLogger(builder);

                builder.RegisterType<JsonNetSerializer>().As<IJsonSerializer>().SingleInstance();

                builder.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance();
                builder.RegisterType<FileReader>().As<IFileReader>().SingleInstance();
                builder.RegisterType<SqlServerHistoryStorage>().As<IHistoryStorage>().SingleInstance();

                builder.Register(
                    context =>
                        new SampleTaskConfigProvider(context.Resolve<ILogger>(), context.Resolve<IJsonSerializer>(),
                            context.Resolve<IFileReader>()).ParseSettings())
                    .AsImplementedInterfaces()
                    .SingleInstance();

                builder.RegisterType<SampleTask>().As<IPanteonWorker>();

                return builder;
            }
        }

        private void RegisterCustomNLogger(ContainerBuilder builder)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            var originalJobFolder = Path.GetDirectoryName(executingAssembly.CodeBase);

            if (!string.IsNullOrEmpty(originalJobFolder))
            {
                string combine = Path.Combine(originalJobFolder.Replace("file:\\", ""), "NLog.config");

                if (File.Exists(combine))
                {
                    LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(combine, true);

                    Logger currentClassLogger = LogManager.GetCurrentClassLogger();

                    builder.Register(c => new LoggerAdapter(currentClassLogger)).As<ILogger>().SingleInstance();
                }
            }
        }
    }
}