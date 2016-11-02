using System.Data.Entity;
using LibraryGradProject.Models;

namespace LibraryGradProject.Contexts
{
    public class LibraryContext : DbContext, ILibraryContext
    {
        public IDbSet<Book> Books { get; set; }
        public IDbSet<Reservation> Reservations { get; set; }
    }
}