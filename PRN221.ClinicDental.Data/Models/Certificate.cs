using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PRN221.ClinicDental.Data.Models;

public partial class Certificate
{
    [Key]
    [Column("CertificateID")]
    public int CertificateId { get; set; }

    [Column("DoctorDetailsID")]
    public int DoctorDetailsId { get; set; }

    [StringLength(255)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? IssuingOrganization { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? IssueDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpirationDate { get; set; }

    [ForeignKey("DoctorDetailsId")]
    [InverseProperty("Certificates")]
    public virtual DoctorDetail DoctorDetails { get; set; } = null!;
}
