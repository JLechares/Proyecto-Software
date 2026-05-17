using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Commands;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Transactions;
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
            using var transaction = await _eventRepository.BeginTransactionAsync();
            var seat = await _eventRepository.GetSeatByIdAsync(command.SeatId);
            
            string actionStatus = "SUCCESS";
            try
            {
                if (seat == null) throw new Exception("Asiento no encontrado");
                var sector = await _eventRepository.GetSectorByIdAsync(seat!.SectorId);
                if (seat != null && seat.Status != "Available")
                {
                    actionStatus = "CONFLICT_OCCUPIED"; // Cambiamos el estado para el log
                    throw new InvalidOperationException("El asiento no está disponible");
                }

                seat!.Status = "Reserved";
                seat.Version++;

                _eventRepository.UpdateSeat(seat);
                
                var reservation = new RESERVATION
                {
                    Id = Guid.NewGuid(),
                    UserId = command.UserId,
                    User = null,
                    Seat = seat,
                    Status = "Pending",
                    ReservedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddMinutes(1)
                };
                await _eventRepository.AddReservationAsync(reservation);
                
                await _eventRepository.SaveChangesAsync();

                await transaction.CommitAsync();
                return new ReserveSeatResponse
                {
                    ReservationId=reservation.Id,
                    UserId = command.UserId,
                    SeatId = command.SeatId
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                actionStatus = "RESERVE_ATTEMPT";
                throw;
            }
            catch (Exception) {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                var metadata = new
                {
                    EventId = (await _eventRepository.GetSectorByIdAsync(seat!.SectorId))?.EventId,
                    SectorId = seat?.SectorId,
                    SeatId = command.SeatId,
                    RequestTimestamp = DateTime.Now
                };
                await _eventRepository.AddAuditLogAsync(new AUDIT_LOG
                {
                    Id = Guid.NewGuid(),
                    UserId = command.UserId,
                    Action = actionStatus,
                    EntityType = "Seat",
                    EntityId = command.SeatId.ToString(),
                    Details = System.Text.Json.JsonSerializer.Serialize(metadata), 
                    CreatedAt = DateTime.Now
                });
                await _eventRepository.SaveChangesAsync();
            }

        }
    }
}
