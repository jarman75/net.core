using Api.Store.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Store.Data
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {            
            builder.HasKey(item => item.Id);
        }
    }

    public class ItemStockConfiguration : IEntityTypeConfiguration<ItemStock>
    {
        public void Configure(EntityTypeBuilder<ItemStock> builder)
        {
            builder.HasKey(stock => stock.Id);
            builder.HasOne<Item>().WithMany().HasForeignKey(stock => stock.ItemId);
        }
    }
}
