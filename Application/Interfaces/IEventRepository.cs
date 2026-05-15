using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Interfaces
{
    public interface IEventRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<IEnumerable<EVENT>> GetAllEventsAsync(int pageNumber, int pageSize);
        Task<IEnumerable<SECTOR>> GetSectorsByEventAsync(int eventId);
        Task<IEnumerable<SEAT>> GetSeatsByEventAndSectorAsync(int eventId, int sectorId);
        Task<SEAT?> GetSeatByIdAsync(Guid seatId);
        Task<RESERVATION?> GetReservationAsync(Guid reservationId);
        void UpdateSeat(SEAT seat);
        void UpdateReservation(RESERVATION reservation);
        Task AddReservationAsync(RESERVATION reservation);
        Task AddAuditLogAsync(AUDIT_LOG log);
        Task <bool> SaveChangesAsync();
    }
}
