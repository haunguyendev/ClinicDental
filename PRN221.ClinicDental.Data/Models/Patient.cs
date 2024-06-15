using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

public partial class Patient
{
    [Key]
    [Column("PatientID")]
    public int PatientId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [InverseProperty("Patient")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [ForeignKey("UserId")]
    [InverseProperty("Patients")]
    public virtual User User { get; set; } = null!;
}
