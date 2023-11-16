using LabWebAPI.Data;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LabWebAPI.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataContext _context;

        public ReservationRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReservation(Reservation reservation)
        {
            _context.Add(reservation);
            return Save();
        }

        public bool DeleteReservation(Reservation reservation)
        {
            _context.Remove(reservation);
            return Save();
        }

        public Reservation GetReservationById(int id)
        {
            return _context.Reservations.Where(r => r.Id == id)
                .Include(r => r.Item)
                .Include(r => r.LabUser)
                .FirstOrDefault();
        }

        public ICollection<Reservation> GetReservations()
        {
            return _context.Reservations
                .Include(r => r.Item)
                .Include(r => r.LabUser)
                .ToList();
        }

        public bool IsReservationAvailable(int itemId, DateTime startTime, DateTime endTime)
        {
            return !_context.Reservations
                .Any(r => r.ItemId == itemId && r.StartTime < endTime && r.EndTime > startTime);
        }

        public bool ReservationExists(int id)
        {
            return _context.Reservations.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        // public bool UpdateReservation(Reservation reservation)
        // {
        //     _context.Update(reservation);
        //     return Save();
        // }
    }
}