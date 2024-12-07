using Centeva.DomainModeling.SampleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Centeva.DomainModeling.SampleApp.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}
