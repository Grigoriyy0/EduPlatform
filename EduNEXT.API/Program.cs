using EduNEXT.Application;
using EduNEXT.Infrastructure;
using Hangfire;
using Hangfire.PostgreSql;

namespace EduNEXT.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        builder.Services.AddHangfire(x =>
        {
            x.UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();
            
            x.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("Hangfire"), new PostgreSqlStorageOptions
            {
                PrepareSchemaIfNecessary = true
            });
        });
        
        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.UseHangfireDashboard();
        
        app.MapControllers();

        app.Run();
    }
}