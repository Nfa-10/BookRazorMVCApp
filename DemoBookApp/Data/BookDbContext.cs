using DemoBookApp.Audit;
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

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
           bool acceptAllChangesOnSuccess,
           CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,
                          cancellationToken));
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                // for entities that inherit from BaseEntity,
                // set UpdatedOn / CreatedOn appropriately
                if (entry.Entity is Auditable trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            // set the updated date to "now"
                            trackable.DateModified = utcNow;

                            // mark property as "don't touch"
                            // we don't want to update on a Modify operation
                            entry.Property("DateCreated").IsModified = false;
                            break;

                        case EntityState.Added:
                            // set both updated and created date to "now"
                            trackable.DateCreated = utcNow;
                            trackable.DateModified = utcNow;
                            break;
                    }
                }
            }
        }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<AuthorModel> Author { get; set; } 

        public DbSet<UserModel> Users { get; set; }



    }
}
