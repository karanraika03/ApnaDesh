
using Microsoft.EntityFrameworkCore;
using ProfileApi.Countries;
using ProfileApi.Data;

namespace ProfileApi.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var connection = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<DataContext>( option =>
            option.UseSqlServer(connection));

            builder.Services.AddTransient<ICountryRepository, CountryRepository>();

            builder.Services.AddTransient<ICountryApplication, CountryApplication>();

            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {     
                app.UseSwagger();
            app.UseSwaggerUI();

                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
