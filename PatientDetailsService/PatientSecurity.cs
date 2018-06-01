using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PatientDataAccess;

namespace PatientDetailsService
{
    public class PatientSecurity
    {
        public static bool Login(string username, string password)
        {
            using (PatientsDBEntities entities = new PatientsDBEntities())
            {
                return entities.Users.Any(user =>
                       user.User_Name.Equals(username, StringComparison.OrdinalIgnoreCase)
                                          && user.Password == password);
            }
        }
    }
}