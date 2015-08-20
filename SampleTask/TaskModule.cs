using Autofac;
using Autofac.Extras.NLog;
using Panteon.SampleTask.Configuration;

namespace Panteon.SampleTask
{
    public class TaskModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EnvironmentWrapper>().As<IEnvironmentWrapper>().SingleInstance();

            builder.RegisterType<JsonNetSerializer>().As<IJsonSerializer>().SingleInstance();

            builder.Register(context => new SampleTaskConfigProvider(context.Resolve<ILogger>()).ParseSettings())
                .AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<SampleTask>().As<IPanteonTask>();
        }
    }
}