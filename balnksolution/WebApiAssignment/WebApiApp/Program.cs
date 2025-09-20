
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApiApplication.Employee;
using WebApiData;
using WebApiData.EmployeeRepo;
using WebApiData.RoleRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace WebApiApp
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
            var connection = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(connection));

            builder.Services.AddTransient<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IEmployeeApplication, EmployeeApplication>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();


            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token. Example: Bearer {your token}"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
                });
            });

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", option =>
                {
                    var jwt = builder.Configuration.GetSection("Jwt");
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,

                        ValidAudience = jwt["Audience"],
                        ValidIssuer = jwt["Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                             Encoding.UTF8.GetBytes(jwt["key"])
                            ),
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                Console.WriteLine("request before next");
                await next();
                Console.WriteLine("response before next");



            });

            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            app.Use(async (context, next) =>
            {
                logger.LogWarning("Request path: {Path}", context.Request.Path);
                await next();
                logger.LogWarning("Response status: {Status}", context.Response.StatusCode);
            });

            app.MapControllers();

            app.Run();
        }
    }
}