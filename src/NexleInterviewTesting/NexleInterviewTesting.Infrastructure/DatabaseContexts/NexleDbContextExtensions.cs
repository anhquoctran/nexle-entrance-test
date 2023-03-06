using EntityFramework.Exceptions.MySQL.Pomelo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NexleInterviewTesting.Infrastructure.DatabaseContexts
{
    public static class NexleDbContextExtensions
    {
        public static IServiceCollection AddNexleDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<NexleDbContext>(c =>
            {
                var connString = config.GetConnectionString("Default");
                try
                {
                    c.UseMySql(connString, ServerVersion.AutoDetect(connString), o =>
                    {
                        o.EnableRetryOnFailure();
                    })
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .UseExceptionProcessor();
                }
                catch (Exception)
                {

                }
                
            });
            return services;
        }
    }
}
