using LibraryGradProject.Controllers;
using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using System.Web.Http;
using Moq;
using Xunit;

namespace LibraryGradProjectTests.Controllers
{
    public class ReservationsControllerTests
    {
        [Fact]
        public void Get_Calls_Repo_GetAll()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Reservation>>();
            mockRepo.Setup(mock => mock.GetAll());
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            // Act
            controller.Get();

            // Assert
            mockRepo.Verify(mock => mock.GetAll(), Times.Once);
        }

        [Fact]
        public void Get_With_Id_Calls_Repo_Get()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Reservation>>();
            mockRepo.Setup(mock => mock.Get(It.IsAny<int>()));
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            // Act
            controller.Get(1);

            // Assert
            mockRepo.Verify(mock => mock.Get(It.Is<int>(x => x == 1)), Times.Once);
        }

        [Fact]
        public void Post_With_Reservation_Calls_Repo_Add()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Reservation>>();
            mockRepo.Setup(mock => mock.Add(It.IsAny<Reservation>()));
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            Reservation newReservation = new Reservation() { BookId = 0 };

            // Act
            controller.Post(newReservation);

            // Assert
            mockRepo.Verify(mock => mock.Add(It.Is<Reservation>(b => b == newReservation)), Times.Once);
        }

        [Fact]
        public void Post_With_Reservation_Returns_Ok_With_Int_If_Repo_Add_Not_Throws()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Reservation>>();
            mockRepo.Setup(mock => mock.Add(It.IsAny<Reservation>()));
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            Reservation newReservation = new Reservation() { BookId = 0 };

            // Act
            IHttpActionResult result = controller.Post(newReservation);

            // Assert
            Assert.IsType<System.Web.Http.Results.OkNegotiatedContentResult<int>>(result);
        }

        [Fact]
        public void Post_With_Reservation_Returns_Conflict_If_Repo_Add_Throws()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Reservation>>();
            mockRepo.Setup(mock => mock.Add(It.IsAny<Reservation>())).Throws(new System.Exception());
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            Reservation newReservation = new Reservation() { BookId = 0 };

            // Act
            IHttpActionResult result = controller.Post(newReservation);

            // Assert
            Assert.IsType<System.Web.Http.Results.ConflictResult>(result);
        }

        [Fact]
        public void Delete_With_Id_Calls_Repo_Remove()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Reservation>>();
            mockRepo.Setup(mock => mock.Remove(It.IsAny<int>()));
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            // Act
            controller.Delete(1);

            // Assert
            mockRepo.Verify(mock => mock.Remove(It.Is<int>(x => x == 1)), Times.Once);
        }

        [Fact]
        public void Put_With_Reservation_Calls_Repo_Get()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Reservation>>();
            mockRepo.Setup(mock => mock.Get(It.IsAny<int>()));
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            Reservation newReservation = new Reservation() { Id = 1, BookId = 0 };

            // Act
            controller.Put(newReservation);

            // Assert
            mockRepo.Verify(mock => mock.Get(It.Is<int>(x => x == 1)), Times.Once);
        }

        [Fact]
        public void Put_With_Reservation_Calls_Repo_Add_If_Repo_Get_Returns_Null()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Reservation>>();
            mockRepo.Setup(mock => mock.Get(It.IsAny<int>())).Returns<Reservation>(null);
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            Reservation newReservation = new Reservation() { Id = 1, BookId = 0 };

            // Act
            controller.Put(newReservation);

            // Assert
            mockRepo.Verify(mock => mock.Add(It.Is<Reservation>(b => b == newReservation)), Times.Once);
        }

        [Fact]
        public void Put_With_Reservation_Not_Calls_Repo_Add_If_Repo_Get_Returns_Reservation()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Reservation>>();
            mockRepo.Setup(mock => mock.Get(It.IsAny<int>())).Returns(new Reservation());
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            Reservation newReservation = new Reservation() { Id = 1, BookId = 0 };

            // Act
            controller.Put(newReservation);

            // Assert
            mockRepo.Verify(mock => mock.Add(It.Is<Reservation>(b => b == newReservation)), Times.Never);
        }

        [Fact]
        public void Put_With_Reservation_Sets_Reservation_Properties_If_Repo_Get_Returns_Reservation()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Reservation>>();
            Reservation oldReservation = new Reservation();
            mockRepo.Setup(mock => mock.Get(It.IsAny<int>())).Returns(oldReservation);
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            Reservation newReservation = new Reservation() { Id = 1, BookId = 0 };

            // Act
            controller.Put(newReservation);

            // Assert
            Assert.Equal(oldReservation.BookId, 0);
        }

        [Fact]
        public void Put_With_Reservation_Returns_Ok_With_Int_If_Repo_Add_Not_Throws()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Reservation>>();
            mockRepo.Setup(mock => mock.Get(It.IsAny<int>())).Returns<Reservation>(null);
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            Reservation newReservation = new Reservation() { Id = 1, BookId = 0 };

            // Act
            IHttpActionResult result = controller.Put(newReservation);

            // Assert
            Assert.IsType<System.Web.Http.Results.OkNegotiatedContentResult<int>>(result);
        }

        [Fact]
        public void Put_With_Reservation_Returns_Conflict_If_Repo_Add_Throws()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Reservation>>();
            mockRepo.Setup(mock => mock.Get(It.IsAny<int>())).Returns<Reservation>(null);
            mockRepo.Setup(mock => mock.Add(It.IsAny<Reservation>())).Throws(new System.Exception());
            ReservationsController controller = new ReservationsController(mockRepo.Object);

            Reservation newReservation = new Reservation() { Id = 1, BookId = 0 };

            // Act
            IHttpActionResult result = controller.Put(newReservation);

            // Assert
            Assert.IsType<System.Web.Http.Results.ConflictResult>(result);
        }
    }
}
