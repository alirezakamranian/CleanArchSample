using Application.Common.Abstractions;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Presentation.Configurations
{
    public static class ServicesConfigurationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddMediatR(Config =>
            {
                Config.RegisterServicesFromAssembly(typeof(
                    Application.IApplicationAssemblyMarker).Assembly);
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer("Server=127.0.0.1;Database=CleanDb;User Id=SA;Password=12230500o90P;TrustServerCertificate=True"));

            services.AddScoped<IDataContext>(provider => 
                provider.GetRequiredService<DataContext>());
        }
    }
}
