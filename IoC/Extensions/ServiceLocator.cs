using System;
using Microsoft.Extensions.DependencyInjection;

namespace EFCache.POC.IoC.Extensions
{
    //Ugly solution, but this only way to wire up extension methods
    public class ServiceLocator
    {
        private ServiceProvider _currentServiceProvider;
        private static Lazy<ServiceLocator> _lazyLocator = new Lazy<ServiceLocator>(() => new ServiceLocator());
        private ServiceLocator()
        {

        }

        public static ServiceLocator Current => _lazyLocator.Value;
        
        public void SetLocatorProvider(ServiceProvider serviceProvider)
        {
            _currentServiceProvider = serviceProvider;
        }

        public object GetInstance(Type serviceType)
        {
            return _currentServiceProvider.GetService(serviceType);
        }

        public TService GetInstance<TService>()
        {
            return _currentServiceProvider.GetService<TService>();
        }
    }
}