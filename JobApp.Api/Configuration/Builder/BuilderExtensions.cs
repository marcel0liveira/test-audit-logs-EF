using JobApp.Application.Provider;
using JobApp.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

namespace JobApp.Api.Configuration.Builder
{
    public static class BuilderExtensions
    {
        public static void AddApi(this IHostApplicationBuilder builder)
        {
            builder.AddJwt();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Add services to the container.
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddAuthorization();

            builder.Services.AddHttpContextAccessor();

            // plano auditLog: incluir
            builder.Services.AddScoped<ICurrentSessionProvider, CurrentSessionProvider>();
        }

        private static void AddJwt(this IHostApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");

            builder.Services.Configure<JwtSettings>(jwtSettings);
            var secretKey = jwtSettings["SecretKey"]!;
            var key = Encoding.ASCII.GetBytes(secretKey);

            // --- AUTHENTICATION ---
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                    // The options above are enough for the sample to work. For a real system,
                    // look into setting RequireSignedTokens, ClockSkew, NameClaimType, and RoleClaimType
                };
            });
        }        
    }
}
