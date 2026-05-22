using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Interfaces
{
    public interface IEventRepository
    {
        Task<IApplicationTransaction> BeginTransactionAsync();
        Task<IEnumerable<EVENT>> GetAllEventsAsync(int pageNumber, int pageSize);
        Task<IEnumerable<SECTOR>> GetSectorsByEventAsync(int eventId);
        Task<IEnumerable<SEAT>> GetSeatsByEventAndSectorAsync(int eventId, int sectorId);
        Task<SEAT?> GetSeatByIdAsync(Guid seatId);
        Task<SECTOR?> GetSectorByIdAsync(int sectorId);
        Task<RESERVATION?> GetReservationAsync(Guid reservationId);
        void UpdateSeat(SEAT seat);
        void UpdateReservation(RESERVATION reservation);
        Task AddReservationAsync(RESERVATION reservation);
        Task AddAuditLogAsync(AUDIT_LOG log);
        Task<IEnumerable<RESERVATION>> GetExpiredPendingReservationsAsync(DateTime now);
        Task <bool> SaveChangesAsync();
    }
}
