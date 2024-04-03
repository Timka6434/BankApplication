using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    internal class Manager : User
    {
        public Manager() 
        {
            login = "manager";
            password = "12345";
            infoUser = login;
            PermissionsAccess = false;
        }
    }
}
