using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DataAccess.Configurations
{
    public class BookPublisherConfiguration : EntityConfiguration<BookPublisher>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<BookPublisher> builder)
        {

            builder.Property(x => x.BookCover).IsRequired();
            builder.Property(x => x.BookFormat).IsRequired();
            builder.Property(x => x.BookWritingSystem).IsRequired();
            builder.Property(x => x.NumberOfPages).IsRequired();
            builder.Property(x => x.Year).IsRequired();

            builder.HasMany(x => x.Prices).WithOne(x => x.BookPublisher).HasForeignKey(x => x.BookPublisherId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
            builder.HasMany(x => x.Discounts).WithOne(x => x.BookPublisher).HasForeignKey(x => x.BookPublisherId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasMany(x => x.CartItems).WithOne(x => x.BookPublisher).HasForeignKey(x => x.BookPublisherId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
            builder.HasMany(x => x.OrderItems).WithOne(x => x.BookPublisher).HasForeignKey(x => x.BookPublisherId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
