using System.Data.Common;
using MartinCostello.SqlLocalDb;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Centeva.DomainModeling.IntegrationTests.Fixtures;

public sealed class SharedDatabaseFixture : IDisposable
{
    // Docker
    public const string ConnectionStringDocker = "Data Source=database;Initial Catalog=CentevaDomainModelingEFCoreTests;PersistSecurityInfo=True;User ID=sa;Password=Centeva1234!";

    // (localdb)
    public const string ConnectionStringLocalDb = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=CentevaDomainModelingEFCoreTests;ConnectRetryCount=0";

    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public SharedDatabaseFixture()
    {
        var isLocalDbInstalled = false;

        using (var localDb = new SqlLocalDbApi())
        {
            isLocalDbInstalled = localDb.IsLocalDBInstalled();
        }

        Connection = isLocalDbInstalled
                    ? new SqlConnection(ConnectionStringLocalDb)
                    : new SqlConnection(ConnectionStringDocker);

        Seed();

        Connection.Open();
    }

    public DbConnection Connection { get; }

    public TestDbContext CreateContext(DbTransaction? transaction = null)
    {
        var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>().UseSqlServer(Connection).Options);

        if (transaction != null)
        {
            context.Database.UseTransaction(transaction);
        }

        return context;
    }

    private void Seed()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }

                _databaseInitialized = true;
            }
        }
    }

    public void Dispose() => Connection.Dispose();
}

