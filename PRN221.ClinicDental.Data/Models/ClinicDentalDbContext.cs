using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<DoctorDetail> DoctorDetails { get; set; }

    public virtual DbSet<DoctorService> DoctorServices { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

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

        => optionsBuilder.UseSqlServer(GetConnectionString());


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA2E7780162");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Docto__5070F446");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Patie__4F7CD00D");

            entity.HasOne(d => d.Service).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Servi__5165187F");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__BBF8A7E1FF9F2535");

            entity.HasOne(d => d.DoctorDetails).WithMany(p => p.Certificates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Certifica__Docto__440B1D61");
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.HasKey(e => e.ClinicId).HasName("PK__Clinics__3347C2FD2CBEEE56");

            entity.HasOne(d => d.ClinicOwner).WithMany(p => p.Clinics)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clinics__ClinicO__3D5E1FD2");
        });

        modelBuilder.Entity<DoctorDetail>(entity =>
        {
            entity.HasKey(e => e.DoctorDetailsId).HasName("PK__DoctorDe__F0985D4BC164048C");

            entity.HasOne(d => d.Clinic).WithMany(p => p.DoctorDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorDet__Clini__412EB0B6");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorDet__Docto__403A8C7D");
        });

        modelBuilder.Entity<DoctorService>(entity =>
        {
            entity.HasKey(e => e.DoctorServiceId).HasName("PK__DoctorSe__2CCB45B486E07B75");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorSer__Docto__4BAC3F29");

            entity.HasOne(d => d.Service).WithMany(p => p.DoctorServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorSer__Servi__4CA06362");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patients__970EC346FD0AE72F");

            entity.HasOne(d => d.User).WithMany(p => p.Patients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patients__UserID__46E78A0C");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A84338956");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB0EA33E83494");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC57D204BE");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleID__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
