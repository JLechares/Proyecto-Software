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
        static string databaseName = "EventReservationDB.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={databaseName}", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            base.OnConfiguring(optionsBuilder);
        }

        // DbSet properties for each entity
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
            });
            modelBuilder.Entity<SECTOR>(entity =>
            {
                entity.ToTable("SECTOR");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // A Sector can have many Seats
                entity.HasMany(s => s.Seats)
                      .WithOne(seat => seat.Sector)
                      .HasForeignKey(seat => seat.SectorId);

            });
            modelBuilder.Entity<SEAT>(entity =>
            {
                entity.ToTable("SEAT");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
                entity.Property(s => s.Version);
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
