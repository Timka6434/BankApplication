using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BankApplication
{
    internal class AuthenticationService
    {
        public static bool PermissionsAccess;

        public static bool Authenticate(string login, string password)
        {
            List<User> users = new List<User>()
            {
                new Admin(),
                new Manager()
            };

            foreach (var user in users)
            {
                if(user.login == login && user.password == password)
                {
                    PermissionsAccess = user.PermissionsAccess;
                    return true;
                }
            }
            return false;
        }
    }
}
