using System.Text;
using EduNEXT.Application;
using EduNEXT.Application.Ports;
using EduNEXT.Infrastructure;
using EduNEXT.Infrastructure.Adapters;
using EduNEXT.Infrastructure.Persistence.Contexts;
using EduNEXT.Infrastructure.Persistence.Utils;
using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace EduNEXT.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Description = "Please insert JWT token into field"
            });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });
        
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

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                var signinKeyBytes = Encoding.UTF8.GetBytes(builder.Configuration["TokenOptions:Secret"]!);

                opt.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["TokenOptions:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["TokenOptions:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signinKeyBytes)
                };
            });
        
        builder.Services.AddAuthorization();

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
            
            // Apply pending migrations automatically on startup
            context.Database.Migrate();

            context.Initialize(builder.Configuration, hashProvider);
        }
        
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseCors("DefaultCors");
        
        if (app.Environment.IsDevelopment())
        {
            app.UseHangfireDashboard("/api/hangfire", new DashboardOptions
            {
                Authorization = new[] { new AllowAllDashboardAuthorizationFilter() }
            });
        }
        else
        {
            app.UseHangfireDashboard("/api/hangfire");
        }
        
        RecurringJob.AddOrUpdate<LessonScheduler>(
            "monthly-lesson-scheduler", 
            x => x.PlanLessonsForNextMonthAsync(CancellationToken.None), 
            "0 0 1 * *"
            );
        
        app.MapControllers();

        app.Run();
    }
}

// Allow Hangfire Dashboard for all in Development environment
public sealed class AllowAllDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context) => true;
}