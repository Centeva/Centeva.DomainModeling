using Centeva.DomainModeling;
using Centeva.DomainModeling.EFCore;
using Centeva.DomainModeling.Mediator;
using Centeva.DomainModeling.SampleApp.BankAccounts;
using Centeva.DomainModeling.SampleApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Mediator
builder.Services
    .AddMediator()
    .AddSingleton<IDomainEventDispatcher, MediatorDomainEventDispatcher>();

// Configure EF Core
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = Path.Join(path, "Centeva.DomainModeling.SampleApp.db");

builder.Services.AddSingleton<DispatchDomainEventsInterceptor>();
builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    options.AddInterceptors(sp.GetRequiredService<DispatchDomainEventsInterceptor>());
    options.UseSqlite($"Data Source={dbPath}");
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

builder.Services.AddAuthorization();

var app = builder.Build();

// Create database / run migrations
if (app.Environment.IsDevelopment())
{
    await using var serviceScope = app.Services.CreateAsyncScope();
    await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Map minimal API endpoints
app.MapBankAccountEndpoints();

app.Run();
