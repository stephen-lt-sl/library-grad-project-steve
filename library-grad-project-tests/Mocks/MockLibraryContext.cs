using System.Data.Entity;
using LibraryGradProject.Models;
using LibraryGradProject.Contexts;
using FakeDbSet;

namespace LibraryGradProjectTests.Mocks
{
    class MockLibraryContext : ILibraryContext
    {
        public IDbSet<Book> Books { get; set; }
        public IDbSet<Reservation> Reservations { get; set; }

        public MockLibraryContext()
        {
            Books = new InMemoryDbSet<Book>();
            Reservations = new InMemoryDbSet<Reservation>();
        }

        public int SaveChanges()
        {
            int changeCount = 0;
            changeCount += DbSetHelper.IncrementPrimaryKey<Book>(b => b.Id, Books);
            changeCount += DbSetHelper.IncrementPrimaryKey<Reservation>(r => r.Id, Reservations);
            return changeCount;
        }

        public void Dispose()
        {

        }
    }
}
