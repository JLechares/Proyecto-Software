using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Commands;
using Domain.Entities;

namespace Application.UseCases.Events.Handlers
{
    public class ReserveSeatHandler : IReserveSeatCommandHandler
    {
        private readonly IEventRepository _eventRepository;
        public ReserveSeatHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<ReserveSeatResponse> HandleAsync(ReserveSeatCommand command)
        {
            await using var transaction = await _eventRepository.BeginTransactionAsync();
            var seat = await _eventRepository.GetSeatByIdAsync(command.SeatId);
            

            string actionStatus = "RESERVE_ATTEMPT";
            try
            {
                if (seat == null)
                {
                    actionStatus = "NOT_FOUND";
                    throw new KeyNotFoundException("Asiento no encontrado");
                }
                    
                var sector = await _eventRepository.GetSectorByIdAsync(seat!.SectorId);
                if (seat != null && seat.Status != "Available")
                {
                    actionStatus = "CONFLICT_OCCUPIED"; 
                    throw new InvalidOperationException("El asiento no está disponible");
                }

                seat!.Status = "Reserved";
                seat.Version++;

                _eventRepository.UpdateSeat(seat);

                var now = DateTime.UtcNow;

                var reservation = new RESERVATION
                {
                    Id = Guid.NewGuid(),
                    UserId = command.UserId,
                    User = null,
                    Seat = seat,
                    Status = "Pending",
                    ReservedAt = now,
                    ExpiresAt = now.AddMinutes(5)
                };

                await _eventRepository.AddReservationAsync(reservation);
                
                await _eventRepository.SaveChangesAsync();

                await transaction.CommitAsync();
                actionStatus = "SUCCESS";

                return new ReserveSeatResponse
                {
                    ReservationId=reservation.Id,
                    UserId = command.UserId,
                    SeatId = command.SeatId,
                    ExpiresAt = reservation.ExpiresAt
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

            finally
            {
                var metadata = new
                {
                    EventId = seat != null ? (await _eventRepository.GetSectorByIdAsync(seat.SectorId))?.EventId : null,
                    SectorId = seat?.SectorId,
                    SeatId = command.SeatId,
                    RequestTimestamp = DateTime.UtcNow
                };
                await _eventRepository.AddAuditLogAsync(new AUDIT_LOG
                {
                    Id = Guid.NewGuid(),
                    UserId = command.UserId,
                    Action = actionStatus,
                    EntityType = "Seat",
                    EntityId = command.SeatId.ToString(),
                    Details = System.Text.Json.JsonSerializer.Serialize(metadata), 
                    CreatedAt = DateTime.UtcNow
                });
                await _eventRepository.SaveChangesAsync();
            }

        }
    }
}
