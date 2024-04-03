using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class Admin : Manager
    {
        public Admin() 
        {
            login = "admin";
            password = "54321";
            infoUser = login;
            PermissionsAccess = true;
        }
    }
}
