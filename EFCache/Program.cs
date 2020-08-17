using System;
using Microsoft.Extensions.DependencyInjection;

namespace EFCache.POC.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var startup = new Startup();
            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);

        }
    }
}
