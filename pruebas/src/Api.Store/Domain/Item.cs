using System;
using System.Collections.Generic;

namespace Api.Store.Domain
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }                
        public List<ItemStock> Stocks { get; set; }

    }
    public class ItemStock
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public DateTime? Entrydate { get; set; }
        public double CostPrice { get; set; }
        public double Price { get; set; }

    }

    public enum Category
    {        
        Normal = 0,        
        Aged = 1,        
        Perishable = 2
    }
}
