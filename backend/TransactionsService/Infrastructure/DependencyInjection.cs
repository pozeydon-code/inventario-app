using Application.Data;
using Application.Transactions.Interfaces;
using Domain.Primitives;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionsService.Application.ProductsGateway;
using TransactionsService.Infrastructure.Services;

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

        services.AddDbContext<TransactionDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        services.AddScoped<ITransactionDbContext>(sp =>
                sp.GetRequiredService<TransactionDbContext>());

        services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<TransactionDbContext>());

        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddHttpClient<IProductsApiClient, ProductsApiClient>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5106");
        });

        return services;
    }
}
