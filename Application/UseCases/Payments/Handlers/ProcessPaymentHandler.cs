using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Commands;
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
            // 1. Iniciamos la transacción para asegurar atomicidad (todo o nada)
            /*using (var transaction = await _eventRepository.BeginTransactionAsync())
            {
                try
                {
                    var reservation = await _eventRepository.GetReservationAsync(command.ReservationId);

                    if (reservation == null) throw new Exception("Reserva no encontrada");

                    if (reservation.Status != "Pending")
                        throw new InvalidOperationException("La reserva no está pendiente de pago");

                    var seat = await _eventRepository.GetSeatByIdAsync(reservation.SeatId);
                    var now = DateTime.UtcNow;

                    // Lógica de Expiración
                    if (reservation.ExpiresAt < now)
                    {
                        reservation.Status = "Expired";
                        if (seat != null)
                        {
                            seat.Status = "Available";
                            seat.Version += 1;
                            _eventRepository.UpdateSeat(seat);
                        }

                        await _eventRepository.AddAuditLogAsync(new AUDIT_LOG
                        {
                            Id = Guid.NewGuid(),
                            UserId = reservation.UserId,
                            Action = "EXPIRED",
                            EntityType = "Reservation",
                            EntityId = reservation.Id.ToString(),
                            Details = "Payment failed because reservation expired",
                            CreatedAt = now
                        });
                        await _eventRepository.SaveChangesAsync();

                        // Confirmamos la transacción incluso para la expiración
                        await transaction.CommitAsync();
                        throw new InvalidOperationException("La reserva está vencida");
                    }

                    // Lógica de Pago Exitoso
                    reservation.Status = "Paid";
                    if (seat == null) throw new Exception("El objeto Seat no puede ser nulo al actualizar");
                    seat.Status = "Sold";
                    seat.Version += 1;

                    _eventRepository.UpdateReservation(reservation);
                    
                    _eventRepository.UpdateSeat(seat);

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

                    // 2. Guardamos todos los cambios juntos
                    await _eventRepository.SaveChangesAsync();

                    // 3. Confirmamos la transacción en la base de datos
                    await transaction.CommitAsync();

                    return new PaymentResponse
                    {
                        ReservationId = reservation.Id,
                        ReservationStatus = reservation.Status,
                        SeatId = reservation.SeatId,
                        SeatStatus = seat.Status,
                        PaidAt = now
                    };
                }
                catch (Exception)
                {
                    // 4. Si algo falla (ej. error de red, de BD o lógica), deshace todo
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            */
            
            var reservation = await _eventRepository.GetReservationAsync(command.ReservationId);
            
            if(reservation == null)
            {
                throw new Exception("Reserva no encontrada");
            }
            if(reservation.Status != "Pending")
            {
                throw new InvalidOperationException("La reserva no está pendiente de pago");
            }
            var seat = await _eventRepository.GetSeatByIdAsync(reservation.SeatId);
            var now = DateTime.UtcNow;

            if (reservation.ExpiresAt < now)
            {
                reservation.Status = "Expired";

                if (seat != null)
                {
                    seat.Status = "Available";
                    seat.Version += 1;
                    _eventRepository.UpdateSeat(seat);
                }

                await _eventRepository.AddAuditLogAsync(new AUDIT_LOG
                {
                    Id = Guid.NewGuid(),
                    UserId = reservation.UserId,
                    Action = "EXPIRED",
                    EntityType = "Reservation",
                    EntityId = reservation.Id.ToString(),
                    Details = "Payment failed because reservation expired",
                    CreatedAt = now
                });

                await _eventRepository.SaveChangesAsync();

                throw new InvalidOperationException("La reserva está vencida");
            }

            reservation.Status = "Paid";
            seat.Status = "Sold";
            seat.Version += 1;
            
            _eventRepository.UpdateReservation(reservation);
            _eventRepository.UpdateSeat(seat);

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
