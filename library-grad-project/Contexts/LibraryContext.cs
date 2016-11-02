using System.Data.Entity;
using LibraryGradProject.Models;

namespace LibraryGradProject.Contexts
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}