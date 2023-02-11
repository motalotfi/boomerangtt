using Bit.Core.Contracts;
using Bit.Model.Implementations;
using Bit.OData.ActionFilters;
using Bit.OData.Contracts;
using Bit.Owin.Implementations;
using Boomrang.App;
using Boomrang.App.Helper;
using Boomrang.App.Identity;
using Boomrang.Data;
using Boomrang.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Owin;
using Swashbuckle.Application;
using System;
using System.IO.Compression;
using System.Reflection;

[assembly: AppModule(typeof(AppModule))]
[assembly: ODataModule("App")]

namespace Boomrang.App
{
    public class AppModule : IAppModule
    {
        public void ConfigureDependencies(IServiceCollection services, IDependencyManager dependencyManager)
        {
            #region Configure services

            dependencyManager.RegisterMinimalDependencies();

            dependencyManager.RegisterDefaultLogger(AspNetCoreAppEnvironmentsProvider.Current.HostingEnvironment.IsDevelopment() ? new[] { typeof(DebugLogStore).GetTypeInfo(), typeof(ConsoleLogStore).GetTypeInfo() } : Array.Empty<TypeInfo>());

            dependencyManager.RegisterDefaultAspNetCoreApp();

            dependencyManager.RegisterDefaultWebApiAndODataConfiguration();

            dependencyManager.RegisterDtoEntityMapper();
            dependencyManager.RegisterMapperConfiguration<DefaultMapperConfiguration>();
            //dependencyManager.RegisterMapperConfiguration<BoomrangMapperConfiguration>();

            services.AddControllers();
            services.AddResponseCompression(opts =>
            {
                opts.Providers.Add<BrotliCompressionProvider>();
                opts.Providers.Add<GzipCompressionProvider>();
            })
                .Configure<BrotliCompressionProviderOptions>(opt => opt.Level = CompressionLevel.Fastest)
                .Configure<GzipCompressionProviderOptions>(opt => opt.Level = CompressionLevel.Fastest);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Boomrang-Api", Version = "v1" });
            });

            #endregion

            #region Configure middlewares

            dependencyManager.RegisterAspNetCoreMiddlewareUsing(aspNetCoreApp =>
            {
                if (!AspNetCoreAppEnvironmentsProvider.Current.HostingEnvironment.IsDevelopment())
                    aspNetCoreApp.UseHttpsRedirection();
                aspNetCoreApp.UseResponseCompression();
                //aspNetCoreApp.UseStaticFiles(new StaticFileOptions
                //{
                //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Assets")),
                //    RequestPath = new PathString("/media")
                //});

                aspNetCoreApp.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
                        {
                            MaxAge = TimeSpan.FromDays(365),
                            Public = true
                        };
                    }
                });

                aspNetCoreApp.UseRouting();

                aspNetCoreApp.UseSwagger();
                aspNetCoreApp.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Boomrang api v1"));
            });

            dependencyManager.RegisterAspNetCoreSingleSignOnClient();
            dependencyManager.Register<IODataModelBuilderProvider, BoomrangODataModelBuilderProvider>(lifeCycle: DependencyLifeCycle.SingleInstance);
            dependencyManager.RegisterAspNetCoreMiddlewareUsing(aspNetCoreApp =>
            {
                aspNetCoreApp.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers().RequireAuthorization();
                });
            });

            dependencyManager.RegisterMinimalAspNetCoreMiddlewares();

            dependencyManager.RegisterMetadata();

            dependencyManager.RegisterODataMiddleware(odataDependencyManager =>
            {
                odataDependencyManager.RegisterGlobalWebApiCustomizerUsing(httpConfiguration =>
                {
                    httpConfiguration.Filters.Add(new DefaultODataAuthorizeAttribute());
                    httpConfiguration.EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", $"Boomrang-Api");
                        c.ApplyDefaultODataConfig(httpConfiguration);
                    }).EnableBitSwaggerUi();
                });

                odataDependencyManager.RegisterWebApiODataMiddlewareUsingDefaultConfiguration();
            });

            dependencyManager.RegisterSingleSignOnServer<BoomrangUserService, BoomrangOAuthClientsProvider>();
            //dependencyManager.Register<IBoomrangDbService, BoomrangDbService>();

            dependencyManager.RegisterEfCoreDbContext<BoomrangDbContext>((serviceProvider, optionsBuilder) =>
            {
                //var appEnv = serviceProvider.GetRequiredService<AppEnvironment>();
                //string connectionString = appEnv.GetConfig<string>("AppConnectionString");
                optionsBuilder.UseSqlServer(BoomrangConfigurationProvider.GetConfiguration().GetConnectionString("AppConnectionString"));

                if (AspNetCoreAppEnvironmentsProvider.Current.HostingEnvironment.IsDevelopment())
                    optionsBuilder.EnableSensitiveDataLogging().EnableDetailedErrors();
            });

            dependencyManager.RegisterIndexPageMiddlewareUsingDefaultConfiguration();

            #endregion
        }

    }
}
