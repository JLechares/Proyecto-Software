using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Payments.Commands;
using Domain.Entities;

namespace Application.UseCases.Payments.Handlers
{
    public class ProcessPaymentHandler : IProcessPaymentHandler
    {
        private readonly IEventRepository _eventRepository;
        public ProcessPaymentHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<PaymentResponse> HandleAsync(ProcessPaymentCommand command)
        {
            var reservation = await _eventRepository.GetReservationAsync(command.ReservationId);
            
            if(reservation == null)
            {
                throw new Exception("Reserva no encontrada");
            }

            if(reservation.Status != "Pending")
            {
                throw new InvalidOperationException("La reserva no está pendiente de pago");
            }

            var now = DateTime.UtcNow;

            if (reservation.ExpiresAt <= now)
            {
                reservation.Status = "Expired";

                var expiredSeat = await _eventRepository.GetSeatByIdAsync(reservation.SeatId);

                if (expiredSeat != null)
                {
                    expiredSeat.Status = "Available";
                    expiredSeat.Version += 1;
                    _eventRepository.UpdateSeat(expiredSeat);
                }

                _eventRepository.UpdateReservation(reservation);
                await _eventRepository.SaveChangesAsync();

                throw new InvalidOperationException("La reserva ya expiró y no puede ser pagada");
            }

            var seat = await _eventRepository.GetSeatByIdAsync(reservation.SeatId);
            var sector = await _eventRepository.GetSectorByIdAsync(seat!.SectorId);
            
            reservation.Status = "Paid";

            seat!.Status = "Sold";
            seat.Version += 1;
            var metadata = new
            {
                EventId = sector!.EventId,
                SectorId = seat.SectorId,
                SeatId = seat.Id,
                RequestTimestamp = DateTime.UtcNow
            };

            _eventRepository.UpdateReservation(reservation);
            _eventRepository.UpdateSeat(seat);
            
            await _eventRepository.AddAuditLogAsync(new AUDIT_LOG
            {
                Id = Guid.NewGuid(),
                UserId = reservation.UserId,
                Action = "PAYMENT_SUCCESS",
                EntityType = "Reservation",
                EntityId = reservation.Id.ToString(),
                Details = System.Text.Json.JsonSerializer.Serialize(metadata),
                CreatedAt = now
            });

            await _eventRepository.SaveChangesAsync();
            return new PaymentResponse
            {
                ReservationId = reservation.Id,
                ReservationStatus = reservation!.Status,       
                SeatId = reservation.SeatId,
                SeatStatus = seat?.Status ?? "Sold",            
                PaidAt = now
            };
            
        }
    }
}
