using EduNEXT.Application.Ports;
using EduNEXT.Infrastructure.Adapters;
using EduNEXT.Infrastructure.Persistence.Contexts;
using EduNEXT.Infrastructure.Repositories;
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
    }
}