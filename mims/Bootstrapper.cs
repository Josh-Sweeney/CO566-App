using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Diagnostics;
using gha.mobile.common.mvvm;
using gha.mobile.common.repositories;
using gha.mobile.common.resolver;
using gha.mobile.mims.mvvm;
using Xamarin.Forms;

namespace mims
{
    public abstract class Bootstrapper
    {
        public Bootstrapper()
        {
            Initialize();
            FinishInitialization();
        }

        protected ContainerBuilder ContainerBuilder { get; private set; }

        protected virtual void Initialize()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            
            Type appViewModel = typeof(AppViewModel);
            var appAssembly = Assembly.GetAssembly(appViewModel);

            ContainerBuilder = new ContainerBuilder();

            foreach (var type in currentAssembly.DefinedTypes.Where(e => e.IsSubclassOf(typeof(Page))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }

            foreach (var type in appAssembly.DefinedTypes.Where(e =>
                         e.IsSubclassOf(typeof(Page)) || e.IsSubclassOf(typeof(AppViewModel))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }

            // Services
            ContainerBuilder.RegisterType<MessageService>().As<IMessageService>();


            ContainerBuilder.RegisterType<SettingsRepository>().SingleInstance();
        }

        private void FinishInitialization()
        {
            var container = ContainerBuilder.Build();

#if DEBUG
            // Show more detailed error messages
            var tracer = new DefaultDiagnosticTracer();
            tracer.OperationCompleted += (sender, args) => { Trace.WriteLine(args.TraceContent); };

            // Subscribe to the diagnostics with your tracer.
            container.SubscribeToDiagnostics(tracer);
#endif

            Resolver.Initialize(container);
        }
    }
}