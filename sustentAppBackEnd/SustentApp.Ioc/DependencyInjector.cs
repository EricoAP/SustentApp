using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SustentApp.Application.Users.Services;
using SustentApp.Application.Utils.Transactions;
using SustentApp.Application.Utils.Transactions.Abstractions;
using SustentApp.Domain.Users.Options;
using SustentApp.Domain.Users.Services;
using SustentApp.Infrastructure.Contexts;
using SustentApp.Infrastructure.Users.Repositories;

namespace SustentApp.Ioc;

public static class DependencyInjector
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SustentAppContext>(options =>
        {
            options.UseMySQL(configuration.GetConnectionString("MySql"));
        });

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.Scan(services =>
            services.FromAssemblyOf<UsersRepository>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        services.Scan(services =>
            services.FromAssemblyOf<UsersService>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        services.Scan(services =>
            services.FromAssemblyOf<UsersAppService>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("AppService")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
    }
}
