using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using VertMagazineStore.Service;

namespace VertMagazineStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<App>().Execute().Wait();
        }


        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);
            services.AddResponseCaching();
            services.AddResponseCompression();
            services.AddMemoryCache();

            services.AddTransient<IAPI, API>();

            services.AddHttpClient<IAPI, API>(client =>
            {
                client.BaseAddress = new Uri(config.GetSection("API").GetSection("api-url").Value);
            });

            services.AddTransient<App>();
          

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
