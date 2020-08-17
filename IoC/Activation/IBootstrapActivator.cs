using Microsoft.Extensions.DependencyInjection;

namespace EFCache.POC.IoC.Activation
{
    public interface IBootstrapActivator
    {
        void Activate(IServiceCollection serviceCollection);
    }
}