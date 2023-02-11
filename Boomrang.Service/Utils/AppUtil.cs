using Autofac;
using Bit.Core.Contracts;
using Bit.Owin.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Boomrang.Service.Utils
{
    public class AppUtil
    {
        public static void BootstrapApp(
            out IDependencyManager dependencyManager,
            out IServiceCollection services,
            out ContainerBuilder containerBuilder)
        {
            dependencyManager = DefaultDependencyManager.Current;
            services = new ServiceCollection();

            dependencyManager.Init();
            containerBuilder = ((IAutofacDependencyManager)dependencyManager)
                .GetContainerBuidler();
        }
    }
}
