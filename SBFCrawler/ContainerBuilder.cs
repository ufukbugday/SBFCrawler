using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SBFCrawler.Services;

namespace SBFCrawler
{
    public class ContainerBuilder
    {
        public ContainerBuilder(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public IServiceProvider Build()
        {
            var services = new ServiceCollection();
            services.AddTransient<IChromiumWebBrowserService, ChromiumWebBrowserService>();
            services.AddSingleton(Configuration);
            
            services.AddScoped<Program>();

            return services.BuildServiceProvider();
        }
    }
}
