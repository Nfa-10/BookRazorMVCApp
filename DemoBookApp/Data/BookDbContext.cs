using Microsoft.EntityFrameworkCore;

namespace DemoBookApp.Data
{
    public class BookDbContext:DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {

        }
        //desired OnModelCreating implementation

        //desired DbSets for tables creation

    }
}
