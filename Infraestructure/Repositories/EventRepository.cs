using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repositories
{
    public class EventRepository : IEventRepository
    {   
        private readonly AppDbContext _appDbContext;

        public EventRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAuditLogAsync(AUDIT_LOG log)
        {
            throw new NotImplementedException();
        }

        public async Task AddReservationAsync(RESERVATION reservation)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EVENT>> GetAllEventsAsync()
        {
            return await _appDbContext.EVENTS.ToListAsync();
        }

        public async Task<SEAT?> GetSeatByIdAsync(int seatId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SEAT>> GetSeatsBySectorIdAsync(int sectorId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SECTOR>> GetSectorsByEventAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateSeat(SEAT seat)
        {
            throw new NotImplementedException();
        }
    }
}
