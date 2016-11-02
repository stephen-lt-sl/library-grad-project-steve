using LibraryGradProject.Models;
using LibraryGradProject.Contexts;
using LibraryGradProject.Repos;
using System.Collections.Generic;
using System.Linq;

namespace LibraryGradProject.Repos
{
    public class FilledBookRepository : BookRepository
    {
        public FilledBookRepository(ILibraryContext dbContext) : base(dbContext)
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
    }
}