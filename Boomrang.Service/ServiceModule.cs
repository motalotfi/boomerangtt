using Bit.Core.Contracts;
using Bit.Owin.Implementations;
using Boomrang.Service;
using Boomrang.Service.Contracts;
using Boomrang.Service.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: AppModule(typeof(ServiceModule))]

namespace Boomrang.Service
{
    public class ServiceModule : IAppModule
    {
        public void ConfigureDependencies(IServiceCollection services, IDependencyManager dependencyManager)
        {
            dependencyManager.Register<IBoomrangDbService, BoomrangDbService>();
            dependencyManager.Register<ISmsService, KavehNegarSmsService>();
            dependencyManager.Register<IComputeHash, ComputeHash>();
            dependencyManager.Register<ImageService>();

            services.Configure<KavehNegarOptions>(AspNetCoreAppEnvironmentsProvider.Current.Configuration.GetSection("KavehNegar"));

            services.AddHttpClient("KavehNegar", (serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = serviceProvider.GetRequiredService<IOptions<KavehNegarOptions>>().Value.Url;
            });
        }
    }
}

