using HONORE_API_MAIN.Data;
using HONORE_API_MAIN.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Define a CORS policy name
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200") // Allow requests from this specific origin
                                 .AllowAnyHeader()    // Allow all headers (e.g., Content-Type, Authorization)
                                 .AllowAnyMethod();   // Allow all HTTP methods (GET, POST, PUT, DELETE, etc.)
                      });
});


// Configure Swagger/OpenAPI for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HonoreAPI", Version = "v1" });
});

// Configure HonoreDBContext to use SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HonoreDBContext>(options =>
    options.UseSqlServer(connectionString,
        sqlServerOptionsAction: sqlOptions =>
        {
            // Enable retry on failure for transient errors (e.g., network glitches, database restarts)
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));

// Register your ProductService for Dependency Injection.
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI in development environment for API documentation and testing
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HonoreAPI V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

// Use CORS middleware AFTER UseRouting and BEFORE UseAuthorization
app.UseCors(MyAllowSpecificOrigins); // Apply the defined CORS policy

app.UseAuthorization();

app.MapControllers();

app.Run();