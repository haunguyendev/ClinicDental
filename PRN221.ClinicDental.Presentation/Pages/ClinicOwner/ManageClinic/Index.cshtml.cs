using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Presentation.Pages.ClinicOwner.ManageClinic
{
    public class IndexModel : PageModel
    {
        private readonly PRN221.ClinicDental.Data.Models.ClinicDentalDbContext _context;

        public IndexModel(PRN221.ClinicDental.Data.Models.ClinicDentalDbContext context)
        {
            _context = context;
        }

        public IList<Clinic> Clinic { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Clinic = await _context.Clinics
                .Include(c => c.Address)
                .Include(c => c.ClinicOwner).ToListAsync();
        }
    }
}
