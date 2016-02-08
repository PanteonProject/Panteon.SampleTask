# Panteon.SampleTask
Sample Task

**Task**
```cs
 public class SampleTask : PanteonWorker, IDisposable
    {
        public SampleTask(ILogger logger, ISampleTaskSettings taskSettings) : base(logger, taskSettings)
        {
        }
        public override string Name => "My-Dummy-Task";

        public override bool Init(bool autoRun)
        {
            SqlProviderServices providerServices;
            return Run((task, offset) => Console.WriteLine($"Dummy Hello {DateTime.Now}"));
        }
    }
```

**Exports*

```cs
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
```
