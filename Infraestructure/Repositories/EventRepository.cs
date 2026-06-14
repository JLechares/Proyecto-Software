using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class EventRepository : IEventRepository
    {   
        private readonly AppDbContext _appDbContext;

        public EventRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IAppTransaction> BeginTransactionAsync()
        {
            var transaction = await _appDbContext.Database.BeginTransactionAsync();
            return new EfAppTransaction(transaction);
        }
        public async Task AddAuditLogAsync(AUDIT_LOG log)
        {
            //_appDbContext.ChangeTracker.Clear();
            await _appDbContext.AUDIT_LOGS.AddAsync(log);
        }

        public async Task AddReservationAsync(RESERVATION reservation)
        {
            await _appDbContext.RESERVATIONS.AddAsync(reservation);
        }

        public async Task<IEnumerable<EVENT>> GetAllEventsAsync(int pageNumber, int pageSize)
        {
            //Se usa sistema de paginación, para cuando sean muchos eventos, sea escalable
            return await _appDbContext.EVENTS
                        .OrderBy(e => e.EventDate)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }

        public async Task<SEAT?> GetSeatByIdAsync(Guid seatId)
        {
            return await _appDbContext.SEATS.FirstOrDefaultAsync(s => s.Id == seatId);
        }
        public async Task<SECTOR?> GetSectorByIdAsync(int sectorId)
        {
            return await _appDbContext.SECTORS.FirstOrDefaultAsync(s => s.Id == sectorId);
        }
        public async Task<RESERVATION?> GetReservationAsync(Guid reservationId)
        {
            return await _appDbContext.RESERVATIONS.FirstOrDefaultAsync(r => r.Id == reservationId);
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

        public async Task<IEnumerable<RESERVATION>> GetExpiredPendingReservationsAsync(DateTime now)
        {
            return await _appDbContext.RESERVATIONS
                .Include(r => r.Seat)
                .Where(r => r.Status == "Pending" && r.ExpiresAt <= now)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public void UpdateSeat(SEAT seat)
        {
            _appDbContext.SEATS.Update(seat);
            
        }

        public void UpdateReservation(RESERVATION reservation)
        {
            _appDbContext.RESERVATIONS.Update(reservation);
            
        }
    }
}
