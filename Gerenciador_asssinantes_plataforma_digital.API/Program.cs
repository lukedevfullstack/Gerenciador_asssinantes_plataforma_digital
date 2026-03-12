using Gerenciador_asssinantes_plataforma_digital.Application.Interfaces;
using Gerenciador_asssinantes_plataforma_digital.Application.Services;
using Gerenciador_asssinantes_plataforma_digital.Domain.Interfaces;
using Gerenciador_asssinantes_plataforma_digital.Infrastructure;
using Gerenciador_asssinantes_plataforma_digital.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. Database Configuration (SQLite used as an example for portability)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 2. Dependency Injection Mapping
builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();
builder.Services.AddScoped<ISubscriberService, SubscriberService>();

// 3. Controller Configuration with Enum-to-String conversion
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Displays Enums as Strings (Basic, Standard, Premium) instead of numbers in Swagger/JSON
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// 4. Swagger/OpenAPI Setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5. Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();