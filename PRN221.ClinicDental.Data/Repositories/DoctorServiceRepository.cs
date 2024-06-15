﻿using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Repositories
{
    public class DoctorServiceRepository : RepositoryBase<DoctorService>, IDoctorServiceRepository
    {
        public DoctorServiceRepository(ClinicDentalDbContext context) : base(context)
        {
        }
    }
}
