using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DataAccess.Configurations
{
    public class FileConfiguration : EntityConfiguration<File>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<File> builder)
        {
            builder.Property(x => x.Path).IsRequired().HasMaxLength(250);
            builder.HasIndex(x => x.Path).IsUnique();
            builder.Property(x => x.Size).IsRequired();

            builder.HasOne(x => x.BookPublisher).WithOne(x => x.Image).HasForeignKey<File>(x => x.BookPublisherId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        
        }
    }
}
