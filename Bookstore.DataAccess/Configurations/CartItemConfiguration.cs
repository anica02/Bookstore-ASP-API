using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DataAccess.Configurations
{
    public class CartItemConfiguration : EntityConfiguration<CartItem>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<CartItem> builder)
        {
            builder.Property(x => x.Quantity).IsRequired();
       
        }
    }

}
