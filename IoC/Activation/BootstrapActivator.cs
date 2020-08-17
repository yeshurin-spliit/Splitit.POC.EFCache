using System;
using System.Linq;
using EFCache.POC.IoC.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace EFCache.POC.IoC.Activation
{
    public class BootstrapActivator : IBootstrapActivator
    {
        public void Activate(IServiceCollection serviceCollection)
        {
            var provider = serviceCollection.BuildServiceProvider();

            var bootstraps = provider.GetServices<IBootstrap>().OrderBy(a => a.Bootorder);

            foreach (var bootstrap in bootstraps)
            {
                try
                {
                    bootstrap.Register(serviceCollection);
                }
                catch (Exception e)
                {
                    EmergencyLogger.LogWarn(e, $"Exception caught at {bootstrap.GetType()} bootstraper");
                }
            }
        }
    }
}