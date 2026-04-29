using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        public Task AddAuditLogAsync(AUDIT_LOG log)
        {
            throw new NotImplementedException();
        }

        public Task AddReservationAsync(RESERVATION reservation)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EVENT>> GetAllEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SEAT?> GetSeatByIdAsync(int seatId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SEAT>> GetSeatsBySectorIdAsync(int sectorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SECTOR>> GetSectorsByEventAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateSeat(SEAT seat)
        {
            throw new NotImplementedException();
        }
    }
}
