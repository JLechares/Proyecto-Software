using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Payments.Commands;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
            var reservation = await _eventRepository.GetReservationByIdAsync(command.ReservationId);

            if (reservation == null)
            {
                throw new Exception("Reserva no encontrada");
            }

            if (reservation.Status != "Pending")
            {
                throw new InvalidOperationException("La reserva no está pendiente de pago");
            }

            var now = DateTime.UtcNow;

            if (reservation.ExpiresAt < now)
            {
                reservation.Status = "Expired";

                if (reservation.Seat != null)
                {
                    reservation.Seat.Status = "Available";
                    reservation.Seat.Version += 1;
                    _eventRepository.UpdateSeat(reservation.Seat);
                }

                await _eventRepository.AddAuditLogAsync(new AUDIT_LOG
                {
                    Id = Guid.NewGuid(),
                    UserId = reservation.UserId,
                    Action = "PAYMENT_FAILED_EXPIRED_RESERVATION",
                    EntityType = "Reservation",
                    EntityId = reservation.Id.ToString(),
                    Details = "Payment failed because reservation expired",
                    CreatedAt = now
                });

                await _eventRepository.SaveChangesAsync();

                throw new InvalidOperationException("La reserva está vencida");
            }

            reservation.Status = "Paid";

            if (reservation.Seat == null)
            {
                throw new Exception("Asiento asociado no encontrado");
            }

            reservation.Seat.Status = "Sold";
            reservation.Seat.Version += 1;

            _eventRepository.UpdateReservation(reservation);
            _eventRepository.UpdateSeat(reservation.Seat);

            await _eventRepository.AddAuditLogAsync(new AUDIT_LOG
            {
                Id = Guid.NewGuid(),
                UserId = reservation.UserId,
                Action = "PAYMENT_SUCCESS",
                EntityType = "Reservation",
                EntityId = reservation.Id.ToString(),
                Details = "Payment processed successfully",
                CreatedAt = now
            });

            await _eventRepository.SaveChangesAsync();

            return new PaymentResponse
            {
                ReservationId = reservation.Id,
                ReservationStatus = reservation.Status,
                SeatId = reservation.SeatId,
                SeatStatus = reservation.Seat.Status,
                PaidAt = now
            };
        }
    }
}
