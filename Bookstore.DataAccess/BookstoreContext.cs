
using Bookstore.Application;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bookstore.DataAccess
{
    public class BookstoreContext:DbContext
    {
        public BookstoreContext()
        {

        }

        public BookstoreContext(DbContextOptions opt):base(opt)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=Bookstore;User ID=SA;Password=14022002Ar&");
        }
            
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookstoreContext).Assembly);
            modelBuilder.Entity<BookAuthor>().HasKey(x => new { x.AuthorId,x.BookId });
            modelBuilder.Entity<BookGenre>().HasKey(x => new { x.GenreId, x.BookId });
            modelBuilder.Entity<Role>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Role>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Role>().Property(x => x.IsDefault).HasDefaultValue(false);
            modelBuilder.Entity<RoleUseCase>().HasKey(x => new { x.RoleId, x.UseCaseId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books  { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem>  CartItems { get; set; }
        public DbSet<Order> Orders  { get; set; }
        public DbSet<Price> Prices  { get; set; }
        public DbSet<BookDiscount> BookDiscounts  { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Publisher> Publishers  { get; set; }
        public DbSet<Genre> Genres  { get; set; }
        public DbSet<BookAuthor> BookAuthors  { get; set; }
        public DbSet<BookGenre> BookGenres  { get; set; }
        public DbSet<BookPublisher> BookPublishers  { get; set; }
        
        public DbSet<File> Files { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<RoleUseCase> RoleUseCases { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
    }
}
