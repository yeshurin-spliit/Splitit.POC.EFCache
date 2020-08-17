using System.Linq;
using System.Reflection;
using EFCache.POC.IoC.Activation;
using EFCache.POC.IoC.Generics;
using Microsoft.Extensions.DependencyInjection;

namespace EFCache.POC.IoC.Extensions
{
    public class BootstrapersRegistration
    {
        public static void Run(IServiceCollection serviceCollection, Assembly[] loadedAssemblies = null)
        {
            if (loadedAssemblies == null)
            {
                loadedAssemblies = AssemblyDiscovery.Discovery();
            }

            var typesFromAssemblies = loadedAssemblies.SelectMany(a =>
                a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(IBootstrap))));
            foreach (var type in typesFromAssemblies)
                serviceCollection.AddTransient(typeof(IBootstrap), type);

            serviceCollection.AddTransient(typeof(IFactory<>), typeof(GenericFactory<>));

            serviceCollection.AddTransient<IBootstrapActivator, BootstrapActivator>();
            var provider = serviceCollection.BuildServiceProvider();
            var activator = provider.GetService<IBootstrapActivator>();

            activator.Activate(serviceCollection);


        }
    }
}