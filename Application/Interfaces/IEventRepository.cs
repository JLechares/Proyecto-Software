using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<EVENT>> GetAllEventsAsync();
        Task<IEnumerable<SECTOR>> GetSectorsByEventAsync(int eventId);
        Task<IEnumerable<SEAT>> GetSeatsBySectorIdAsync(int sectorId);
        Task<SEAT?> GetSeatByIdAsync(int seatId);

        void UpdateSeat(SEAT seat);
        Task AddReservationAsync(RESERVATION reservation);
        Task AddAuditLogAsync(AUDIT_LOG log);
        Task <bool> SaveChangesAsync();
    }
}
