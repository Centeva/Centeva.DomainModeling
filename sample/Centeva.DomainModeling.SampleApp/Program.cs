using Centeva.DomainModeling;
using Centeva.DomainModeling.EFCore;
using Centeva.DomainModeling.SampleApp.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// MediatR
builder.Services
    .AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>())
    .AddSingleton<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
