using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Attendance
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                        .UseKestrel()
                        .UseStartup<Startup>()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .Build();

            host.Run();
        }
    }
}