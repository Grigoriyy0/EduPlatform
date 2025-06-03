using EduNEXT.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EduNEXT.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<TimeTableService>();
    }
}