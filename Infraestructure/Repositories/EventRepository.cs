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
            await _appDbContext.AUDIT_LOGS.AddAsync(log);
        }

        public async Task AddReservationAsync(RESERVATION reservation)
        {
            await _appDbContext.RESERVATIONS.AddAsync(reservation);
        }

        public async Task<IEnumerable<EVENT>> GetAllEventsAsync()
        {
            return await _appDbContext.EVENTS.ToListAsync();
        }

        public async Task<SEAT?> GetSeatByIdAsync(Guid seatId)
        {
            return await _appDbContext.SEATS.FirstOrDefaultAsync(s => s.Id == seatId);
        }

        public async Task<IEnumerable<SEAT>> GetSeatsByEventAndSectorAsync(int eventId, int sectorId)
        {
            return await _appDbContext.SEATS
                .Where(s => s.SectorId == sectorId && _appDbContext.SECTORS
                    .Any(sec => sec.Id == sectorId && sec.EventId == eventId))
                .ToListAsync();
        }

        public async Task<IEnumerable<SECTOR>> GetSectorsByEventAsync(int eventId)
        {
            return await _appDbContext.SECTORS.Where(s => s.EventId == eventId).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public void UpdateSeat(SEAT seat)
        {
            _appDbContext.SEATS.Update(seat);
        }
    }
}
