﻿using Microsoft.EntityFrameworkCore;

namespace EfQuerysOptimized;

public class TestData
{
    //get DataContext with InMemoryDatabase
    public static DataContext GetContext(bool memory = true)
    {
        var options = memory 
            ? new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options
            : new DbContextOptionsBuilder<DataContext>()
            //postgresql
            .UseNpgsql("Host=localhost;Database=TestDb;Username=postgres;Password=postgres")
            .Options;

        var context = new DataContext(options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Accounts.AddRange(GetAccounts());
        context.SaveChanges();

        return context;
    }
    public static Account[] GetAccounts()
    {
        return new[]
        {
            new Account
            {
                Id = 1,
                Name = "Account 1",
                Description = "Account 1 Description",
                Balance = 1000,
                Transactions = new List<Transaction>
                {
                    new()
                    {
                        Id = 1,
                        Description = "Transaction 1",
                        Amount = 100,
                        Date = new DateTime(2021, 1, 1,0,0,0,DateTimeKind.Utc),
                        AccountId = 1
                    },
                    new()
                    {
                        Id = 2,
                        Description = "Transaction 2",
                        Amount = 200,
                        Date = new DateTime(2021, 1, 2,0,0,0,DateTimeKind.Utc),
                        AccountId = 1
                    },
                    new()
                    {
                        Id = 3,
                        Description = "Transaction 3",
                        Amount = 300,
                        Date = new DateTime(2021, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                        AccountId = 1
                    },
                    new()
                    {
                        Id = 4,
                        Description = "Transaction 4",
                        Amount = 400,
                        Date = new DateTime(2021, 1, 4, 0, 0, 0, DateTimeKind.Utc),
                        AccountId = 1
                    },
                    new()
                    {
                        Id = 5,
                        Description = "Transaction 5",
                        Amount = 500,
                        Date = new DateTime(2021, 1, 5, 0, 0, 0, DateTimeKind.Utc),
                        AccountId = 1
                    },
                    new()
                    {
                        Id = 6,
                        Description = "Transaction 6",
                        Amount = 600,
                        Date = new DateTime(2021, 1, 6, 0, 0, 0, DateTimeKind.Utc),
                        AccountId = 1
                    },
                    new()
                    {
                        Id = 7,
                        Description = "Transaction 7",
                        Amount = 700,
                        Date = new DateTime(2021, 1, 7, 0, 0, 0, DateTimeKind.Utc),
                        AccountId = 1
                    },
                    new()
                    {
                        Id = 8,
                        Description = "Transaction 8",
                        Amount = 800,
                        Date = new DateTime(2021, 1, 8, 0, 0, 0, DateTimeKind.Utc),
                        AccountId = 1
                    },
                    new()
                    {
                        Id = 9,
                        Description = "Transaction 9",
                        Amount = 900,
                        Date = new DateTime(2021, 1, 9, 0, 0, 0, DateTimeKind.Utc),
                        AccountId = 1
                    }
                }
            }
        };
    }
}
