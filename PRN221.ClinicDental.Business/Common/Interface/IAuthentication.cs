using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.Common.Interface
{
    public interface IAuthentication
    {
        public string Hash(string password);

        public bool Verify(string passwordHash, string inputPassword);
    }
}
