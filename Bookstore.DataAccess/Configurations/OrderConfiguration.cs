using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DataAccess.Configurations
{
    public class OrderConfiguration : EntityConfiguration<Order>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
            builder.Property(x => x.DeliveryMethod).IsRequired();
            builder.Property(x => x.PaymentMethod).IsRequired();
      
            builder.HasMany(x => x.OrderItems).WithOne(x => x.Order).HasForeignKey(x => x.OrderId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
      
        }
    }
}
