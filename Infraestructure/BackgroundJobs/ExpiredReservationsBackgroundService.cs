using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infraestructure.BackgroundJobs
{
    public class ExpiredReservationsBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ExpiredReservationsBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                var eventRepository = scope.ServiceProvider.GetRequiredService<IEventRepository>();

                var now = DateTime.UtcNow;
                var expiredReservations = await eventRepository.GetExpiredPendingReservationsAsync(now);

                foreach (var reservation in expiredReservations)
                {
                    reservation.Status = "Expired";

                    if (reservation.Seat != null)
                    {
                        reservation.Seat.Status = "Available";
                        reservation.Seat.Version += 1;
                        eventRepository.UpdateSeat(reservation.Seat);
                    }

                    eventRepository.UpdateReservation(reservation);

                    await eventRepository.AddAuditLogAsync(new AUDIT_LOG
                    {
                        Id = Guid.NewGuid(),
                        UserId = reservation.UserId,
                        Action = "RESERVATION_EXPIRED",
                        EntityType = "Reservation",
                        EntityId = reservation.Id.ToString(),
                        Details = "Reservation expired and seat was released automatically",
                        CreatedAt = now
                    });
                }

                await eventRepository.SaveChangesAsync();

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}