using EHRMvcDemo.Models;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace EHRMvcDemo.Data
{
    public class EHRDbContext : DbContext
    {
        public EHRDbContext(DbContextOptions<EHRDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<LabResult> LabResults { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patients", "Healthcare");
            modelBuilder.Entity<Doctor>().ToTable("Doctors", "Healthcare");
            modelBuilder.Entity<Appointment>().ToTable("Appointments", "Healthcare");
            modelBuilder.Entity<LabResult>().ToTable("LabResults", "Healthcare");
            modelBuilder.Entity<AuditLog>().ToTable("AuditLog", "Healthcare");

            base.OnModelCreating(modelBuilder);
        }
    }
}
