using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using NetFrame.Infrastructure.Caches;
using NetFrame.Infrastructure.WebToken;
using System.Globalization;
using System.Text;

namespace NetFrame.Infrastructure.Startup
{
    public static class BaseStartup
    {
        public static IServiceCollection AddBaseServices(this IServiceCollection services, Type startupType = null!, IConfiguration config = null!)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddCors(options =>
            {
                options.AddPolicy("NetFramePolicy",
                    builder => builder
                    .SetIsOriginAllowed(hostName => true)
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfos.JwtKey)),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });

            services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = long.MaxValue);

            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddTransient<IJwtFactory, JwtFactory>();
            services.TryAdd(ServiceDescriptor.Singleton<IMemoryCache, MemoryCache>());
            services.AddScoped<ICacheHelper, CacheHelper>();

            services.AddAutoMapper();

            services.AddMvc(option =>
            {
                option.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
                option.EnableEndpointRouting = false;
            })
           .AddNewtonsoftJson(options =>
           {
               options.SerializerSettings.Culture = new CultureInfo("tr-TR");
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
           });

            services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ElasticSearchLogActionFilter), 1);
                opt.Filters.Add(typeof(AntiXssActionFilter), 2);
                opt.Filters.Add(typeof(ValidatorActionFilter), 3);
            });
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
                
            return services;
        }
    }
}
