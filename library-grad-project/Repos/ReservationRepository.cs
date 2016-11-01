using LibraryGradProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryGradProject.Repos
{
    public class ReservationRepository : IRepository<Reservation>
    {
        private List<Reservation> _reservations = new List<Reservation>();

        public void Add(Reservation entity)
        {
            IEnumerable<Reservation> overlappingReservations = _reservations
                .Where(reservation => reservation.EndDate > entity.BeginDate)
                .Where(reservation => reservation.BeginDate < entity.EndDate);
            if (overlappingReservations.Count() > 0)
            {
                throw new System.Exception("Reservation timeslot is already reserved.");
            }
            entity.Id = _reservations.Count;
            _reservations.Add(entity);
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _reservations;
        }

        public Reservation Get(int id)
        {
            return _reservations.Where(reservation => reservation.Id == id).SingleOrDefault();
        }

        public void Remove(int id)
        {
            Reservation reservationToRemove = Get(id);
            _reservations.Remove(reservationToRemove);
        }
    }
}