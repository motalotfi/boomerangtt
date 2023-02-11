using Bit.Core;
using Bit.Owin;
using Bit.Owin.Implementations;
using Boomrang.Util;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading.Tasks;

namespace Boomrang.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            AssemblyContainer.Current.AddAppAssemblies(AppAssembliesProvider.GetAppAssemblies().ToArray());

            AspNetCoreAppEnvironmentsProvider.Current.Use();

            await BuildWebHost(args)
                .RunAsync();
        }

        public static IHost BuildWebHost(string[] args) =>
            BitWebHost.CreateWebHost(args)
                .Build();
    }
}
