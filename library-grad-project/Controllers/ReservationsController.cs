using LibraryGradProject.Models;
using LibraryGradProject.Repos;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LibraryGradProject.Controllers
{
    public class ReservationsController : ApiController
    {
        private IRepository<Reservation> _reservationRepo;

        public ReservationsController(IRepository<Reservation> reservationRepository)
        {
            _reservationRepo = reservationRepository;
        }

        // GET api/books
        public IEnumerable<Reservation> Get()
        {
            return _reservationRepo.GetAll();
        }

        // GET api/values/{int}
        public Reservation Get(int id)
        {
            return _reservationRepo.Get(id);
        }

        // POST api/values
        public IHttpActionResult Post(Reservation newReservation)
        {
            try
            {
                _reservationRepo.Add(newReservation);
            }
            catch(Exception e)
            {
                return Conflict();
            }
            return Ok(newReservation.Id);
        }

        // DELETE api/values/{int}
        public void Delete(int id)
        {
            _reservationRepo.Remove(id);
       } 

        // PUT api/values/{int}
        public IHttpActionResult Put(Reservation newReservation)
        {
            Reservation oldReservation = _reservationRepo.Get(newReservation.Id);
            try
            {
                if (oldReservation != null)
                {
                    oldReservation.BookId = newReservation.BookId;
                    oldReservation.BeginDate = newReservation.BeginDate;
                    oldReservation.EndDate = newReservation.EndDate;
                }
                else
                {
                    _reservationRepo.Add(newReservation);
                }
            }
            catch(Exception e)
            {
                return Conflict();
            }
            return Ok(newReservation.Id);
        }
    }
}