using ContactsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
