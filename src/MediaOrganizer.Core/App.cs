using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MediaOrganizer.Core.ViewModels.Main;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Polly;
using MvvmCross;

namespace MediaOrganizer.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<MainViewModel>();
        }

        public override Task Startup()
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream("MediaOrganizer.Core.appsettings.json"))
            {
                IHost host = new HostBuilder()
                    .ConfigureHostConfiguration(c =>
                    {
                        c.AddJsonStream(stream);
                    })
                    .ConfigureServices((c, x) => ConfigureServices(c, x))
                    .ConfigureLogging(l => l.AddConsole(abc =>
                    {
                        abc.DisableColors = true;
                    }))
                    .Build();
            }

            return base.Startup();
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            Mvx.IoCProvider.RegisterSingleton(typeof(IConfiguration), ctx.Configuration);

            var world = ctx.Configuration["Hello"];
            var baseUrl = ctx.Configuration["BaseUrl"];

            if (ctx.HostingEnvironment.IsDevelopment())
            {
            }

            services.AddHttpClient("AzureWebSites", client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }));
        }
    }
}
