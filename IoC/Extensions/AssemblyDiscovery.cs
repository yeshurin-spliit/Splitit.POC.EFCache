using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace EFCache.POC.IoC.Extensions
{
    public class AssemblyDiscovery
    {
        public static Assembly[] Discovery()
        {
            return DependencyContext.Default.RuntimeLibraries.SelectMany(l => l.GetDefaultAssemblyNames(DependencyContext.Default)).Select(Assembly.Load).ToArray();
        }
    }
}