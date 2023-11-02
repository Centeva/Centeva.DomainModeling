using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Centeva.DomainModeling.IntegrationTests.Fixtures;

public sealed class SharedDatabaseFixture : IDisposable
{
    public SharedDatabaseFixture()
    {
        Connection = new SqliteConnection("Filename=:memory:");
        Connection.Open();
    }

    public DbConnection Connection { get; }

    public TestDbContext CreateContext()
    {
        var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseSqlite(Connection).Options);

        context.Database.EnsureCreated();

        return context;
    }

    public void Dispose() => Connection.Dispose();
}

