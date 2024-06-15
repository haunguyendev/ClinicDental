using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

public partial class DoctorDetail
{
    [Key]
    [Column("DoctorDetailsID")]
    public int DoctorDetailsId { get; set; }

    [Column("DoctorID")]
    public int DoctorId { get; set; }

    [Column("ClinicID")]
    public int ClinicId { get; set; }

    [StringLength(100)]
    public string? Specialty { get; set; }

    [InverseProperty("Doctor")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("DoctorDetails")]
    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    [ForeignKey("ClinicId")]
    [InverseProperty("DoctorDetails")]
    public virtual Clinic Clinic { get; set; } = null!;

    [ForeignKey("DoctorId")]
    [InverseProperty("DoctorDetails")]
    public virtual User Doctor { get; set; } = null!;

    [InverseProperty("Doctor")]
    public virtual ICollection<DoctorService> DoctorServices { get; set; } = new List<DoctorService>();
}
