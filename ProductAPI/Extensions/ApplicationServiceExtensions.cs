using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Helpers;

namespace ProductAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("ProductDB"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowCorsPolicy", c =>
                {
                    c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddScoped<IResponseHelper, ResponseHelper>();

            return services;
        }
    }
}
