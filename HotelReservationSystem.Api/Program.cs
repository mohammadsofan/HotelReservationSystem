using HotelReservationSystem.Api.Middlewares;
using HotelReservationSystem.Application.Extensions;
using HotelReservationSystem.Application.Settings;
using HotelReservationSystem.Infrastructure.Extensions;
using HotelReservationSystem.Infrastructure.Seeders;
using Microsoft.OpenApi.Models;
using Serilog;
namespace HotelReservationSystem.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateBootstrapLogger();
            var logger = Log.ForContext<Program>();
            logger.Information("Starting up Api");
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddControllers();
                builder.Host.UseSerilog((context, services, config) =>
                {
                    config.ReadFrom.Configuration(context.Configuration);
                    config.ReadFrom.Services(services)
                    .Enrich.FromLogContext();
                });
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(options =>
                {
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Enter 'Bearer' followed by your JWT token.\nExample: Bearer abcdef12345"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Authorization",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                        });
                });
                builder.Services.AddApplication();
                builder.Services.AddInfrastructure(builder.Configuration);
                builder.Services.Configure<JwtSettings>(
                        builder.Configuration.GetSection("JwtSettings"));
                builder.Services.AddAuthorization();
                builder.Services.AddTransient<ExceptionHandlingMiddleware>();
                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseMiddleware<ExceptionHandlingMiddleware>();
                app.UseHttpsRedirection();
                app.UseAuthentication();
                app.UseAuthorization();


                app.MapControllers();

                using (var scope = app.Services.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetRequiredService<HotelSeeder>();
                    await seeder.Seed();
                }
                app.Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "An unhandled exception occurred during bootstrapping");
            }
        }
    }
}
