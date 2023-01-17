using Microsoft.Extensions.Configuration;
using SBFCrawler.Model;

namespace SBFCrawler.Shared.Extensions
{
    public static class LoginExtensions
    {
        public static ConfigLogin GetLoginDataFromConfig(this IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return new ConfigLogin
            {
                Username = configuration["Credentials:Username"]!,
                Password = configuration["Credentials:Password"]!
            };
        }

    }
}