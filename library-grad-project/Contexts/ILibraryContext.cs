using System.Data.Entity;
using LibraryGradProject.Models;

namespace LibraryGradProject.Contexts
{
    public interface ILibraryContext : System.IDisposable
    {
        IDbSet<Book> Books { get; set; }
        IDbSet<Reservation> Reservations { get; set; }

        int SaveChanges();
    }
}