using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

[Index("Username", Name = "UQ__Users__536C85E417F46290", IsUnique = true)]
[Index("Email", Name = "UQ__Users__A9D105347EAF180D", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;
    [StringLength(20)]
    public string PhoneNumber { get; set; }
    [StringLength(255)]
    public string Address { get; set; }
    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Appointment> AppointmentCustomers { get; set; } = new List<Appointment>();

    [InverseProperty("Dentist")]
    public virtual ICollection<Appointment> AppointmentDentists { get; set; } = new List<Appointment>();

    [InverseProperty("ClinicOwner")]
    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();

    [InverseProperty("User")]
    public virtual DentistDetail? DentistDetail { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;
}
