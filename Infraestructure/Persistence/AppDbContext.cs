using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infraestructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        

        public DbSet<Domain.Entities.USER> USERS { get; set; }
        public DbSet<Domain.Entities.RESERVATION> RESERVATIONS { get; set; }
        public DbSet<Domain.Entities.SEAT> SEATS { get; set; }
        public DbSet<Domain.Entities.SECTOR> SECTORS { get; set; }
        public DbSet<Domain.Entities.EVENT> EVENTS { get; set; }
        public DbSet<Domain.Entities.AUDIT_LOG> AUDIT_LOGS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EVENT>(entity =>
            {
                entity.ToTable("EVENT");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasMany(e => e.Sectors)
                      .WithOne(s => s.Event)
                      .HasForeignKey(s => s.EventId);
                entity.HasData(new EVENT
                {
                    Id = 1,
                    Name = "Maria Becerra Concierto",
                    EventDate = new DateTime(2026, 08, 15),
                    Venue = "River Plate Stadium",
                    Status = "Active"
                });
            });
            modelBuilder.Entity<SECTOR>(entity =>
            {
                entity.ToTable("SECTOR");
                entity.HasKey(e => e.Id);
                entity.Property(s => s.Price).HasPrecision(18, 2);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // A Sector can have many Seats
                entity.HasMany(s => s.Seats)
                      .WithOne(seat => seat.Sector)
                      .HasForeignKey(seat => seat.SectorId);
                entity.HasData(
                    new SECTOR { Id = 1, EventId = 1, Name = "VIP", Price = 25000.00m, Capacity = 50, Event=null!},
                    new SECTOR { Id = 2, EventId = 1, Name = "Preferencial", Price = 12500.00m, Capacity = 50, Event=null! }
                );
            });
            modelBuilder.Entity<SEAT>(entity =>
            {
                entity.ToTable("SEAT");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
                entity.Property(s => s.Version).IsConcurrencyToken();
                var seats = new List<SEAT>();
                for (int i = 1; i <= 100; i++)
                {
                    var guidId = Guid.Parse($"00000000-0000-0000-0000-{i:D12}");
                    seats.Add(new SEAT
                    {
                        Id = guidId,
                        SectorId = i <= 50 ? 1 : 2,
                        RowIdentifier = $"Fila {((i - 1) % 50) / 10 + 1}",
                        SeatNumber = i,
                        Status = "Available",
                        Sector = null!
                    });
                }
                entity.HasData(seats);
            });
            modelBuilder.Entity<RESERVATION>(entity =>
            {
                entity.ToTable("RESERVATION");
                entity.HasKey(e => e.Id); 
                entity.Property(s=>s.Id)
                      .ValueGeneratedOnAdd();

                entity.HasOne(r=>r.Seat)
                      .WithMany(s => s.Reservations)
                      .HasForeignKey(r => r.SeatId);

                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reservations)
                      .HasForeignKey(r => r.UserId);
            });

            modelBuilder.Entity<USER>(entity =>
            {
                entity.ToTable("USER");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
               
                entity.HasMany(u => u.Reservations)
                      .WithOne(r => r.User)
                      .HasForeignKey(r => r.UserId);
                entity.HasData(
                    new USER
                    {
                        Id = 1,
                        Name = "Juan",
                        Email = "Juanlechares@gmail.com",
                        PasswordHash = "1232132"
                    },
                    new USER
                    {
                        Id = 2,
                        Name = "Juan Cruz",
                        Email = "Juanmerino@gmail.com",
                        PasswordHash = "5555555"
                    }
                );

            });
            modelBuilder.Entity<AUDIT_LOG>(entity =>
            {
                entity.ToTable("AUDIT_LOG");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); 

               
                entity.HasOne(a => a.User)
                      .WithMany(u => u.AuditLogs)
                      .HasForeignKey(a => a.UserId)
                      .IsRequired(false);

            });
        }
    }
}
