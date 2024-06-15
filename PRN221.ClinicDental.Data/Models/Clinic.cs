using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

public partial class Clinic
{
    [Key]
    [Column("ClinicID")]
    public int ClinicId { get; set; }

    [Column("ClinicOwnerID")]
    public int ClinicOwnerId { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public TimeOnly? OpeningTime { get; set; }

    public TimeOnly? ClosingTime { get; set; }

    [ForeignKey("ClinicOwnerId")]
    [InverseProperty("Clinics")]
    public virtual User ClinicOwner { get; set; } = null!;

    [InverseProperty("Clinic")]
    public virtual ICollection<DoctorDetail> DoctorDetails { get; set; } = new List<DoctorDetail>();
}
