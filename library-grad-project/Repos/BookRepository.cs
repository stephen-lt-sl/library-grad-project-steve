using LibraryGradProject.Models;
using LibraryGradProject.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace LibraryGradProject.Repos
{
    public class BookRepository : IRepository<Book>
    {
        private ILibraryContext _dbContext;

        public BookRepository(ILibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Book entity)
        {
            _dbContext.Books.Add(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Book> GetAll()
        {
            var query = from b in _dbContext.Books
                        orderby b.Id
                        select b;
            return query.AsEnumerable();
        }

        public Book Get(int id)
        {
            var query = from b in _dbContext.Books
                        where b.Id == id
                        select b;
            return query.AsEnumerable().SingleOrDefault();
        }

        public void Remove(int id)
        {
            Book bookToRemove = Get(id);
            _dbContext.Books.Remove(bookToRemove);
            _dbContext.SaveChanges();
        }
    }
}