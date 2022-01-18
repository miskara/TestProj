using Microsoft.Extensions.Configuration;
using System.IO;

namespace TestProj.Class
{
    public class AppSettings
    {
		public static IConfigurationRoot GetConfiguration(string environmentName = null, bool addUserSecrets = false)
		{
			var builder = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);

			if (!string.IsNullOrWhiteSpace(environmentName))
			{
				builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
			}

			builder = builder.AddEnvironmentVariables();

			return builder.Build();
		}

		public static string JsonTestDb => GetConfiguration().GetSection("ConnectionStrings")["JsonTestDb"];
	}
}

