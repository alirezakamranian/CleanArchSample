using Application.Common.Abstractions;
using Application.UtilityServicesAbstractions;
using Infrastructure.DataAccess;
using Infrastructure.UtilityServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Presentation.Abstractions;
using Presentation.ExceptionHandlers;
using static System.Net.Mime.MediaTypeNames;

namespace Presentation.Configurations
{
    public static class ServicesConfigurationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            //MediatR
            services.AddMediatR(Config =>
            {
                Config.RegisterServicesFromAssembly(typeof(
                    Application.IApplicationAssemblyMarker).Assembly);
            });

            //Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //DbContext
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer("Server=127.0.0.1;Database=CleanDb;User Id=SA;Password=12230500o90P;TrustServerCertificate=True"));

            services.AddScoped<IDataContext>(provider => 
                provider.GetRequiredService<DataContext>());

            //ExceptionHandling
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            //UtilityServices
            services.AddTransient<IPasswordHasher, PasswordHasher>();

        }

        public static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            var assembly = typeof(Presentation.IPresentationAssemblyMarker).Assembly;

            ServiceDescriptor[] serviceDescriptors = assembly.DefinedTypes.Where(
                type => type is { IsAbstract: false, IsInterface: false } 
                    &&type.IsAssignableTo(typeof(IEndpoint))).Select(type =>
                        ServiceDescriptor.Transient(typeof(IEndpoint), type)).ToArray();

            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }

        public static IApplicationBuilder MapEndpoints(this WebApplication app)
        {
            IEnumerable<IEndpoint> endpoints = app.Services
                .GetRequiredService<IEnumerable<IEndpoint>>();

            foreach (IEndpoint endpoint in endpoints)
            {
                endpoint.MapEndpoint(app);
            }

            return app;
        }
    }
}
