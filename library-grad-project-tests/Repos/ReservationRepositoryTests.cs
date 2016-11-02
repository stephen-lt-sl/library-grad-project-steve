using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using LibraryGradProjectTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace LibraryGradProjectTests.Repos
{
    public class ReservationRepositoryTests
    {
        [Fact]
        public void New_Reservation_Repository_Is_Empty()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            ReservationRepository repo = new ReservationRepository(mockDb);

            // Act
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Assert
            Assert.Empty(reservations);
        }

        [Fact]
        public void Add_Inserts_New_Reservation()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            ReservationRepository repo = new ReservationRepository(mockDb);
            Reservation newReservation = new Reservation() { BeginDate = new DateTime() };

            // Act
            repo.Add(newReservation);
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Assert
            Assert.Equal(new Reservation[] { newReservation }, reservations.ToArray());
        }

        [Fact]
        public void Add_Sets_New_Id()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            ReservationRepository repo = new ReservationRepository(mockDb);
            Reservation newReservation = new Reservation() { BookId = 0 };

            // Act
            repo.Add(newReservation);
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Assert
            Assert.Equal(1, reservations.First().Id);
        }

        [Fact]
        public void Add_Not_Throws_If_Reservation_Slot_Not_In_Use()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            ReservationRepository repo = new ReservationRepository(mockDb);
            Reservation newReservation1 = new Reservation() { BeginDate = new DateTime(100), EndDate = new DateTime(500) };
            Reservation newReservation2 = new Reservation() { BeginDate = new DateTime(600), EndDate = new DateTime(700) };

            // Act
            repo.Add(newReservation1);

            // Assert
            repo.Add(newReservation2);
        }

        [Fact]
        public void Add_Throws_If_Reservation_Slot_In_Use()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            ReservationRepository repo = new ReservationRepository(mockDb);
            Reservation newReservation1 = new Reservation() { BeginDate = new DateTime(100), EndDate = new DateTime(500) };
            Reservation newReservation2 = new Reservation() { BeginDate = new DateTime(400), EndDate = new DateTime(700) };

            // Act
            repo.Add(newReservation1);

            // Assert
            Assert.Throws<System.Exception>(() => repo.Add(newReservation2));
        }

        [Fact]
        public void Get_Returns_Specific_Reservation()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            ReservationRepository repo = new ReservationRepository(mockDb);
            Reservation newReservation1 = new Reservation() { BookId = 0 };
            Reservation newReservation2 = new Reservation() { BookId = 1 };
            repo.Add(newReservation1);
            repo.Add(newReservation2);

            // Act
            Reservation reservation = repo.Get(2);

            // Assert
            Assert.Equal(newReservation2, reservation);
        }

        [Fact]
        public void Get_All_Returns_All_Reservations()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            ReservationRepository repo = new ReservationRepository(mockDb);
            Reservation newReservation1 = new Reservation() { BookId = 0 };
            Reservation newReservation2 = new Reservation() { BookId = 1 };
            repo.Add(newReservation1);
            repo.Add(newReservation2);

            // Act
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Assert
            Assert.Equal(new Reservation[] { newReservation1, newReservation2 }, reservations.ToArray());
        }

        [Fact]
        public void Delete_Removes_Correct_Reservation()
        {
            // Arrange
            MockLibraryContext mockDb = new MockLibraryContext();
            ReservationRepository repo = new ReservationRepository(mockDb);
            Reservation newReservation1 = new Reservation() { BookId = 0 };
            Reservation newReservation2 = new Reservation() { BookId = 1 };
            Reservation newReservation3 = new Reservation() { BookId = 2 };
            repo.Add(newReservation1);
            repo.Add(newReservation2);
            repo.Add(newReservation3);

            // Act
            repo.Remove(2);
            IEnumerable<Reservation> reservations = repo.GetAll();

            // Assert
            Assert.Equal(new Reservation[] { newReservation1, newReservation3 }, reservations.ToArray());
        }
    }
}
