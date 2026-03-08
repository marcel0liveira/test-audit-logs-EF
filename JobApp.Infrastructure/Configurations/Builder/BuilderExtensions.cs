using JobApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            // Register DbContext
            services.AddDbContext<JobAppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Register repositories, services, etc.
            // services.AddScoped<IApplicantRepository, ApplicantRepository>();
            // services.AddScoped<ICompanyRepository, CompanyRepository>();
            // services.AddScoped<IJobPostRepository, JobPostRepository>();

            // Add other infrastructure services as needed
        }
    }
}
