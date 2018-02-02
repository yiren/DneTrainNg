using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Models
{
    public class ApplicationUser:IdentityUser
    {
        public virtual List<ApplicationToken> ApplicationTokens { get; set; }
    }
}
