using Scalar.AspNetCore;

namespace JobApp.Api.Configuration.Pipeline
{
    public static class PipelineExtensions
    {
        public static void UseApi(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.ConfigureScallar();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
        }

        private static void ConfigureScallar(this WebApplication app)
        {
            app.MapScalarApiReference("scalar", options =>
            {
                options.AddPreferredSecuritySchemes("BearerAuth")
                        .AddHttpAuthentication("BearerAuth", auth =>
                        {
                            auth.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";
                        });
    
                options.Title = "Testando Scalar no lugar do SwaggerUI";
                options.Theme = ScalarTheme.Mars; // Exemplo de uso das propriedades que você listou
                options.ShowSidebar = true;
                options.OpenApiRoutePattern = "/openapi/{documentName}.json";
            });
        }
    }
}
