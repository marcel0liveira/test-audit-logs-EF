namespace JobApp.Api.Configuration.Builder
{
    public static class BuilderExtensions
    {
        public static void AddApi(this IHostApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Add services to the container.
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
        }
    }
}
