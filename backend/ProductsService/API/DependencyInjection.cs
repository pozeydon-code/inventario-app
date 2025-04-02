using API.Middlewares;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalHost", policy =>
                                  policy.WithOrigins(allowedOrigins!)
                                        .AllowAnyHeader()
                                        .AllowAnyMethod());
        });
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<GloblalExceptionHandlingMiddleware>();
        return services;
    }
}
