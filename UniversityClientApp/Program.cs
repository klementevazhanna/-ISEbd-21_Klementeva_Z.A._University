using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using UniversityContracts.ViewModels;

namespace UniversityClientApp
{
    public class Program
    {
        public static UserViewModel User { get; set; }

        public static void Main(string[] args)
        {
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
