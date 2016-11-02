﻿using LibraryGradProject.Models;
using LibraryGradProject.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace LibraryGradProject.Repos
{
    public class ReservationRepository : IRepository<Reservation>
    {
        private ILibraryContext _dbContext;

        public ReservationRepository(ILibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Reservation entity)
        {
            var overlappingQuery = from r in _dbContext.Reservations
                                   where r.EndDate > entity.BeginDate && r.BeginDate < entity.EndDate
                                   select r;
            if (overlappingQuery.Count() > 0)
            {
                throw new System.Exception("Reservation timeslot is already reserved.");
            }
            _dbContext.Reservations.Add(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Reservation> GetAll()
        {
            var query = from r in _dbContext.Reservations
                        orderby r.Id
                        select r;
            return query.AsEnumerable();
        }

        public Reservation Get(int id)
        {
            var query = from r in _dbContext.Reservations
                        where r.Id == id
                        select r;
            return query.AsEnumerable().SingleOrDefault();
        }

        public void Remove(int id)
        {
            Reservation reservationToRemove = Get(id);
            _dbContext.Reservations.Remove(reservationToRemove);
            _dbContext.SaveChanges();
        }
    }
}