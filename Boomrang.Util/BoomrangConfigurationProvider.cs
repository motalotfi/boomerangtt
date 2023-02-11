using Microsoft.Extensions.Configuration;
using System.IO;

namespace Boomrang.Util
{
    public class BoomrangConfigurationProvider
    {
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();
        }
    }
}
