using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

[Index("ClinicId", "ServiceId", Name = "UQ_ClinicService", IsUnique = true)]
public partial class ClinicService
{
    [Key]
    public int ClinicServiceId { get; set; }

    public int ClinicId { get; set; }

    public int ServiceId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("ClinicServices")]
    public virtual Clinic Clinic { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("ClinicServices")]
    public virtual Service Service { get; set; } = null!;
}
