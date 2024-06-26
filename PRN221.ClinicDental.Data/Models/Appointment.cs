using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

public partial class Appointment
{
    [Key]
    public int AppointmentId { get; set; }

    public int CustomerId { get; set; }

    public int ServiceId { get; set; }

    public int ClinicId { get; set; }

    public int DentistId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime AppointmentTime { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }
    public int? Slot { get; set; }

    [StringLength(255)]
    public string? Notes { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("Appointments")]
    public virtual Clinic Clinic { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("AppointmentCustomers")]
    public virtual User Customer { get; set; } = null!;

    [ForeignKey("DentistId")]
    [InverseProperty("AppointmentDentists")]
    public virtual User Dentist { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("Appointments")]
    public virtual Service Service { get; set; } = null!;
}
