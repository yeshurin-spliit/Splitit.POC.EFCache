using Microsoft.Extensions.DependencyInjection;

namespace EFCache.POC.IoC
{
    public interface IBootstrap
    {
        void Register(IServiceCollection serviceCollection);
        int Bootorder { get; }
    }
}