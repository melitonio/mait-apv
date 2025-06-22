using Persistence;
using DotNetCore.CAP;
using MAIT.DataAccess;
using MAIT.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;
using Subscribers;
using Interfaces;
using Dto;


var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.SingleLine = false;
    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
    options.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Enabled;
});

builder.Services.AddHealthChecks();
builder.Services.Configure<DbOptions>(builder.Configuration.GetSection(nameof(DbOptions)));

// 1. Lee Data/GQ.json como fuente de configuración
builder.Configuration.AddJsonFile("Data/GQ.json", optional: false, reloadOnChange: false);

// 2. Enlaza los valores a tu POCO GqData
builder.Services.Configure<GqDataDto>(builder.Configuration);

builder.Services.AddSingleton<IDataService, DataService<ServiceDbContext>>();
builder.Services.AddSingleton<IApvService, ApvService>();
builder.Services.AddSingleton<ZonaPostalService>();
builder.Services.AddSingleton<LocalizacionService>();

builder.Services.AddDbContext<ServiceDbContext>(options =>
{
    var config = builder.Configuration.GetSection(nameof(DbOptions)).Get<DbOptions>();
    options.UseNpgsql(config!.ConnectionString);
});

builder.Services.AddCap(x =>
{
    x.UseEntityFramework<ServiceDbContext>();
    var rab = builder.Configuration.GetSection(nameof(RabbitMQOptions)).Get<RabbitMQOptions>();
    x.UseRabbitMQ(rabbitMqOptions =>
    {
        rabbitMqOptions.HostName = rab!.HostName;
        rabbitMqOptions.UserName = rab.UserName;
        rabbitMqOptions.Password = rab.Password;
        rabbitMqOptions.VirtualHost = rab.VirtualHost;
    });
    x.UseDashboard(); // opcional para ver /cap
});

builder.Services.AddTransient<ExpedienteEventHandler>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Apv service",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Document Series API v1");
        options.RoutePrefix = "swagger"; // disponible en /swagger
    });
}

var dataService = app.Services.GetRequiredService<IApvService>();
app.MapHealthChecks("/healthz");
app.Run();

public partial class Program { } // <--- Esto es clave para que WebApplicationFactory lo encuentre
