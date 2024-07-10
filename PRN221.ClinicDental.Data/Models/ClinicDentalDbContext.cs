using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Data.Models;

public partial class ClinicDentalDbContext : DbContext
{
    public ClinicDentalDbContext()
    {
    }

    public ClinicDentalDbContext(DbContextOptions<ClinicDentalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<ClinicService> ClinicServices { get; set; }

    public virtual DbSet<DentistDetail> DentistDetails { get; set; }
    public virtual DbSet<DentistService> DentistServices { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

   
    public string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        return config["ConnectionStrings:DefaultConnection"];
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=flocalbrand.site,1444;Initial Catalog=DentalClinic;User ID=sa;Password=<YourStrong@Passw0rd>;TrustServerCertificate=True");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC280841F6C");

            entity.Property(e => e.Status).HasDefaultValue("Scheduled");

            entity.HasOne(d => d.Clinic).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Clinics");

            entity.HasOne(d => d.Customer).WithMany(p => p.AppointmentCustomers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Users_Customer");

            entity.HasOne(d => d.Dentist).WithMany(p => p.AppointmentDentists)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Users_Dentist");

            entity.HasOne(d => d.Service).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Services");
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.HasKey(e => e.ClinicId).HasName("PK__Clinics__3347C2DD332B6F58");

            entity.HasOne(d => d.ClinicOwner).WithMany(p => p.Clinics)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clinics_Users");
            entity.HasOne(c => c.Address)
           .WithOne(a => a.Clinic)
           .HasForeignKey<Clinic>(c => c.AddressId)
           .HasConstraintName("FK_Clinic_Address");

        });

        modelBuilder.Entity<ClinicService>(entity =>
        {
            entity.HasKey(e => e.ClinicServiceId).HasName("PK__ClinicSe__32750C42B0F1B300");

            entity.HasOne(d => d.Clinic).WithMany(p => p.ClinicServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClinicServices_Clinics");

            entity.HasOne(d => d.Service).WithMany(p => p.ClinicServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClinicServices_Services");
        });

        modelBuilder.Entity<DentistDetail>(entity =>
        {
            entity.HasKey(e => e.DentistDetailId).HasName("PK__DentistD__859D62F153740678");

            entity.HasOne(d => d.Clinic).WithMany(p => p.DentistDetails).HasConstraintName("FK_DentistDetails_Clinics");

            entity.HasOne(d => d.User).WithOne(p => p.DentistDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DentistDetails_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A1B65C4C2");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB00A1639594A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C3AD45CF5");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });
        modelBuilder.Entity<DentistService>()
           .HasKey(ds => new { ds.DentistId, ds.ServiceId });

        modelBuilder.Entity<DentistService>()
            .HasOne(ds => ds.Dentist)
            .WithMany(d => d.DentistServices)
            .HasForeignKey(ds => ds.DentistId);

        modelBuilder.Entity<DentistService>()
            .HasOne(ds => ds.Service)
            .WithMany(s => s.DentistServices)
            .HasForeignKey(ds => ds.ServiceId);
       

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
