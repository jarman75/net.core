using System.Data.Common;
using Npgsql;
using Testcontainers.PostgreSql;

namespace Integration.Test;

public sealed class PostgreSqlContainerTest : IAsyncLifetime
{
  private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("test-db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithCleanUp(true)
        .Build();        

  public Task InitializeAsync()
  {
    return _postgreSqlContainer.StartAsync();
  }

  public Task DisposeAsync()
  {
    return _postgreSqlContainer.DisposeAsync().AsTask();
  }

  [Fact]
  public void ExecuteCommand()
  {
    using DbConnection connection = new NpgsqlConnection(_postgreSqlContainer.GetConnectionString());
    using DbCommand command = new NpgsqlCommand();
    connection.Open();
    command.Connection = connection;
    command.CommandText = "SELECT 1";
    command.ExecuteReader();

  }
}
