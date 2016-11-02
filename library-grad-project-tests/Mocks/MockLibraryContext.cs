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
            Books = new InMemoryDbSet<Book>(true);
            Reservations = new InMemoryDbSet<Reservation>(true);
        }

        public int SaveChanges()
        {
            int changeCount = 0;
            changeCount += DbSetHelper.IncrementPrimaryKey(b => b.Id, Books);
            changeCount += DbSetHelper.IncrementPrimaryKey(r => r.Id, Reservations);
            return changeCount;
        }

        public void Dispose()
        {

        }
    }
}
