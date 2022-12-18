using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetFrame.Infrastructure.WebToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace NetFrame.Infrastructure.Startup
{
    public static class BaseStartup
    {
        public static IServiceCollection AddBaseServices(this IServiceCollection services, IConfiguration config = null!, Type type = null!) 
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
                    //.AllowAnyOrigin()
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

            return services;
        }
    }
}
