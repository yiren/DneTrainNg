using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Models
{
    public class ApplicationRole:IdentityRole
    {
        public ApplicationRole()
        {

        }
        public ApplicationRole(string roleName)
        {
            this.Name = roleName;
        }
    }
}
