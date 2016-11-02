using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using LibraryGradProjectTests.Mocks;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace LibraryGradProjectTests.Repos
{
    public class BookRepositoryTests
    {
        [Fact]
        public void New_Book_Repository_Is_Empty()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            BookRepository repo = new BookRepository(mockDb);

            // Act
            IEnumerable<Book> books = repo.GetAll();

            // Assert
            Assert.Equal(new Book[] { }, books);
        }

        [Fact]
        public void Add_Inserts_New_Book()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            BookRepository repo = new BookRepository(mockDb);
            Book newBook = new Book() { Title = "Test" };

            // Act
            repo.Add(newBook);

            // Assert
            Assert.Equal(newBook, mockDb.Books.First());
        }

        [Fact]
        public void Add_Sets_New_Id()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            BookRepository repo = new BookRepository(mockDb);
            Book newBook = new Book() { Title = "Test" };

            // Act
            repo.Add(newBook);
            IEnumerable<Book> books = repo.GetAll();

            // Assert
            Assert.Equal(1, books.First().Id);
        }

        [Fact]
        public void Get_Returns_Specific_Book()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            BookRepository repo = new BookRepository(mockDb);
            Book newBook1 = new Book() { Title = "Test1" };
            Book newBook2 = new Book() { Title = "Test2" };
            repo.Add(newBook1);
            repo.Add(newBook2);

            // Act
            Book book = repo.Get(2);

            // Assert
            Assert.Equal(newBook2, book);
        }

        [Fact]
        public void Get_All_Returns_All_Books()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            BookRepository repo = new BookRepository(mockDb);
            Book newBook1 = new Book() { Title = "Test1" };
            Book newBook2 = new Book() { Title = "Test2" };
            repo.Add(newBook1);
            repo.Add(newBook2);

            // Act
            IEnumerable<Book> books = repo.GetAll();

            // Assert
            Assert.Equal(new Book[] { newBook1, newBook2 }, books.ToArray());
        }

        [Fact]
        public void Delete_Removes_Correct_Book()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            BookRepository repo = new BookRepository(mockDb);
            Book newBook1 = new Book() { Title = "Test1" };
            Book newBook2 = new Book() { Title = "Test2" };
            Book newBook3 = new Book() { Title = "Test3" };
            repo.Add(newBook1);
            repo.Add(newBook2);
            repo.Add(newBook3);

            // Act
            repo.Remove(2);
            IEnumerable<Book> books = repo.GetAll();

            // Assert
            Assert.Equal(new Book[] { newBook1, newBook3 }, books.ToArray());
        }
    }
}
