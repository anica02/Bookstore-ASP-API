using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DataAccess.Configurations
{
    public class AuthorConfiguration : EntityConfiguration<Author>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(35);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(35);
            builder.HasIndex(x => x.Pseudonym).IsUnique();
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.Country).IsRequired();
            builder.HasMany(x => x.BookAuthors).WithOne(x => x.Author).HasForeignKey(x => x.AuthorId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
