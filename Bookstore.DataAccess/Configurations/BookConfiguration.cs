
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DataAccess.Configurations
{
    public class BookConfiguration : EntityConfiguration<Book>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(90);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Code).IsRequired().HasMaxLength(10);
            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.Description).IsRequired();

          
        
        }
    }
}
