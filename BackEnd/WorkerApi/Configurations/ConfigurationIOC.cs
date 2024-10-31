using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WorkerModels.Interface.Repository;
using WorkerModels.Interface.Services;
using WorkerModels.Interfaces.Security;
using WorkerModels.Security;
using WorkerRepositories.Repositories;
using WorkerSecurity.Authentication;
using WorkerServices.Services;

namespace WorkerApi.Configurations
{
    public static class ConfigurationIOC
    {
        public static IServiceCollection AddJWTAuthentication(this IServiceCollection service, IConfiguration config)
        {
            var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>()!;
            service.AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                   .AddJwtBearer(opt =>
                   {
                       opt.TokenValidationParameters = new TokenValidationParameters()
                       {
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                           ValidateIssuer = true,
                           ValidIssuer = jwtSettings.Issuer,
                           ValidateAudience = true,
                           ValidAudience = jwtSettings.Audience,
                           ClockSkew = TimeSpan.Zero,
                           ValidateLifetime = true,
                           LifetimeValidator = (before, expires, token, param) =>
                           {
                               return expires > DateTime.UtcNow;
                           }
                       };
                       opt.SaveToken = true;
                       opt.RequireHttpsMetadata = true;
                   });
            service.AddAuthorization();
            service.AddMemoryCache();

            return service;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWorkerRepository, WorkerRepository>();
            services.AddTransient<IPhonesRepository, PhonesRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IWorkerService, WorkerService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IJwtSettings, JwtSettings>();
            return services;
        }

        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Workers API", Version = "v1" });

                // Definindo o esquema de segurança JWT
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT com o prefixo 'Bearer ' (exemplo: 'Bearer seu_token_jwt')"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddCrossOrigin(this IServiceCollection services)
        {
            services.AddCors(delegate (CorsOptions opt)
            {
                opt.AddDefaultPolicy(delegate (CorsPolicyBuilder policy)
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                        .SetIsOriginAllowed((string o) => true);
                });
            });
            return services;
        }
    }

}