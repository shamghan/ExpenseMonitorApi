using Microsoft.EntityFrameworkCore;
using ExpenseMonitor.Infrastructure.Persistence;
using ExpenseMonitor.Infrastructure.Extensions;
using ExpenseMonitor.Infrastructure.Seeders;
using ExpenseMonitor.Application.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using ExpenseMonitor.API.Middleware;
using ExpenseMonitor.Domain.Entities;
using Microsoft.OpenApi.Models;
using ExpenseMonitor.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


new CompactJsonFormatter();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);
var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantsSeeder>();
await seeder.Seed();
// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }//by default program.cs class is internal, so make this public class