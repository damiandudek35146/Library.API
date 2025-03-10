using Library.Domain.Interfaces;
using Library.Infrastructure.Repositories;
using Polly;
using MediatR;
using System.Reflection;
using Serilog.Events;
using Serilog;
using Library.Infrastructure;
using Library.Application;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHttpClient("BookService")
//    .AddPolicyHandler(HttpPolicyExtensions
//        .HandleTransientHttpError()
//        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSerilogRequestLogging();

app.MapControllers();

try
{
    Log.Information("Running app...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Critical error when starting the application");
}
finally
{
    Log.CloseAndFlush(); // Zamyka strumieñ logów po zakoñczeniu dzia³ania aplikacji
}

