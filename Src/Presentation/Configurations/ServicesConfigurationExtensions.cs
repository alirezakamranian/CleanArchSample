using Application.ArticleUsecases.AuthorizationProfie;
using Application.Common.Abstractions;
using Application.UtilityServicesAbstractions;
using Infrastructure.DataAccess;
using Infrastructure.UtilityServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Abstractions;
using Presentation.ExceptionHandlers;
using System.Text;
using System.Threading.RateLimiting;
using static System.Net.Mime.MediaTypeNames;

namespace Presentation.Configurations
{
    public static class ServicesConfigurationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
                                                                     //Asp.net core services

            //Auth
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["AuthOptions:Audience"],
                    ValidIssuer = builder.Configuration["AuthOptions:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration["AuthOptions:Key"]
                    ?? throw new NullReferenceException()))
                };
            });

            builder.Services.AddAuthorization();

            //Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            //DbContext
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer("Server=127.0.0.1;Database=CleanDb;User Id=SA;Password=12230500o90P;TrustServerCertificate=True"));

            services.AddScoped<IDataContext>(provider =>
                provider.GetRequiredService<DataContext>());

            //ExceptionHandling
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            // Configure rate limiting
            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("FixedForCreate", opt =>
                {
                    opt.Window = TimeSpan.FromMinutes(1);    
                    opt.PermitLimit = 2;                   
                    opt.QueueLimit = 0;                      
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                options.AddFixedWindowLimiter("FixedForGet", opt =>
                {
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.PermitLimit = 5;
                    opt.QueueLimit = 0;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                options.AddFixedWindowLimiter("FixedForUserRegister", opt =>
                {
                    opt.Window = TimeSpan.FromHours(1);
                    opt.PermitLimit = 1;
                    opt.QueueLimit = 0;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                options.OnRejected = async (onRejectedContext, cancellationToken) =>
                {
                    onRejectedContext.HttpContext.Response.StatusCode = 429;
                    onRejectedContext.HttpContext.Response.ContentType = "text/plain";
                    await onRejectedContext.HttpContext.Response.WriteAsync("Too many requests. Please try again later", cancellationToken);
                };
            });


            //InternalServices

            //MediatR
            services.AddMediatR(Config =>
            {
                Config.RegisterServicesFromAssembly(typeof(
                    Application.IApplicationAssemblyMarker).Assembly);
            });

            //UtilityServices
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddTransient<IUserManager, UserManager>();

            //AuthorizationHandlers
            builder.Services.AddTransient<IAuthorizationHandler, ArticleAuthorizationHandler>();
        }

        //EnpointsConfiguration
        public static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            var assembly = typeof(Presentation.IPresentationAssemblyMarker).Assembly;

            ServiceDescriptor[] serviceDescriptors = assembly.DefinedTypes.Where(
                type => type is { IsAbstract: false, IsInterface: false }
                    && type.IsAssignableTo(typeof(IEndpoint))).Select(type =>
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
