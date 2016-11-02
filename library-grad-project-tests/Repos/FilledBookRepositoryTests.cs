using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using LibraryGradProjectTests.Mocks;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using DeepEqual.Syntax;

namespace LibraryGradProjectTests.Repos
{
    public class FilledBookRepositoryTests
    {
        private Book bookA = new Book
        {
            Id = 1,
            ISBN = "ABCD1234",
            Title = "Cool book",
            Author = "Book Writer",
            PublishDate = "01/01/1992",
        };

        private Book bookB = new Book
        {
            Id = 2,
            ISBN = "EFGH5678",
            Title = "Lame Book",
            Author = "Booker Writeman",
            PublishDate = "02/03/1994",
        };

        [Fact]
        public void New_Filled_Book_Repository_Contains_2_Books()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            FilledBookRepository repo = new FilledBookRepository(mockDb);

            // Act
            IEnumerable<Book> books = repo.GetAll();

            // Assert
            books.ShouldDeepEqual(new Book[] { bookA, bookB });
        }

        [Fact]
        public void Add_Inserts_New_Book()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            FilledBookRepository repo = new FilledBookRepository(mockDb);
            Book newBook = new Book() { Title = "Test" };

            // Act
            repo.Add(newBook);
            IEnumerable<Book> books = repo.GetAll();

            // Assert
            books.ShouldDeepEqual(new Book[] { bookA, bookB, newBook });
        }

        [Fact]
        public void Add_Sets_New_Id()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            FilledBookRepository repo = new FilledBookRepository(mockDb);
            Book newBook = new Book() { Title = "Test" };

            // Act
            repo.Add(newBook);
            IEnumerable<Book> books = repo.GetAll();

            
            // Assert
            Assert.Equal(3, books.Last().Id);
        }

        [Fact]
        public void Get_Returns_Specific_Book()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            FilledBookRepository repo = new FilledBookRepository(mockDb);
            Book newBook1 = new Book() { Title = "Test1" };
            Book newBook2 = new Book() { Title = "Test2" };
            repo.Add(newBook1);
            repo.Add(newBook2);

            // Act
            Book book = repo.Get(4);

            // Assert
            Assert.Equal(newBook2, book);
        }

        [Fact]
        public void Get_All_Returns_All_Books()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            FilledBookRepository repo = new FilledBookRepository(mockDb);
            Book newBook1 = new Book() { Title = "Test1" };
            Book newBook2 = new Book() { Title = "Test2" };
            repo.Add(newBook1);
            repo.Add(newBook2);

            // Act
            IEnumerable<Book> books = repo.GetAll();

            // Assert
            books.ShouldDeepEqual(new Book[] { bookA, bookB, newBook1, newBook2 });
        }

        [Fact]
        public void Delete_Removes_Correct_Book()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            FilledBookRepository repo = new FilledBookRepository(mockDb);
            Book newBook1 = new Book() { Title = "Test1" };
            Book newBook2 = new Book() { Title = "Test2" };
            Book newBook3 = new Book() { Title = "Test3" };
            repo.Add(newBook1);
            repo.Add(newBook2);
            repo.Add(newBook3);

            // Act
            repo.Remove(4);
            IEnumerable<Book> books = repo.GetAll();

            // Assert
            books.ShouldDeepEqual(new Book[] { bookA, bookB, newBook1, newBook3 });
        }
    }
}
