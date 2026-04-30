using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Commands;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
            if (seat == null) throw new Exception("Asiento no encontrado");
            seat.Status = "Sold";
            _eventRepository.UpdateSeat(seat);
            await _eventRepository.SaveChangesAsync();

            var reservation = new RESERVATION
            {
                Id = Guid.NewGuid(),
                SeatId = seat.Id,
                UserId = command.UserId,
                User = null,
                Seat = seat,
                Status = seat.Status,
                ReservedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5)
            };

            await _eventRepository.AddReservationAsync(reservation);
            var auditLog = new AUDIT_LOG
            {
                Id = Guid.NewGuid(),
                UserId = reservation.UserId,
                Action = "RESERVE_SUCCESS",
                EntityType = "Seat",
                EntityId = seat.Id.ToString(),
                Details = "Seat reserved successfully",
                CreatedAt = DateTime.UtcNow
            };

            await _eventRepository.AddAuditLogAsync(auditLog);
            return new ReserveSeatResponse
            {
                UserId = reservation.UserId,
                SeatId = reservation.SeatId
            };
        }
    }
}
