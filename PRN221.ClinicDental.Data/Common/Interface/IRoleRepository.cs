﻿using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Common.Interface
{
    public interface IRoleRepository:IRepositoryBase<Role>
    {
        Task<List<Role>> GetRoles();
    }
}
