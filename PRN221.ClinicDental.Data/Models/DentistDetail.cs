using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

[Index("UserId", Name = "UQ__DentistD__1788CC4D6F77906B", IsUnique = true)]
public partial class DentistDetail
{
    [Key]
    public int DentistDetailId { get; set; }

    public int UserId { get; set; }

    public int? ClinicId { get; set; }

    [StringLength(255)]
    public string? Certificate { get; set; }

    [StringLength(255)]
    public string? Qualification { get; set; }

    [StringLength(255)]
    public string? Experience { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("DentistDetails")]
    public virtual Clinic? Clinic { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("DentistDetail")]
    public virtual User User { get; set; } = null!;
    [InverseProperty("Dentist")]
    public virtual ICollection<DentistService> DentistServices { get; set; } = new List<DentistService>();
}
