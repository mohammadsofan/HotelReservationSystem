using HotelReservationSystem.Api.Middlewares;
using HotelReservationSystem.Application.Extensions;
using HotelReservationSystem.Application.Settings;
using HotelReservationSystem.Infrastructure.Extensions;
using HotelReservationSystem.Infrastructure.Seeders;
namespace HotelReservationSystem.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.Configure<JwtSettings>(
                    builder.Configuration.GetSection("JwtSettings"));
            builder.Services.AddAuthorization();
            builder.Services.AddTransient<ExceptionHandlingMiddleware>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope()) {
                var seeder = scope.ServiceProvider.GetRequiredService<HotelSeeder>();
                await seeder.Seed();
            }
            app.Run();
        }
    }
}
