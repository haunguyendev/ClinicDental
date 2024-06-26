using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Models
{
    public partial class Address
    {
        [Key]
        public int AddressId { get; set; }

        [StringLength(255)]
        public string StreetAddress { get; set; } = null!;

        [StringLength(100)]
        public string District { get; set; } = null!;
        [InverseProperty("Address")]
        public virtual Clinic Clinic { get; set; }

    }
}
