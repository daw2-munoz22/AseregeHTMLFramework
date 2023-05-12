using AseregeBarcelonaWeb.Manager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AseregeBarcelonaWeb
{
	public class Program
	{
		public static void Main(string[] args)
		{
            MySQLManager manager = new MySQLManager();
			manager.CreateTables();
			manager.Dispose();
			
            CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
    }	
}
