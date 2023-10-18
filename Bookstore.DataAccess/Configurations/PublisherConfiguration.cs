using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DataAccess.Configurations
{
    public class PublisherConfiguration : EntityConfiguration<Publisher>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Publisher> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);
            builder.Property(x => x.Location).IsRequired().HasMaxLength(100);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.Website).IsUnique();
            builder.HasMany(x => x.BookPublishers).WithOne(x => x.Publisher).HasForeignKey(x => x.PublisherId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
