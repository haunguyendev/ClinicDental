using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

public partial class Appointment
{
    [Key]
    [Column("AppointmentID")]
    public int AppointmentId { get; set; }

    [Column("PatientID")]
    public int PatientId { get; set; }

    [Column("DoctorID")]
    public int DoctorId { get; set; }

    [Column("ServiceID")]
    public int ServiceId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime AppointmentDateTime { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("Appointments")]
    public virtual DoctorDetail Doctor { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("Appointments")]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("Appointments")]
    public virtual Service Service { get; set; } = null!;
}
