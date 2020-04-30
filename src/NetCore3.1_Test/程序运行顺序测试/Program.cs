using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace 程序运行顺序测试
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //入参 IWebHostBuilder
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("ConfigureWebHostDefaults");
                    webBuilder.UseStartup<Startup>();
                    Thread.Sleep(1000);
                    Console.WriteLine("ConfigureWebHostDefaults_1000");
                })
                //入参IConfigurationBuilder
                .ConfigureHostConfiguration(builder =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("ConfigureHostConfiguration");
                })
                //委托入参是：IConfigurationBuilder
                .ConfigureAppConfiguration(builder =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("ConfigureAppConfiguration");
                })
                //入参 IServiceCollection
                .ConfigureServices(services =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("ConfigureServices");
                });
    }
}
