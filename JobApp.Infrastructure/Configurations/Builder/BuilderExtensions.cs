using JobApp.Domain.Models;
using JobApp.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Text;

namespace JobApp.Infrastructure.Configurations.Builder
{
    public static class BuilderExtensions
    {
        public static void AddInfrastructure(this IHostApplicationBuilder builder)
        {
            // Register infrastructure services
            builder.Services.AddDatabase(builder.Configuration);
            // Add other infrastructure services as needed
        }

        private static void AddDatabase(this IServiceCollection services, IConfigurationManager configuration)
        {
            // plano auditLog: Ajustar para permitir JSON

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.EnableDynamicJson();
            var dataSource = dataSourceBuilder.Build();

            // Register DbContext
            services.AddDbContext<JobAppDbContext>(options =>
                options.UseNpgsql(dataSource));

            // Register repositories, services, etc.
            // services.AddScoped<IApplicantRepository, ApplicantRepository>();
            // services.AddScoped<ICompanyRepository, CompanyRepository>();
            // services.AddScoped<IJobPostRepository, JobPostRepository>();

            // Add other infrastructure services as needed
        }
    }
}
