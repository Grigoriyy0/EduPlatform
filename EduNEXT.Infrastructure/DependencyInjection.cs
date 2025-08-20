using EduNEXT.Application.Ports;
using EduNEXT.Infrastructure.Adapters;
using EduNEXT.Infrastructure.Adapters.Repositories;
using EduNEXT.Infrastructure.Messaging;
using EduNEXT.Infrastructure.Persistence.Contexts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace EduNEXT.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MainContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("Postgres")));
        services.AddScoped<IHashProvider, DefaultHashProvider>();
        services.AddScoped<IPasswordGenerator, DefaultPasswordGenerator>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITimeSlotsRepository, TimeSlotsRepository>();
        services.AddScoped<ILessonsRepository, LessonsRepository>();
        services.AddScoped<ISalaryRepository, SalaryRepository>();
        services.AddScoped<LessonScheduler>();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseRawJsonSerializer();
                
                cfg.Host(configuration.GetConnectionString("RabbitMq"), h =>
                {
                    h.Username(configuration["RabbitMQ:Username"]!);
                    h.Password(configuration["RabbitMQ:Password"]!);
                });
                
                
                cfg.Publish<Notification>(p =>
                {
                    p.Durable = true;
                    p.BindQueue("telegram-notification-exchange",  "telegram-notification-queue");
                });
            });
        });
        
        services.AddScoped<IPublisher, Publisher>();
    }
}