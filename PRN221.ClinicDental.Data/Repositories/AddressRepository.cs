using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Repositories
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(ClinicDentalDbContext context) : base(context)
        {
            
        }

        public async Task<Address> GetAddressById(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x=> x.AddressId == id);
        }

        public async Task<List<Address>> GetAll()
        {
           return await _context.Addresses.ToListAsync();
        }

        
    }
}
