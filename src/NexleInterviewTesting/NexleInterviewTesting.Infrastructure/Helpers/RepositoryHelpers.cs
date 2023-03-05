using Microsoft.Extensions.DependencyInjection;
using NexleInterviewTesting.Domain.Repositories;
using NexleInterviewTesting.Domain.UnitOfWorks;
using NexleInterviewTesting.Infrastructure.DatabaseContexts;

namespace NexleInterviewTesting.Infrastructure.Helpers
{
    public static class RepositoryHelpers
    {
        public static IServiceCollection AddUnitOfWorkAndRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User, int>, Repository<User>>(f =>
            {
                var dbContext = f.GetRequiredService<NexleDbContext>();
                return new Repository<User>(dbContext);
            });

            services.AddScoped<IRepository<Token, int>, Repository<Token>>(f =>
            {
                var dbContext = f.GetRequiredService<NexleDbContext>();
                return new Repository<Token>(dbContext);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>(f =>
            {
                var dbContext = f.GetRequiredService<NexleDbContext>();
                return new UnitOfWork(dbContext);
            });

            return services;
        }
    }
}
