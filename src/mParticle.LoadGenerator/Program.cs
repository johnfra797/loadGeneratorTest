using Microsoft.Extensions.DependencyInjection;
using mParticle.Data.Definitions;
using mParticle.Data.Implementations;
using System;
using mParticle.Domain.DTO;
using mParticle.LoadGenerator.services;
using Serilog;

namespace mParticle.LoadGenerator
{
    static class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IHttpRequestRepository, HttpRequestRepository>()
                .AddSingleton<ILoadGeneratorService, LoadGeneratorService>()
                .AddLogging(data=>data.AddSerilog())
                .BuildServiceProvider(); 
            
            string configFile = "config.json";
            if (args.Length > 0)
            {
                configFile = args[0];
            }

            ConfigRequestDTO config = ConfigRequestDTO.GetArguments(configFile);
            if (config == null)
            {
                Console.WriteLine("Failed to parse configuration.");
                return;
            }
            var loadGeneratorService = serviceProvider.GetService<ILoadGeneratorService>();
           
            loadGeneratorService.RequestHTTP(config);

        }
    }
}
