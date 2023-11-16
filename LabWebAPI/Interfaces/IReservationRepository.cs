using LabWebAPI.Model;

namespace LabWebAPI.Interfaces
{
    public interface IReservationRepository
    {
        //* Read Methods
        ICollection<Reservation> GetReservations();
        Reservation GetReservationById(int id);

        //* Crete Method
        bool CreateReservation(Reservation reservation);

        //* Update Method
        // bool UpdateReservation(Reservation reservation);

        //* Delete Method
        bool DeleteReservation(Reservation reservation);

        //* Utils Methods
        bool ReservationExists(int id);
        bool IsReservationAvailable(int itemId, DateTime startTime, DateTime endTime);
        bool Save();
    }
}