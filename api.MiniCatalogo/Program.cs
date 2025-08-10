using api.MiniCatalogo.Configuration.ApplicationServiceInjection;
using api.MiniCatalogo.Configuration.Seed;
using System.Reflection;

namespace api.MiniCatalogo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Generate();

            builder.AddApplicationServicesInject();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API.MiniCatalogo v1"));
            }

            app.Services.SeedDatabase();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            
            app.MapControllers();

            app.Run();
        }
    }
}
