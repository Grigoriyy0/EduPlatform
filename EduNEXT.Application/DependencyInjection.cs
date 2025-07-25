using EduNEXT.Application.Commands.Student.AddStudentCommand;
using Microsoft.Extensions.DependencyInjection;

namespace EduNEXT.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(AddStudentCommand).Assembly);
        });
    }
}