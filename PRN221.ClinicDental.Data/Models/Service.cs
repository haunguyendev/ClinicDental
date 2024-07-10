using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

public partial class Service
{
    [Key]
    public int ServiceId { get; set; }

    [StringLength(100)]
    public string ServiceName { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }
    public string? ImageURL { get; set; }

    [InverseProperty("Service")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("Service")]
    public virtual ICollection<ClinicService> ClinicServices { get; set; } = new List<ClinicService>();
    [InverseProperty("Service")]
    public virtual ICollection<DentistService> DentistServices { get; set; } = new List<DentistService>();

}
