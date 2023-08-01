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


        /*public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

    public virtual async Task<int> SaveChangesAsync()
    {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is Auditable && 
            (x.State == EntityState.Added || x.State == EntityState.Modified));



        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                ((Auditable)entity.Entity).DateCreated = DateTime.UtcNow;

            }
         ((Auditable)entity.Entity).DateModified = DateTime.UtcNow;

        }
    }*/
        /*public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Auditable && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((Auditable)entityEntry.Entity).DateUpdated = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((Auditable)entityEntry.Entity).DateCreated = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }*/
        public DbSet<BookModel> Books { get; set; }
        public DbSet<AuthorModel> Author { get; set; } 



    }
}
