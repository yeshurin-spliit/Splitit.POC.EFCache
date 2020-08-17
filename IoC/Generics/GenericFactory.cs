using System;
using Microsoft.Extensions.DependencyInjection;

namespace EFCache.POC.IoC.Generics
{
    public class GenericFactory<T> : IFactory<T>
    {
        private readonly IServiceProvider _serviceProvider;

        public GenericFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Create()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}