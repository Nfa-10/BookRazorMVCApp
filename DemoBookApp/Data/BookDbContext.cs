using DemoBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoBookApp.Data
{
    public class BookDbContext:DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorModel>()
                .Property(u => u.Gender)
                .HasConversion<int>();

            modelBuilder.Entity<BookModel>()
                .HasOne(s => s.Author)
                .WithMany(s => s.Books)
                .HasForeignKey(s => s.AuthorID)
                .OnDelete(DeleteBehavior.NoAction);

        }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<AuthorModel> Author { get; set; } 



    }
}
