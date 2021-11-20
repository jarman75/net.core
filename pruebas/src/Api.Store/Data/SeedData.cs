using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Store.Data
{
    public class SeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            SeedItems(builder);
            SeedStocks(builder);
        }

        private static void SeedStocks(ModelBuilder builder)
        {
            builder.Entity<ItemStock>().HasData(
                    new ItemStock
                    {
                        Id = Guid.Parse("1d3ab58e-0482-4dbb-8ac0-72117580e758"),
                        ItemId = Guid.Parse("53519ebf-9417-484e-b0a6-e78d44c6b2ad"),
                        ExpirationDate = DateTime.Now.AddDays(4),
                        ManufacturingDate = DateTime.Now.AddDays(-7),
                        Entrydate = DateTime.Now.AddDays(-7),
                        CostPrice = 1.05,
                        Price = 1.30
                    },
                    new ItemStock
                    {
                        Id = Guid.Parse("721a379a-cedb-4a47-ad03-0fe9af994433"),
                        ItemId = Guid.Parse("53519ebf-9417-484e-b0a6-e78d44c6b2ad"),
                        ExpirationDate = DateTime.Now.AddDays(2),
                        ManufacturingDate = DateTime.Now.AddDays(-4),
                        Entrydate = DateTime.Now.AddDays(-4),
                        CostPrice = 1.05,
                        Price = 1.30
                    },
                    new ItemStock
                    {
                        Id = Guid.Parse("61259bd6-634c-4a6a-a1ab-3910fde293e9"),
                        ItemId = Guid.Parse("53519ebf-9417-484e-b0a6-e78d44c6b2ad"),
                        ExpirationDate = DateTime.Now,
                        ManufacturingDate = DateTime.Now.AddDays(-3),
                        Entrydate = DateTime.Now.AddDays(-3),
                        CostPrice = 1.18,
                        Price = 1.30
                    },
                    new ItemStock
                    {
                        Id = Guid.Parse("a366cf93-c7b3-4e03-bd9e-e50313988385"),
                        ItemId = Guid.Parse("62a161e3-3eae-45dd-80bf-f7338ddc861b"),
                        ExpirationDate = null,
                        ManufacturingDate = DateTime.Now.AddYears(-3),
                        Entrydate = DateTime.Now.AddYears(-1),
                        CostPrice = 300.45,
                        Price = 450.99
                    },
                    new ItemStock
                    {
                        Id = Guid.Parse("4d1d8cae-fa94-4d1e-b22c-fe03829c4a85"),
                        ItemId = Guid.Parse("62a161e3-3eae-45dd-80bf-f7338ddc861b"),
                        ExpirationDate = null,
                        ManufacturingDate = DateTime.Now.AddYears(-10),
                        Entrydate = DateTime.Now.AddYears(-5),
                        CostPrice = 500.45,
                        Price = 750.99
                    },
                    new ItemStock
                    {
                        Id = Guid.Parse("1fa4eca3-6cfa-4968-a3a8-d6a3513b36c7"),
                        ItemId = Guid.Parse("62a161e3-3eae-45dd-80bf-f7338ddc861b"),
                        ExpirationDate = null,
                        ManufacturingDate = DateTime.Now.AddYears(-50),
                        Entrydate = DateTime.Now.AddYears(-1),
                        CostPrice = 2500.85,
                        Price = 3200.99
                    },
                    new ItemStock
                    {
                        Id = Guid.Parse("ce954070-523c-4182-a67b-698470354c02"),
                        ItemId = Guid.Parse("3ad59e5b-614c-47a2-b1a1-eda1e7d86b4d"),
                        ExpirationDate = null,
                        CostPrice = 4.15,
                        Price = 3.30
                    }

            );
        }

        private static void SeedItems(ModelBuilder builder)
        {
            builder.Entity<Item>().HasData(
                        new Item
                        {
                            Id = Guid.Parse("53519ebf-9417-484e-b0a6-e78d44c6b2ad"),
                            Name = "Bottle of fresh milk",
                            Category = Category.Perishable
                        },
                        new Item
                        {
                            Id = Guid.Parse("62a161e3-3eae-45dd-80bf-f7338ddc861b"),
                            Name = "Bottle of grand reserve wine",
                            Category = Category.Aged
                        },
                        new Item
                        {
                            Id = Guid.Parse("3ad59e5b-614c-47a2-b1a1-eda1e7d86b4d"),
                            Name = "Toilet Paper",
                            Category = Category.Normal
                        }
            );
        }
    }
}