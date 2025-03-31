using Application.Data;
using Application.Products.Interfaces;
using Domain.Primitives;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        services.AddScoped<IProductDbContext>(sp =>
                sp.GetRequiredService<ProductDbContext>());

        services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ProductDbContext>());

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
