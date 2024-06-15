using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

public partial class DoctorService
{
    [Key]
    [Column("DoctorServiceID")]
    public int DoctorServiceId { get; set; }

    [Column("DoctorID")]
    public int DoctorId { get; set; }

    [Column("ServiceID")]
    public int ServiceId { get; set; }

    [ForeignKey("DoctorId")]
    [InverseProperty("DoctorServices")]
    public virtual DoctorDetail Doctor { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("DoctorServices")]
    public virtual Service Service { get; set; } = null!;
}
