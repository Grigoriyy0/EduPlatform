using EduNEXT.Application.Commands.AddStudentCommand;
using EduNEXT.Application.Services;
using EduNEXT.Application.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace EduNEXT.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<TimeTableService>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(AddStudentCommand).Assembly);
        });

        services.AddSingleton<InMemoryRepository>();
    }
}