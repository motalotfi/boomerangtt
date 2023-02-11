using System.Collections.Generic;
using System.Reflection;

namespace Boomrang.Util
{
    public static class AppAssembliesProvider
    {
        public static IEnumerable<Assembly> GetAppAssemblies()
        {
            yield return Assembly.Load("Boomrang.Data");
            yield return Assembly.Load("Boomrang.Service");
            yield return Assembly.Load("Boomrang.App");
        }
    }
}
