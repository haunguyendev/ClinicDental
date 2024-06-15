using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

[Index("Username", Name = "UQ__Users__536C85E476385DEC", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("RoleID")]
    public int RoleId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Email { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    public string? Address { get; set; }

    [InverseProperty("ClinicOwner")]
    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();

    [InverseProperty("Doctor")]
    public virtual ICollection<DoctorDetail> DoctorDetails { get; set; } = new List<DoctorDetail>();

    [InverseProperty("User")]
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;
}
