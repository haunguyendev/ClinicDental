using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

public partial class Clinic
{
    [Key]
    public int ClinicId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;
    public int AddressId { get; set; }


    public int ClinicOwnerId { get; set; }

    public TimeOnly OpeningTime { get; set; }

    public TimeOnly ClosingTime { get; set; }
    public string ImageURL { get; set; }

    [ForeignKey("AddressId")]
    [InverseProperty("Clinic")]
    public virtual Address Address { get; set; } = null!;

    [InverseProperty("Clinic")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [ForeignKey("ClinicOwnerId")]
    [InverseProperty("Clinics")]
    public virtual User ClinicOwner { get; set; } = null!;

    [InverseProperty("Clinic")]
    public virtual ICollection<ClinicService> ClinicServices { get; set; } = new List<ClinicService>();

    [InverseProperty("Clinic")]
    public virtual ICollection<DentistDetail> DentistDetails { get; set; } = new List<DentistDetail>();
}
