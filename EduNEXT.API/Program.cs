using EduNEXT.Application;
using EduNEXT.Infrastructure;
using EduNEXT.Infrastructure.Adapters;
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
        
        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.UseCors("DefaultCors");
        
        app.UseHangfireDashboard();
        
        using (var scope = app.Services.CreateScope())
        {
            var recurringJobs = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
            var scheduler = scope.ServiceProvider.GetRequiredService<LessonScheduler>();

            recurringJobs.AddOrUpdate(
                "monthly-lesson-scheduler",
                () => scheduler.PlanLessonsForNextMonthAsync(CancellationToken.None),
                "0 0 1 * *" // каждый месяц 1 числа в 00:00
            );
        }
        
        app.MapControllers();

        app.Run();
    }
}