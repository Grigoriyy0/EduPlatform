using EduNEXT.Application;
using EduNEXT.Application.Ports;
using EduNEXT.Infrastructure;
using EduNEXT.Infrastructure.Adapters;
using EduNEXT.Infrastructure.Persistence.Contexts;
using EduNEXT.Infrastructure.Persistence.Utils;
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
            
            x.UsePostgreSqlStorage(opt => 
                opt.UseNpgsqlConnection(builder.Configuration.GetConnectionString("Hangfire")));
        });
        
        builder.Services.AddHangfireServer();
        
        builder.Services.AddCors(opt =>
            opt.AddPolicy("DefaultCors", config =>
            {
                config.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials().SetIsOriginAllowed(origin =>
                    {
                        if (origin.ToLower().StartsWith("http://localhost")) return true;
                        if (origin.ToLower().StartsWith("https://knigolub.dev")) return true;
                        return false;
                    });
            })
        );

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<MainContext>();
            var hashProvider = scope.ServiceProvider.GetRequiredService<IHashProvider>();
            
            context.Initialize(builder.Configuration, hashProvider);
        }
        
        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.UseCors("DefaultCors");
        
        app.UseHangfireDashboard();
        
        RecurringJob.AddOrUpdate<LessonScheduler>(
            "monthly-lesson-scheduler", 
            x => x.PlanLessonsForNextMonthAsync(CancellationToken.None), 
            "0 0 1 * *"
            );
        
        app.MapControllers();

        app.Run();
    }
}