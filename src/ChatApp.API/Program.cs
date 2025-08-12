using ChatApp.Application.Interfaces;
using ChatApp.Infrastructure.Data;
using ChatApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // JWT Configuration
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        // Add Authorization policies if needed
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
        });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
        });


        builder.Services.AddOpenApi();
        builder.Services.AddControllers();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });

        builder.Services.AddDbContext<ChatAppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Check DB connection before starting app
        using (var scope = app.Services.CreateScope())
        {
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("DefaultConnection");

            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();
                Console.WriteLine("Database connection successful.");
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to the database: " + ex.Message);
                return; // Exit app early if DB connection fails
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            // Use generated documentation
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "ChatApp API V1");
            });

            // Use Scalar API documentation
            //app.MapScalarApiReference("/scalar");
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors("AllowAll");
        app.UseHttpsRedirection();

        app.Run();
    }
}
