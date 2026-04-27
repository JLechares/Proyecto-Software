using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
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
            //REMOVE THE PROBLEM OF THE PLURAL BEING ADDED TO TABLES
            modelBuilder.Entity<USER>().ToTable("USER");
            modelBuilder.Entity<RESERVATION>().ToTable("RESERVATION");
            modelBuilder.Entity<SEAT>().ToTable("SEAT");
            modelBuilder.Entity<SECTOR>().ToTable("SECTOR");
            modelBuilder.Entity<EVENT>().ToTable("EVENT");
            modelBuilder.Entity<AUDIT_LOG>().ToTable("AUDIT_LOG");
        }
    }
}
