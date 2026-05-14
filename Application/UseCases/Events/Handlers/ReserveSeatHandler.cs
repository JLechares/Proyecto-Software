using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Commands;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

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
            var seat = await _eventRepository.GetSeatByIdAsync(command.SeatId);
            var now = DateTime.UtcNow;

            if (seat == null)
            {
                throw new Exception("Asiento no encontrado");
            }

            if (seat.Status != "Available")
            {
                await _eventRepository.AddAuditLogAsync(new AUDIT_LOG
                {
                    Id = Guid.NewGuid(),
                    UserId = command.UserId,
                    Action = "RESERVE_FAILED",
                    EntityType = "Seat",
                    EntityId = command.SeatId.ToString(),
                    Details = "Seat is not available",
                    CreatedAt = now
                });

                await _eventRepository.SaveChangesAsync();

                throw new InvalidOperationException("Asiento no disponible");
            }
                
            // cambiar el "Sold" x "Reserved"
            seat.Status = "Reserved";
            seat.Version += 1;
            
            
            var reservation = new RESERVATION
            {
                Id = Guid.NewGuid(),
                SeatId = seat.Id,
                UserId = command.UserId,
                User = null,
                Seat = seat,
                Status = "Pending",
                ReservedAt = now,
                ExpiresAt = now.AddMinutes(5)
            };

            var auditLog = new AUDIT_LOG
            {
                Id = Guid.NewGuid(),
                UserId = reservation.UserId,
                Action = "RESERVE_SUCCESS",
                EntityType = "Seat",
                EntityId = reservation.SeatId.ToString(),
                Details = "Seat reserved successfully",
                CreatedAt = now
            };

            _eventRepository.UpdateSeat(seat);
            await _eventRepository.AddReservationAsync(reservation);    
            await _eventRepository.AddAuditLogAsync(auditLog);
            try
            {
                await _eventRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                await _eventRepository.AddAuditLogAsync(new AUDIT_LOG
                {
                    Id = Guid.NewGuid(),
                    UserId = command.UserId,
                    Action = "RESERVE_CONFLICT",
                    EntityType = "Seat",
                    EntityId = seat.Id.ToString(),
                    Details = "Concurrency conflict while reserving seat",
                    CreatedAt = now
                });
                await _eventRepository.SaveChangesAsync();

                throw new InvalidOperationException("Asiento ya no disponible");
            }
            

            return new ReserveSeatResponse
            {
                ReservationID = reservation.Id,
                UserId = reservation.UserId,
                SeatId = reservation.SeatId,
                Status = reservation.Status,
                ExpiresAt = reservation.ExpiresAt
            };
        }
    }
}
