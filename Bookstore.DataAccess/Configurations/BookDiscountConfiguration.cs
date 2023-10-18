using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DataAccess.Configurations
{
    public class BookDiscountConfiguration : EntityConfiguration<BookDiscount>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<BookDiscount> builder)
        {
            builder.Property(x => x.DiscountPercentage).IsRequired();
            builder.Property(x => x.StartsFrom).IsRequired();
            builder.Property(x => x.EndsAt).IsRequired();
        }
    }
}
