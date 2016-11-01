using LibraryGradProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryGradProject.Repos
{
    public class FilledBookRepository : IRepository<Book>
    {
        public FilledBookRepository()
        {
            Book bookA = new Book
            {
                ISBN = "ABCD1234",
                Title = "Cool book",
                Author = "Book Writer",
                PublishDate = "01/01/1992",
            };
            Book bookB = new Book
            {
                ISBN = "EFGH5678",
                Title = "Lame Book",
                Author = "Booker Writeman",
                PublishDate = "02/03/1994",
            };
            Add(bookA);
            Add(bookB);
        }

        private List<Book> _bookCollection = new List<Book>();

        public void Add(Book entity)
        {
            entity.Id = _bookCollection.Count;
            _bookCollection.Add(entity);
        }

        public IEnumerable<Book> GetAll()
        {
            return _bookCollection;
        }

        public Book Get(int id)
        {
            return _bookCollection.Where(book => book.Id == id).SingleOrDefault();
        }

        public void Remove(int id)
        {
            Book bookToRemove = Get(id);
            _bookCollection.Remove(bookToRemove);
        }
    }
}